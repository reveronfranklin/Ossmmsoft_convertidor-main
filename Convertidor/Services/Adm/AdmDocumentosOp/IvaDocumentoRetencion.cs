using System.Globalization;
using Convertidor.Data.Entities.Adm;

namespace Convertidor.Services.Adm.AdmDocumentosOp;

// Snapshot de la alicuota del documento; TIPO_RETENCION_ID conserva la familia IVA.
public static class IvaDocumentoRetencion
{
    public const string Marca = "IVA_DOCUMENTO";

    public static decimal LeerPorcentaje(string valor)
    {
        if (!decimal.TryParse(valor?.Trim().Replace(',', '.'),
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture, out var porcentaje) || porcentaje < 0 || porcentaje > 100)
            throw new InvalidOperationException("Porcentaje IVA o de retencion invalido en la configuracion fiscal");
        return porcentaje;
    }

    public record Documento(ADM_DOCUMENTOS_OP Datos, int CodigoRetencion, decimal PorRetencion,
        decimal Alicuota);

    public static List<ADM_RETENCIONES_OP> Agrupar(IEnumerable<Documento> documentos, int tipoIva)
    {
        return documentos.Where(x => x.Datos.MONTO_RETENIDO > 0)
            .GroupBy(x => new { x.Datos.CODIGO_ORDEN_PAGO, x.Datos.TIPO_IMPUESTO_ID,
                x.CodigoRetencion, x.PorRetencion, x.Alicuota })
            .Select(grupo =>
            {
                var documento = grupo.First().Datos;
                var alicuota = grupo.Key.Alicuota.ToString("0.####", CultureInfo.InvariantCulture);
                return new ADM_RETENCIONES_OP
                {
                    CODIGO_ORDEN_PAGO = grupo.Key.CODIGO_ORDEN_PAGO,
                    TIPO_RETENCION_ID = tipoIva,
                    CODIGO_RETENCION = grupo.Key.CodigoRetencion,
                    POR_RETENCION = grupo.Key.PorRetencion,
                    BASE_IMPONIBLE = grupo.Sum(x => x.Datos.BASE_IMPONIBLE),
                    MONTO_RETENCION = grupo.Sum(x => x.Datos.MONTO_RETENIDO),
                    CODIGO_EMPRESA = documento.CODIGO_EMPRESA,
                    CODIGO_PRESUPUESTO = documento.CODIGO_PRESUPUESTO,
                    USUARIO_INS = documento.USUARIO_INS,
                    FECHA_INS = DateTime.Now,
                    EXTRA1 = grupo.Key.TIPO_IMPUESTO_ID.ToString(CultureInfo.InvariantCulture),
                    EXTRA2 = alicuota,
                    EXTRA3 = $"I.V.A. ({alicuota}%)",
                    EXTRA4 = Marca
                };
            }).ToList();
    }

    public static bool MismoGrupo(ADM_RETENCIONES_OP actual, ADM_RETENCIONES_OP esperado) =>
        actual.EXTRA4 == Marca && actual.EXTRA1 == esperado.EXTRA1 &&
        actual.EXTRA2 == esperado.EXTRA2 && actual.CODIGO_RETENCION == esperado.CODIGO_RETENCION &&
        actual.POR_RETENCION == esperado.POR_RETENCION;

    public static void Actualizar(ADM_RETENCIONES_OP actual, ADM_RETENCIONES_OP grupo, string numero)
    {
        actual.CODIGO_RETENCION = grupo.CODIGO_RETENCION;
        actual.POR_RETENCION = grupo.POR_RETENCION;
        actual.BASE_IMPONIBLE = grupo.BASE_IMPONIBLE;
        actual.MONTO_RETENCION = grupo.MONTO_RETENCION;
        actual.EXTRA1 = grupo.EXTRA1;
        actual.EXTRA2 = grupo.EXTRA2;
        actual.EXTRA3 = grupo.EXTRA3;
        actual.EXTRA4 = grupo.EXTRA4;
        actual.FECHA_UPD = DateTime.Now;
        if (string.IsNullOrWhiteSpace(actual.NUMERO_COMPROBANTE) || actual.NUMERO_COMPROBANTE == "0")
            actual.NUMERO_COMPROBANTE = numero ?? string.Empty;
    }
}
