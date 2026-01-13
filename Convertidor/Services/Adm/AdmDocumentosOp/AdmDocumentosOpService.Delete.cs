using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmDocumentosOp;

public partial class AdmDocumentosOpService
{
    
      public async Task<ResultDto<AdmDocumentosOpDeleteDto>> Delete(AdmDocumentosOpDeleteDto dto) 
        {
            ResultDto<AdmDocumentosOpDeleteDto> result = new ResultDto<AdmDocumentosOpDeleteDto>(null);
            try
            {
                
          
                
                var codigoDocumentoOp = await _repository.GetCodigoDocumentoOp(dto.CodigoDocumentoOp);
                if (codigoDocumentoOp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo documento op no existe";
                    return result;
                }
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(codigoDocumentoOp.CODIGO_ORDEN_PAGO);
                if (codigoOrdenPago==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }
                
                if (codigoOrdenPago.STATUS=="AP" || codigoOrdenPago.STATUS == "AN")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Orden de Pago no puede ser modificada esta: {codigoOrdenPago.STATUS}";
                    return result;
                }
                var resultDeleted =_admImpuestosDocumentosOpRepository.DeleteByDocumento(codigoDocumentoOp.CODIGO_DOCUMENTO_OP);

               var deleted = await _repository.Delete(dto.CodigoDocumentoOp);
               await ReconstruirRetenciones(codigoDocumentoOp.CODIGO_ORDEN_PAGO);
               var documentos = await _repository.GetByCodigoOrdenPago(codigoDocumentoOp.CODIGO_ORDEN_PAGO);
               var cantidadDocumentos = 0;
               if (documentos != null && documentos.Count() > 0)
               {
                   cantidadDocumentos = documentos.Count();
               }
               
                if (deleted.Length > 0)
                {
                    result.CantidadRegistros = cantidadDocumentos;
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    
                    result.CantidadRegistros = cantidadDocumentos;
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }
        
}