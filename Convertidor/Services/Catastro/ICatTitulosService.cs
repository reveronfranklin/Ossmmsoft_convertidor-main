using Convertidor.Dtos.Catastro;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Catastro
{
    public interface ICatTitulosService
    {
        Task<ResultDto<List<CatTitulosResponseDto>>> GetAll();
        Task<ResultDto<CatTitulosResponseDto>> Create(CatTitulosUpdateDto dto);
        Task<ResultDto<CatTitulosResponseDto>> Update(CatTitulosUpdateDto dto);
        Task<ResultDto<CatTitulosDeleteDto>> Delete(CatTitulosDeleteDto dto);
        Task<ResultDto<List<TreePUC>>> GetTreeTitulos();
    }
}
