using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmLotePagoService;

public partial class AdmLotePagoService
{
    
    
        public async Task<string> GetSearchText(ADM_LOTE_PAGO dto)
        {
            var result = "";
            var cuentaBanco = await   _sisCuentaBancoRepository.GetById(dto.CODIGO_CUENTA_BANCO);
            var banco=await _bancoRepository.GetByCodigo(cuentaBanco.CODIGO_BANCO);
            var descriptivaTipoPago = await _admDescriptivaRepository.GetByCodigo(dto.TIPO_PAGO_ID);

            result = $"{descriptivaTipoPago.DESCRIPCION}-{cuentaBanco.NO_CUENTA}-{banco.NOMBRE}-{dto.STATUS}-{dto.TITULO}";

            return result;
        }
        public async Task<ResultDto<List<AdmLotePagoResponseDto>>> GetAll(AdmLotePagoFilterDto filter)
        {
            ResultDto<List<AdmLotePagoResponseDto>> result = new ResultDto<List<AdmLotePagoResponseDto>>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                filter.CodigEmpresa = conectado.Empresa;
                var lotes = await _repository.GetAll(filter);
                if (lotes.Data == null)
                {
                    result.Data = null;
                    result.CantidadRegistros=0;
                    result.Page = 0;
                    result.TotalPage = 0;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListLotePagoDto(lotes.Data);
                result.Data = resultDto;
                result.CantidadRegistros=lotes.CantidadRegistros;
                result.Page = lotes.Page;
                result.TotalPage = lotes.TotalPage;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
           
        }

        
}