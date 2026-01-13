using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ITmpDetalleLibroService
    {

        Task<ResultDto<List<TmpDetalleLibroResponseDto>>> GetAll();
        Task<ResultDto<TmpDetalleLibroResponseDto>> Create(TmpDetalleLibroUpdateDto dto);
        Task<ResultDto<TmpDetalleLibroResponseDto>> Update(TmpDetalleLibroUpdateDto dto);
        Task<ResultDto<TmpDetalleLibroDeleteDto>> Delete(TmpDetalleLibroDeleteDto dto);
    }
}
