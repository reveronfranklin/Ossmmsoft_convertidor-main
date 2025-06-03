using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmLotePagoService;

public partial class AdmLotePagoService
{
    public async Task<ResultDto<AdmLotePagoDeleteDto>> Delete(AdmLotePagoDeleteDto dto)
    {

        ResultDto<AdmLotePagoDeleteDto> result = new ResultDto<AdmLotePagoDeleteDto>(null);
        try
        {

            var lote = await _repository.GetByCodigo(dto.CodigoLotePago);
            if (lote == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Lote No existe";
                return result;
            }
            if (lote.STATUS != "PE" )
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = $"Lote de pago no muede ser eliminado esta en estatus: {lote.STATUS}";
                return result;
            }

            var deleted = await _repository.Delete(dto.CodigoLotePago);

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