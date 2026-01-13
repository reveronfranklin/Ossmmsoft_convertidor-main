using Convertidor.Dtos.Adm.Pagos;

namespace Convertidor.Services.Adm.Pagos.AdmPagosService;

public partial class AdmPagosService
{
         public async Task<ResultDto<bool>> Delete(PagoDeleteDto dto)
        {
            int codigoOrdenPago = 0;
            ResultDto<bool> result = new ResultDto<bool>(false);
            try
            {
                var pago = await _repository.GetByCodigoCheque(dto.CodigoPago);
                if (pago == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Codigo de Pago No Existe";
                    return result;
                }
                if (pago.STATUS != "PE" )
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = $"Lote de pago no puede ser eliminado esta en estatus: {pago.STATUS}";
                    return result;
                }
                
                var beneficiario = await _beneficiariosPagosRepository.GetByPago(dto.CodigoPago);
                if (beneficiario is not null)
                {
                    codigoOrdenPago = (int)beneficiario.CODIGO_ORDEN_PAGO;
                }
                 var esModificable=await PagoEsModificable((int)pago.CODIGO_LOTE_PAGO);
                if (esModificable.Length > 0)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message =esModificable;
                    return result;
                }
                
                var deleted = await _repository.Delete(dto.CodigoPago);
                if (deleted.Length > 0)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message =deleted;
                    return result;
                }


                var totalPagado =
                    await _beneficiariosPagosRepository.GetTotalPagadoCodigoBeneficiarioOp(
                        (int)beneficiario.CODIGO_BENEFICIARIO_OP);
                    await _admBeneficiariosOpRepository.UpdateMontoPagado((int)beneficiario.CODIGO_BENEFICIARIO_OP, totalPagado); 

            
               
                result.Data = true;
                result.IsValid = true;
                result.Message ="";
                return result;
                
                
                
            }
            catch (Exception e)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = e.Message;
                return result;
            }
        }
      
}