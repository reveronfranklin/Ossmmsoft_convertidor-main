using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmOrdenPago;

public partial class AdmOrdenPagoService 
{
          public async Task<ResultDto<AdmOrdenPagoResponseDto>> Anular(AdmOrdenPagoAprobarAnular dto)
        {
            ResultDto<AdmOrdenPagoResponseDto> result = new ResultDto<AdmOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }
                var totalPagado = await GetMontoPagado(dto.CodigoOrdenPago);
                var validaAnularOrdenPago =await ValidaAnularOrdenPago(codigoOrdenPago,totalPagado);
                
                if (validaAnularOrdenPago!="")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message =validaAnularOrdenPago;
                    return result;
                }

                codigoOrdenPago.STATUS = "AN";

                
                codigoOrdenPago.CODIGO_EMPRESA = conectado.Empresa;
                codigoOrdenPago.USUARIO_UPD = conectado.Usuario;
                codigoOrdenPago.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoOrdenPago);
                //ACTUALIZAR ADM_BENEFICIARIO_OP MONTO A MONTO_ANULADO
                await _admBeneficiariosOpRepository.UpdateMontoAnulado(dto.CodigoOrdenPago);
               
                var pucOrdenPago = await _admPucOrdenPagoRepository.GetByOrdenPago(dto.CodigoOrdenPago);
                if (pucOrdenPago != null && pucOrdenPago.Count > 0)
                {

                    foreach (var item in pucOrdenPago)
                    {
                        
                        //ACTUALIZAR MONTO_ANULADO=MONTO-MONTO_PAGADO EN ADM_PUC_ORDEN_PAGO
                        await _admPucOrdenPagoRepository.UpdateMontoAnulado(item.CODIGO_PUC_ORDEN_PAGO);
                     
                        await _prePucCompromisosRepository.UpdateMontoCausadoById(
                            item.CODIGO_PUC_COMPROMISO, 0);
                    }
                   
                }
                
                await _preSaldosRepository.RecalcularSaldo(codigoOrdenPago.CODIGO_PRESUPUESTO);
                var descriptivas = await _admDescriptivaRepository.GetAll();
                var proveedores = await _admProveedoresRepository.GetByCodigo(codigoOrdenPago.CODIGO_PROVEEDOR);
                var resultDto = await MapOrdenPagoDto(codigoOrdenPago,descriptivas,proveedores);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }
        
}