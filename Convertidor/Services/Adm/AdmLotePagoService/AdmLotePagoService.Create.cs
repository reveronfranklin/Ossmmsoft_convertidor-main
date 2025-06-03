using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmLotePagoService;

public partial class AdmLotePagoService
{
    public async Task<ResultDto<AdmLotePagoResponseDto>> Create(AdmLotePagoUpdateDto dto)
        {

            ResultDto<AdmLotePagoResponseDto> result = new ResultDto<AdmLotePagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (string.IsNullOrEmpty(dto.Titulo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
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
              
                
                var descriptivaTipoPago = await _admDescriptivaRepository.GetByCodigo(dto.TipoPagoId);
                if (descriptivaTipoPago==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Pago no existe";
                    return result;
                }
                var banco=await _bancoRepository.GetByCodigo(cuentaBanco.CODIGO_BANCO);
                
                var presupuesto=await _presupuestosRepository.GetByCodigo(conectado.Empresa,dto.CodigoPresupuesto);
                if (presupuesto==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }
                
                
                ADM_LOTE_PAGO entity = new ADM_LOTE_PAGO();
                entity.CODIGO_LOTE_PAGO = await _repository.GetNextKey();
                entity.FECHA_PAGO=dto.FechaPago;
                entity.CODIGO_CUENTA_BANCO=dto.CodigoCuentaBanco;
                entity.TIPO_PAGO_ID=dto.TipoPagoId;
                entity.TITULO = dto.Titulo;
                entity.STATUS = "PE";
                entity.SEARCH_TEXT = $"{descriptivaTipoPago.DESCRIPCION}-{cuentaBanco.NO_CUENTA}-{banco.NOMBRE}-{entity.STATUS}-{entity.TITULO}";

                entity.CODIGO_PRESUPUESTO=dto.CodigoPresupuesto;
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapAdmLotePagoDto(created.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";

                }
                else
                {

                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                }

                return result;  

              



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