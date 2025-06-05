using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm.Pagos;

namespace Convertidor.Services.Adm.Pagos.AdmPagosService;

public partial class AdmPagosService
{
      public async Task<ResultDto<bool>> UpdateMonto(PagoUpdateMontoDto dto)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            try
            {
                
              

                
                var beneficiario = await _beneficiariosPagosRepository.GetCodigoBeneficiarioPago(dto.CodigoBeneficiarioPago);
                if (beneficiario == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "No existen beneficiario op";
                    return result;
                }
                var pago = await _repository.GetByCodigoCheque(beneficiario.CODIGO_CHEQUE);
                if (pago == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Codigo de Pago No Existe";
                    return result;
                }
                
                var lote = await _admLotePagoRepository.GetByCodigo((int)pago.CODIGO_LOTE_PAGO);
                if (lote == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Lote No Existe";
                    return result;
                }
                
                if (lote.STATUS != "PE" )
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = $"Lote de pago no puede ser Modificado esta en estatus: {lote.STATUS}";
                    return result;
                }
                

                
                if (dto.Monto<=0)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = $"Monto Invalido";
                    return result;
                }
                
                var esModificable=await PagoEsModificable((int)pago.CODIGO_LOTE_PAGO);
                if (esModificable.Length > 0)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message =esModificable;
                    return result;
                }
                
                var beneficiarioOp =
                    await _admBeneficiariosOpRepository.GetCodigoBeneficiarioOp((int)beneficiario.CODIGO_BENEFICIARIO_OP);
                if (beneficiarioOp != null)
                {
                    
                    var validarMontoPago = await ValidarMontoPago((int)beneficiario.CODIGO_BENEFICIARIO_OP, dto.Monto);

                    if (validarMontoPago.Length>0)
                    {
                        result.Data = false;
                        result.IsValid = false;
                        result.Message = validarMontoPago;
                        return result;
                    }
                }

           
                beneficiario.MONTO = dto.Monto;
                var conectado = await _sisUsuarioRepository.GetConectado();
                beneficiario.USUARIO_UPD=conectado.Usuario;
                beneficiario.FECHA_UPD = DateTime.Now;
                var updated = await _beneficiariosPagosRepository.Update(beneficiario);
                if (updated.IsValid && updated.Data != null)
                {
                    var totalPagado =
                        await _beneficiariosPagosRepository.GetTotalPagadoCodigoBeneficiarioOp(
                            (int)beneficiario.CODIGO_BENEFICIARIO_OP);
                    await _admBeneficiariosOpRepository.UpdateMontoPagado((int)beneficiario.CODIGO_BENEFICIARIO_OP, totalPagado); 

                    result.Data = true;
                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = false;
                    result.IsValid = updated.IsValid;
                    result.Message = updated.Message;
                }

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
        
        public async Task<bool> CreateBeneficiarioPago( PagoCreateDto dto,int codigoPresupuesto,int codigoPago)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            try
            {
                ADM_BENEFICIARIOS_CH entity = new ADM_BENEFICIARIOS_CH();
                entity.CODIGO_BENEFICIARIO_CH = await _beneficiariosPagosRepository.GetNextKey();
                entity.CODIGO_CHEQUE = codigoPago;
                entity.CODIGO_BENEFICIARIO_OP = dto.CodigoBeneficiarioOP;
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.NUMERO_ORDEN_PAGO = dto.NumeroOrdenPago;
                entity.MONTO = dto.Monto;
                entity.MONTO_ANULADO = 0;
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.CODIGO_PRESUPUESTO = codigoPresupuesto;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;
                await _beneficiariosPagosRepository.Add(entity);
                var totalPagado =
                    await _beneficiariosPagosRepository.GetTotalPagadoCodigoBeneficiarioOp(
                        (int)dto.CodigoBeneficiarioOP);
                await _admBeneficiariosOpRepository.UpdateMontoPagado((int)dto.CodigoBeneficiarioOP, totalPagado); 

                return true;
            }
            catch (Exception e)
            {
               return false;
            }
          


        }

        
}