using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmLotePagoService;

public partial class AdmLotePagoService
{
         public async Task AnularMontoPagadoEnOrdenPago(int codigoLotePago)
        {
            AdmChequeFilterDto filter = new AdmChequeFilterDto();
            filter.CodigoLote=codigoLotePago;
            filter.SearchText = "";
            filter.PageNumber = 1;
            filter.PageSize = 1000;
            var pagos=await _chequesRepository.GetByLote(filter);
            if (pagos.Data !=null )
            {
                foreach (var itemPago in pagos.Data)
                {
                    var beneficiariosLotePago=await _admBeneficiariosPagosRepository.GetByPago(itemPago.CODIGO_CHEQUE);
                    if (beneficiariosLotePago!=null && beneficiariosLotePago.CODIGO_BENEFICIARIO_OP!=null)
                    {
                        await _admBeneficiariosOpRepository.UpdateMontoPagado((int)beneficiariosLotePago
                            .CODIGO_BENEFICIARIO_OP,0);
                    }
                }
            }
            
          
        }
        
        public async Task<ResultDto<AdmLotePagoResponseDto>> CambioStatus(AdmLotePagoCambioStatusDto dto)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            ResultDto<AdmLotePagoResponseDto> result = new ResultDto<AdmLotePagoResponseDto>(null);
            try
            {
                var lotePago=await _repository.GetByCodigo(dto.CodigoLotePago);
                if (lotePago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote no existe";
                    return result;
                }

               
                
                lotePago.STATUS = dto.Status;
                lotePago.SEARCH_TEXT = await GetSearchText(lotePago);

                lotePago.FECHA_UPD = DateTime.Now;
                lotePago.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(lotePago);
                await _chequesRepository.CambioEstatus(dto.Status,lotePago.CODIGO_LOTE_PAGO,conectado.Usuario,DateTime.Now);

                var resultDto = await  MapAdmLotePagoDto(lotePago);
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
            }
            return result;
        }
       
}