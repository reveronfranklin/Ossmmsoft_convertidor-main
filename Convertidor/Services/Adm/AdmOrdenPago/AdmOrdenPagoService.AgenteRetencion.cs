using Convertidor.Data.Entities.Adm;

namespace Convertidor.Services.Adm.AdmOrdenPago;

public partial class AdmOrdenPagoService
{
    private async Task ActualizarDatosAgenteRetencion(ADM_ORDEN_PAGO ordenPago, int codigoEmpresa)
    {
        var empresa = await _sisEmpresaRepository.GetByCodigo(codigoEmpresa);
        if (empresa == null)
        {
            return;
        }

        ordenPago.NOMBRE_AGENTE_RETENCION = empresa.NOMBRE_EMPRESA;
        ordenPago.DIRECCION_AGENTE_RETENCION = empresa.EXTRA4;
        ordenPago.TELEFONO_AGENTE_RETENCION = empresa.EXTRA6;

        var tipoIdentificacion = await _sisDescriptivaRepository.GetById(empresa.IDENTIFICACION_ID);
        var prefijoRif = tipoIdentificacion?.CODIGO_DESCRIPCION ?? string.Empty;
        ordenPago.RIF_AGENTE_RETENCION = $"{prefijoRif}{empresa.NUMERO_IDENTIFICACION}";
    }
}
