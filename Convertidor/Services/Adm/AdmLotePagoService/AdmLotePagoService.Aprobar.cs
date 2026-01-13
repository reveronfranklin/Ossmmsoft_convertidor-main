using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmLotePagoService;

public partial class AdmLotePagoService
{
        public async Task<ResultDto<AdmLotePagoResponseDto>> Aprobar(AdmLotePagoCambioStatusDto dto)
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

                if (lotePago.STATUS != "PE" )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote de pago debe estar Pendiente para poder Aprobarlo";
                    return result;
                }
                
                lotePago.STATUS = dto.Status;
                lotePago.SEARCH_TEXT = await GetSearchText(lotePago);

                lotePago.FECHA_UPD = DateTime.Now;
                lotePago.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(lotePago);
                await _chequesRepository.CambioEstatus(dto.Status,lotePago.CODIGO_LOTE_PAGO,conectado.Usuario,DateTime.Now);
                var pagoElectronicp=await _admPagoElectronicoService.GenerateFilePagoElectronico(lotePago.CODIGO_LOTE_PAGO,conectado.Usuario);
                var resultDto = await  MapAdmLotePagoDto(lotePago);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
                result.LinkData = pagoElectronicp.LinkData;
                result.LinkDataArlternative = pagoElectronicp.LinkDataArlternative;
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