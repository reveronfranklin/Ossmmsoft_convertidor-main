using Convertidor.Dtos.Adm.Pagos;

namespace Convertidor.Services.Adm.Pagos.AdmPagosService;

public partial class AdmPagosService
{
          public async Task<ResultDto<PagoResponseDto>> Update(PagoUpdateDto dto)
        {
            ResultDto<PagoResponseDto> result = new ResultDto<PagoResponseDto>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            try
            {
                var pago = await _repository.GetByCodigoCheque(dto.CodigoPago);
                if (pago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo de Pago No Existe";
                    return result;
                }
                if (pago.STATUS != "PE" )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Lote de pago no muede ser Modificado esta en estatus: {pago.STATUS}";
                    return result;
                }

                var esModificable=await PagoEsModificable((int)pago.CODIGO_LOTE_PAGO);
                if (esModificable.Length > 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message =esModificable;
                    return result;
                }
                
                var beneficiario = await _beneficiariosPagosRepository.GetCodigoBeneficiarioPago(dto.CodigoBeneficiarioPago);
                if (beneficiario == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existen beneficiario op";
                    return result;
                }

                if (string.IsNullOrEmpty(dto.Motivo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }


                if (dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;

                }
                var validarMontoPago = await ValidarMontoPago((int)beneficiario.CODIGO_BENEFICIARIO_OP, dto.Monto);

                if (validarMontoPago.Length>0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = validarMontoPago;
                    return result;
                }
                
                
                PagoUpdateMontoDto pagoUpdateMontoDto = new PagoUpdateMontoDto();
                pagoUpdateMontoDto.CodigoBeneficiarioPago = dto.CodigoBeneficiarioPago;
                pagoUpdateMontoDto.Monto = dto.Monto;
                var resultUpdateMonto =await  UpdateMonto(pagoUpdateMontoDto);
                if (resultUpdateMonto.IsValid == false)
                {
                    result.Data = null;
                    result.IsValid = resultUpdateMonto.IsValid;
                    result.Message = resultUpdateMonto.Message;
                    return result;
                }
                
                pago.MOTIVO=dto.Motivo;
                pago.FECHA_UPD = DateTime.Now;
                pago.USUARIO_UPD = conectado.Usuario;
              
                await _repository.Update(pago);
                await _admLotePagoRepository.UpdateSearchText((int)pago.CODIGO_LOTE_PAGO);
                var resultDto = await MapChequesDto(pago);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
                return result;

            }
            catch (Exception e)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = e.Message;
                return result;
            }
            
        }

}