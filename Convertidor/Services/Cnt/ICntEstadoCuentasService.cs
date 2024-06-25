using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntEstadoCuentasService
    {
        Task<ResultDto<List<CntEstadoCuentasResponseDto>>> GetAll();
        Task<ResultDto<CntEstadoCuentasResponseDto>> Create(CntEstadoCuentasUpdateDto dto);
        Task<ResultDto<CntEstadoCuentasResponseDto>> Update(CntEstadoCuentasUpdateDto dto);
    }
}
