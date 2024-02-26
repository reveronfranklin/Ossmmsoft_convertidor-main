using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm;

public interface IBmConteoService
{
    Task<ResultDto<List<BmConteoResponseDto>>> GetAll();
    Task<ResultDto<BmConteoResponseDto>> Update(BmConteoUpdateDto dto);

    Task<ResultDto<BmConteoResponseDto>> Create(BmConteoUpdateDto dto);
    Task<ResultDto<BmConteoDeleteDto>> Delete(BmConteoDeleteDto dto);

    Task<ResultDto<bool>> CerrarConteo(BmConteoCerrarDto dto);

}