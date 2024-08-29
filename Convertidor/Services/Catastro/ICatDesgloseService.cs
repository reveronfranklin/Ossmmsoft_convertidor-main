using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDesgloseService
    {
        Task<ResultDto<List<CatDesgloseResponseDto>>> GetAll();
        Task<ResultDto<CatDesgloseResponseDto>> Create(CatDesgloseUpdateDto dto);
    }
}
