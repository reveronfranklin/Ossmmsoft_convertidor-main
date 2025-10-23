using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmDocumentosOp;

public partial class AdmDocumentosOpService
{
    
    
        public async Task ReconstruirRetenciones(int codigoOrdenPago)
        {
            //1- Eliminar todas las Retenciones de la orden de pago adm_retenciones_op
            var descriptivaIva = await _admDescriptivaRepository.GetByCodigoDescriptivaTexto("IVA");
            
            await _admRetencionesOpRepository.DeleteByOrdePagoSinIva(codigoOrdenPago,descriptivaIva.DESCRIPCION_ID);
            
            //2- recorrer todos los documentos de la orden de pago y ejecutar por cada uno
            var documentos = await _repository.GetByCodigoOrdenPago(codigoOrdenPago);
            if (documentos != null && documentos.Count() > 0)
            {
                foreach (var item in documentos)
                {
                    await ReplicaIvaDocumentoEnAdmRetenciones(item.CODIGO_DOCUMENTO_OP);
                    
                }
            }
            
            //3- ReplicaIvaDocumentoEnAdmRetenciones(int codigoDocumento)
        }
        
        public async Task ReplicaIvaDocumentoEnAdmRetenciones(int codigoDocumento)
        {
            
           
            
            var codigoDocumentoOp = await _repository.GetCodigoDocumentoOp(codigoDocumento);
            if (codigoDocumentoOp != null && codigoDocumentoOp.MONTO_RETENIDO>0)
            {
                var documentos = await _repository.GetByCodigoOrdenPago(codigoDocumentoOp.CODIGO_ORDEN_PAGO);
                var totalMontoRetenido = documentos.Sum(x => x.MONTO_RETENIDO);
                var totalBaseImponible = documentos.Sum(x => x.BASE_IMPONIBLE);
                var totalExento = documentos.Sum(x => x.MONTO_IMPUESTO_EXENTO);
                int codigoRetencion = 0;
                int tipoRetencionId = 0;
                var descriptivaRetencion =await _admDescriptivaRepository.GetByCodigo((int)codigoDocumentoOp.ESTATUS_FISCO_ID);
                if (descriptivaRetencion != null)
                {  
                   
                    var descriptivaIva = await _admDescriptivaRepository.GetByCodigoDescriptivaTexto("IVA");
                    tipoRetencionId= descriptivaIva.DESCRIPCION_ID;
                    var admRetencion= await  _admRetencionesRepository.GetByExtra1(descriptivaRetencion.CODIGO);
                    codigoRetencion= admRetencion.CODIGO_RETENCION;
                }

                var retencionOp =
                    await _admRetencionesOpService.GetByOrdenPagoCodigoRetencionTipoRetencion(
                        codigoDocumentoOp.CODIGO_ORDEN_PAGO, codigoRetencion, tipoRetencionId);
                if (retencionOp == null)
                {
                    AdmRetencionesOpUpdateDto admRetencionesOpDto = new AdmRetencionesOpUpdateDto();
                    admRetencionesOpDto.CodigoRetencionOp = 0;
                    admRetencionesOpDto.CodigoOrdenPago = codigoDocumentoOp.CODIGO_ORDEN_PAGO;
                    admRetencionesOpDto.CodigoRetencion = 0;
                    admRetencionesOpDto.TipoRetencionId = 0;
                    descriptivaRetencion =await _admDescriptivaRepository.GetByCodigo((int)codigoDocumentoOp.ESTATUS_FISCO_ID);
                    if (descriptivaRetencion != null)
                    {  
                        admRetencionesOpDto.PorRetencion = Convert.ToDecimal(descriptivaRetencion.EXTRA1);
                        var descriptivaIva = await _admDescriptivaRepository.GetByCodigoDescriptivaTexto("IVA");
                        admRetencionesOpDto.TipoRetencionId = descriptivaIva.DESCRIPCION_ID;
                        var admRetencion= await  _admRetencionesRepository.GetByExtra1(descriptivaRetencion.CODIGO);
                        admRetencionesOpDto.CodigoRetencion = admRetencion.CODIGO_RETENCION;
                    }
                    admRetencionesOpDto.MontoRetencion = codigoDocumentoOp.MONTO_RETENIDO;
                    admRetencionesOpDto.BaseImponible = codigoDocumentoOp.BASE_IMPONIBLE + codigoDocumentoOp.MONTO_IMPUESTO_EXENTO;
                    admRetencionesOpDto.CodigoPresupuesto = codigoDocumentoOp.CODIGO_PRESUPUESTO;
                    admRetencionesOpDto.NumeroComprobante = "";
                    await _admRetencionesOpService.Create(admRetencionesOpDto);
                }
                else
                {
                    
                    var montoRetencion= totalMontoRetenido;
                    var baseImponible=totalBaseImponible + totalExento;
                    var response= await _admRetencionesOpRepository.UpdateMontos(retencionOp.CODIGO_RETENCION_OP,(decimal)montoRetencion,(decimal)baseImponible);
                    
                }
                    
                    
                
                
              
                
            }
            
        }

        
}