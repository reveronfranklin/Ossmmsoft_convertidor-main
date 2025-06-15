using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmLotePagoService;

public partial class AdmLotePagoService
{
        public async Task<ResultDto<AdmLotePagoResponseDto>> Update(AdmLotePagoUpdateDto dto)
        {

            ResultDto<AdmLotePagoResponseDto> result = new ResultDto<AdmLotePagoResponseDto>(null);
            try
            {
                
                if (string.IsNullOrEmpty(dto.Titulo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }
                
                var conectado = await _sisUsuarioRepository.GetConectado();
                var lotePago=await _repository.GetByCodigo(dto.CodigoLotePago);
                if (lotePago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote no existe";
                    return result;
                }
                if (lotePago.STATUS != "PE" )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Lote de pago no muede ser Modificado esta en estatus: {lotePago.STATUS}";
                    return result;
                }
                var cuentaBanco = await   _sisCuentaBancoRepository.GetById(dto.CodigoCuentaBanco);
                if (cuentaBanco == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cuenta Banco no existe";
                    return result;
                }
                var banco=await _bancoRepository.GetByCodigo(cuentaBanco.CODIGO_BANCO);
                
                var descriptivaTipoPago = await _admDescriptivaRepository.GetByCodigo(dto.TipoPagoId);
                if (descriptivaTipoPago==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Pago no existe";
                    return result;
                }
                
                var descriptivaTipoPagoActual = await _admDescriptivaRepository.GetByCodigo(lotePago.TIPO_PAGO_ID);
                if (descriptivaTipoPago!=null && descriptivaTipoPagoActual!=null && descriptivaTipoPagoActual.EXTRA2!=descriptivaTipoPago.EXTRA2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Tipo de Pago Invalido, No puede ser Modificado el Tipo de Pago de: {descriptivaTipoPagoActual.EXTRA2} a: {descriptivaTipoPago.EXTRA2}";
                    return result;
                }

                
                var presupuesto=await _presupuestosRepository.GetByCodigo(conectado.Empresa,dto.CodigoPresupuesto);
                if (presupuesto==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }
                
                lotePago.FECHA_PAGO=dto.FechaPago;
                lotePago.CODIGO_CUENTA_BANCO=dto.CodigoCuentaBanco;
                //lotePago.TIPO_PAGO_ID=dto.TipoPagoId;
                lotePago.TITULO = dto.Titulo;
                lotePago.SEARCH_TEXT = $"{descriptivaTipoPago.DESCRIPCION}-{cuentaBanco.NO_CUENTA}-{banco.NOMBRE}-{lotePago.STATUS}-{lotePago.TITULO}";
                lotePago.CODIGO_PRESUPUESTO=dto.CodigoPresupuesto;
               
               
             
                lotePago.CODIGO_EMPRESA = conectado.Empresa;
                lotePago.FECHA_UPD = DateTime.Now;
                lotePago.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(lotePago);
                var resultDto = await  MapAdmLotePagoDto(lotePago);
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