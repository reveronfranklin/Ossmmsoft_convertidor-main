using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatAuditoriaService
    {
        Task<ResultDto<List<CatAuditoriaResponseDto>>> GetAll();
        Task<ResultDto<CatAuditoriaResponseDto>> Create(CatAuditoriaUpdateDto dto);
        Task<ResultDto<CatAuditoriaResponseDto>> Update(CatAuditoriaUpdateDto dto);
    }
}
