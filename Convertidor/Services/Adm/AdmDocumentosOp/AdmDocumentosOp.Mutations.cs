using System.Globalization;
using Convertidor.Data.Entities.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Services.Adm.AdmDocumentosOp;

public partial class AdmDocumentosOpService
{
    public async Task ReconstruirRetenciones(int codigoOrdenPago)
    {
        // Tambien admite llamadas independientes del alta/edicion/borrado del documento.
        var ownsTransaction = _context.Database.CurrentTransaction == null;
        await using var transaction = ownsTransaction
            ? await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable)
            : null;

        var documentos = await IvaDocumentoConsultas.Documentos(_context.ADM_DOCUMENTOS_OP
            .Where(x => x.CODIGO_ORDEN_PAGO == codigoOrdenPago)).ToListAsync();
        var tiposIva = await IvaDocumentoConsultas.Tipos(_context.ADM_DESCRIPTIVAS
            .Where(x => x.CODIGO == "IVA").OrderBy(x => x.DESCRIPCION_ID)).ToListAsync();
        if (tiposIva.Count == 0)
            throw new InvalidOperationException("No existe configuracion de familia IVA para las retenciones");

        var tipoIds = tiposIva.Select(x => x.DESCRIPCION_ID).ToList();
        var existentes = await _context.ADM_RETENCIONES_OP.AsTracking()
            .Where(x => x.CODIGO_ORDEN_PAGO == codigoOrdenPago && tipoIds.Contains(x.TIPO_RETENCION_ID))
            .OrderBy(x => x.CODIGO_RETENCION_OP).ToListAsync();
        if (existentes.Any(x => x.EXTRA4 != IvaDocumentoRetencion.Marca &&
            (!string.IsNullOrWhiteSpace(x.EXTRA1) || !string.IsNullOrWhiteSpace(x.EXTRA2) ||
             !string.IsNullOrWhiteSpace(x.EXTRA3) || !string.IsNullOrWhiteSpace(x.EXTRA4))))
            throw new InvalidOperationException("Los campos de alicuota de la retencion IVA estan ocupados; requiere revision");
        var entradas = new List<IvaDocumentoRetencion.Documento>();
        foreach (var documento in documentos.Where(x => x.MONTO_RETENIDO > 0))
        {
            var impuesto = await IvaDocumentoConsultas.Tipos(_context.ADM_DESCRIPTIVAS
                .Where(x => x.DESCRIPCION_ID == documento.TIPO_IMPUESTO_ID)).SingleOrDefaultAsync();
            var estatus = documento.ESTATUS_FISCO_ID.HasValue
                ? await IvaDocumentoConsultas.Tipos(_context.ADM_DESCRIPTIVAS
                    .Where(x => x.DESCRIPCION_ID == documento.ESTATUS_FISCO_ID.Value)).SingleOrDefaultAsync() : null;
            if (impuesto == null || estatus == null)
                throw new InvalidOperationException($"Configuracion IVA invalida en documento {documento.CODIGO_DOCUMENTO_OP}");
            var alicuota = IvaDocumentoRetencion.LeerPorcentaje(impuesto.EXTRA1);
            var porcentaje = IvaDocumentoRetencion.LeerPorcentaje(estatus.EXTRA1);
            if (porcentaje == 0)
                throw new InvalidOperationException($"Porcentaje retenido inconsistente en documento {documento.CODIGO_DOCUMENTO_OP}");
            var conceptos = await IvaDocumentoConsultas.Conceptos(_context.ADM_RETENCIONES
                .Where(x => x.EXTRA1 == estatus.CODIGO && tipoIds.Contains(x.TIPO_RETENCION_ID))).ToListAsync();
            var titulos = tiposIva.Where(x => conceptos.Any(c => c.TIPO_RETENCION_ID == x.DESCRIPCION_ID))
                .Select(x => x.TITULO_ID).Distinct().ToList();
            if (titulos.Count != 1)
                throw new InvalidOperationException($"Configuracion de concepto de retencion ausente o ambigua en documento {documento.CODIGO_DOCUMENTO_OP}");
            var tipo = IvaDocumentoRetencion.ResolverTipo(tiposIva, titulos[0], alicuota);
            var conceptosDelTipo = conceptos.Where(x => x.TIPO_RETENCION_ID == tipo.DESCRIPCION_ID).ToList();
            if (conceptosDelTipo.Count != 1)
                throw new InvalidOperationException($"Concepto IVA {alicuota}% para estatus fiscal {documento.ESTATUS_FISCO_ID} ausente o duplicado. Complete la configuracion de retenciones IVA");
            entradas.Add(new IvaDocumentoRetencion.Documento(documento, conceptosDelTipo[0].CODIGO_RETENCION,
                porcentaje, alicuota, tipo.DESCRIPCION_ID));
        }

        var grupos = IvaDocumentoRetencion.Agrupar(entradas);
        var disponibles = existentes.ToList();
        var numeroOrden = await _context.ADM_ORDEN_PAGO.Where(x => x.CODIGO_ORDEN_PAGO == codigoOrdenPago)
            .Select(x => x.NUMERO_COMPROBANTE).SingleAsync();
        var numero = existentes.Select(x => x.NUMERO_COMPROBANTE)
            .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x) && x != "0");
        if (string.IsNullOrWhiteSpace(numero) && numeroOrden > 0)
            numero = numeroOrden.Value.ToString("0", CultureInfo.InvariantCulture);
        var siguiente = (await _context.ADM_RETENCIONES_OP.Select(x => (int?)x.CODIGO_RETENCION_OP).MaxAsync() ?? 0) + 1;

        foreach (var grupo in grupos)
        {
            var actual = disponibles.FirstOrDefault(x => IvaDocumentoRetencion.MismoGrupo(x, grupo));
            if (actual == null)
            {
                // Reutilizar una fila al cambiar alicuota o migrar una retencion antigua.
                actual = disponibles.FirstOrDefault(x => !grupos.Any(g => IvaDocumentoRetencion.MismoGrupo(x, g)));
            }
            if (actual == null)
            {
                grupo.CODIGO_RETENCION_OP = siguiente++;
                grupo.NUMERO_COMPROBANTE = numero ?? string.Empty;
                _context.ADM_RETENCIONES_OP.Add(grupo);
            }
            else
            {
                disponibles.Remove(actual);
                IvaDocumentoRetencion.Actualizar(actual, grupo, numero);
            }
        }
        _context.ADM_RETENCIONES_OP.RemoveRange(disponibles);
        await _context.SaveChangesAsync();
        await _admRetencionesOpService.ReplicaRetencionesEnAdmBeneficiariosOp(codigoOrdenPago);
        await _admRetencionesOpService.ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(codigoOrdenPago);
        if (ownsTransaction)
            await transaction.CommitAsync();
    }

    public async Task ReplicaIvaDocumentoEnAdmRetenciones(int codigoDocumento)
    {
        var documento = await _repository.GetCodigoDocumentoOp(codigoDocumento);
        if (documento != null)
            await ReconstruirRetenciones(documento.CODIGO_ORDEN_PAGO);
    }
}
