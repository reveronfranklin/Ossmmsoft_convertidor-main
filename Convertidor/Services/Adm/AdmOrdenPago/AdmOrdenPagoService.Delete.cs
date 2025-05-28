using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmOrdenPago;

public partial class AdmOrdenPagoService 
{
           public async Task<ResultDto<AdmOrdenPagoDeleteDto>> Delete(AdmOrdenPagoDeleteDto dto) 
        {
            ResultDto<AdmOrdenPagoDeleteDto> result = new ResultDto<AdmOrdenPagoDeleteDto>(null);
            try
            {

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }
                var modificable= await OrdenDePagoEsModificable((int)dto.CodigoOrdenPago);
                if (!modificable)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Orden de pago no es Modificable(Status o Tiene Pagos)";
                    return result;
                }

                if (codigoOrdenPago.STATUS != "PE")
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no Puede ser Eliminada, necesita estar en Status PENDIENTE";
                    return result;
                }

                var pucOrdenPago = await _admPucOrdenPagoRepository.GetByOrdenPago(dto.CodigoOrdenPago);
                if (pucOrdenPago != null && pucOrdenPago.Count > 0)
                {

                    foreach (var item in pucOrdenPago)
                    {
                        await _prePucCompromisosRepository.UpdateMontoCausadoById(
                            item.CODIGO_PUC_COMPROMISO, 0);
                    }
                   
                }
             
              

                var deleted = await _repository.Delete(dto.CodigoOrdenPago);

                await _prePresupuestosRepository.RecalcularSaldo(codigoOrdenPago.CODIGO_PRESUPUESTO);

                
                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
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