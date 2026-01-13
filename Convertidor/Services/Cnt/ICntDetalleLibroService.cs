using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntDetalleLibroService
    {
        Task<ResultDto<List<CntDetalleLibroResponseDto>>> GetAll();
        Task<ResultDto<List<CntDetalleLibroResponseDto>>> GetAllByCodigoLibro(int codigoLibro);
        Task<ResultDto<CntDetalleLibroResponseDto>> GetByCodigo(int codigoDetalleLibro);
        Task<ResultDto<CntDetalleLibroResponseDto>> Create(CntDetalleLibroUpdateDto dto);
        Task<ResultDto<CntDetalleLibroResponseDto>> Update(CntDetalleLibroUpdateDto dto);
        Task<ResultDto<CntDetalleLibroDeleteDto>> Delete(CntDetalleLibroDeleteDto dto);
    }
}
