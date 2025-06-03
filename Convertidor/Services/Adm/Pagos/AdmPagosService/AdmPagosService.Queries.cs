using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Pagos;

namespace Convertidor.Services.Adm.Pagos.AdmPagosService;

public partial class AdmPagosService
{
    public async Task<ResultDto<List<PagoResponseDto>>> GetByLote(AdmChequeFilterDto dto)
    {

        ResultDto<List<PagoResponseDto>> result = new ResultDto<List<PagoResponseDto>>(null);
        try
        {
            await _repository.UpdateSearchText(dto.CodigoLote);
            var pagos = await _repository.GetByLote(dto);
             
            if (pagos.Data != null && pagos.Data.Count() > 0)
            {
                var listDto = await MapListChequesDto(pagos.Data);

                result.Data = listDto;
                result.IsValid = true;
                result.CantidadRegistros = pagos.CantidadRegistros;
                result.Page = pagos.Page;
                result.TotalPage= pagos.TotalPage;
                result.Message = "";


                return result;
            }
            else
            {
                result.Data = null;
                result.IsValid = false;
                result.CantidadRegistros = 0;
                result.Page = 0;
                result.TotalPage= 0;
                result.Message = "No data";

                return result;
            }
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