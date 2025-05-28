using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Convertidor.Services.Adm.AdmRetencionesOp
{
    // Usa 'partial' para indicar que la clase se define en múltiples archivos
    public partial class AdmRetencionesOpService
    {
        public async Task<AdmRetencionesOpResponseDto> MapRetencionesOpDto(ADM_RETENCIONES_OP dtos)
        {

            try
            {
                AdmRetencionesOpResponseDto itemResult = new AdmRetencionesOpResponseDto();
                itemResult.CodigoRetencionOp = dtos.CODIGO_RETENCION_OP;
                itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
                itemResult.TipoRetencionId = dtos.TIPO_RETENCION_ID;
                itemResult.DescripcionTipoRetencion = "";
                var descriptivaTipoRetencion = await _admDescriptivaRepository.GetByCodigo(dtos.TIPO_RETENCION_ID);
                if (descriptivaTipoRetencion != null)
                {
                    itemResult.DescripcionTipoRetencion = descriptivaTipoRetencion.DESCRIPCION;
                }

                if (dtos.CODIGO_RETENCION == null)
                {
                    dtos.CODIGO_RETENCION = 0;
                }
                itemResult.CodigoRetencion = dtos.CODIGO_RETENCION;
                var admRetenciones = await _admRetencionesRepository.GetCodigoRetencion((int)dtos.CODIGO_RETENCION);
                itemResult.ConceptoPago = "";
                if (admRetenciones != null)
                {
                    itemResult.ConceptoPago = admRetenciones.CONCEPTO_PAGO;
                }

                if (dtos.POR_RETENCION == null) dtos.POR_RETENCION = 0;
                itemResult.PorRetencion = dtos.POR_RETENCION;
                if (dtos.POR_RETENCION == 0)
                {
                    itemResult.PorRetencion = admRetenciones.POR_RETENCION;

                }


                if (dtos.MONTO_RETENCION == null) dtos.MONTO_RETENCION = 0;
                itemResult.MontoRetencion = dtos.MONTO_RETENCION;

                if (dtos.BASE_IMPONIBLE == null) dtos.BASE_IMPONIBLE = 0;
                itemResult.BaseImponible = dtos.BASE_IMPONIBLE;

                itemResult.MontoRetenido = dtos.MONTO_RETENCION;

                itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
                itemResult.NumeroComprobante = dtos.NUMERO_COMPROBANTE;
                return itemResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           

        }

        public async Task<List<AdmRetencionesOpResponseDto>> MapListRetencionesOpDto(List<ADM_RETENCIONES_OP> dtos)
        {
            List<AdmRetencionesOpResponseDto> result = new List<AdmRetencionesOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapRetencionesOpDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }
    }
}