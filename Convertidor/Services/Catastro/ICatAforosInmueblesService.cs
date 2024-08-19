using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatAforosInmueblesService
    {
        Task<ResultDto<List<CatAforosInmueblesResponseDto>>> GetAll();
        Task<ResultDto<CatAforosInmueblesResponseDto>> Create(CatAforosInmueblesUpdateDto dto);
        Task<ResultDto<CatAforosInmueblesResponseDto>> Update(CatAforosInmueblesUpdateDto dto);
    }
}
