using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm.AdmDocumentosOp;

public partial class AdmDocumentosOpService
{
        public async Task<AdmDocumentosOpResponseDto> MapDocumentosOpDto(ADM_DOCUMENTOS_OP dtos)
        {
            AdmDocumentosOpResponseDto itemResult = new AdmDocumentosOpResponseDto();
            itemResult.CodigoDocumentoOp = dtos.CODIGO_DOCUMENTO_OP;
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.FechaComprobante = dtos.FECHA_COMPROBANTE;
            itemResult.FechaComprobanteString = Fecha.GetFechaString(dtos.FECHA_COMPROBANTE);
            FechaDto fechaComprobanteObj = Fecha.GetFechaDto( dtos.FECHA_COMPROBANTE);
            itemResult.FechaComprobanteObj = (FechaDto) fechaComprobanteObj;    
            itemResult.PeriodoImpositivo = dtos.PERIODO_IMPOSITIVO;
            
            //Lista de valores ===> Titulo Id 31
            itemResult.DescripcionTipoOperacion = "";
            itemResult.TipoOperacionId = dtos.TIPO_OPERACION_ID;
            itemResult.DescripcionTipoOperacion =  await _admDescriptivaRepository.GetDescripcion(itemResult.TipoOperacionId);
            
            //Lista de valores ===> Titulo Id 32
            itemResult.DescripcionTipoDocumento = "";
            itemResult.TipoDocumentoId = dtos.TIPO_DOCUMENTO_ID;
            itemResult.DescripcionTipoDocumento =  await _admDescriptivaRepository.GetDescripcion(itemResult.TipoDocumentoId);

            //Lista de valores ===> Titulo Id 34
            itemResult.DescripcionTipoTransaccion = "";
            itemResult.TipoTransaccionId = dtos.TIPO_TRANSACCION_ID;
            itemResult.DescripcionTipoTransaccion =  await _admDescriptivaRepository.GetDescripcion(itemResult.TipoTransaccionId);

            //Lista de valores ===> Titulo Id 18
            itemResult.DescripcionTipoImpuesto = "";
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.DescripcionTipoImpuesto =  await _admDescriptivaRepository.GetDescripcion(itemResult.TipoImpuestoId);

            //Lista de valores ===> Titulo Id 33
            itemResult.DescripcionEstatusFisco = "";
            itemResult.EstatusFiscoId = dtos.ESTATUS_FISCO_ID;
            itemResult.DescripcionEstatusFisco =  await _admDescriptivaRepository.GetDescripcion((int)itemResult.EstatusFiscoId);

            
            itemResult.FechaDocumento = dtos.FECHA_DOCUMENTO;
            itemResult.FechaDocumentoString =Fecha.GetFechaString(dtos.FECHA_DOCUMENTO);
            FechaDto fechaDocumentoObj = Fecha.GetFechaDto(dtos.FECHA_DOCUMENTO);
            itemResult.FechaDocumentoObj = (FechaDto)fechaDocumentoObj;
            itemResult.NumeroDocumento = dtos.NUMERO_DOCUMENTO;
            itemResult.NumeroControlDocumento = dtos.NUMERO_CONTROL_DOCUMENTO;
            itemResult.MontoDocumento = dtos.MONTO_DOCUMENTO;
            itemResult.BaseImponible = dtos.BASE_IMPONIBLE;
            itemResult.MontoImpuesto = dtos.MONTO_IMPUESTO;
            itemResult.NumeroDocumentoAfectado = dtos.NUMERO_DOCUMENTO_AFECTADO;
         
            itemResult.MontoImpuestoExento = dtos.MONTO_IMPUESTO_EXENTO;
            itemResult.MontoRetenido = dtos.MONTO_RETENIDO;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.NumeroExpediente = dtos.NUMERO_EXPEDIENTE;
           
            

            return itemResult;
        }

        public async Task<List<AdmDocumentosOpResponseDto>> MapListDocumentosOpDto(List<ADM_DOCUMENTOS_OP> dtos)
        {
            List<AdmDocumentosOpResponseDto> result = new List<AdmDocumentosOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapDocumentosOpDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

}