using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDocumentosLegalesService
    {
        Task<ResultDto<List<CatDocumentosLegalesResponseDto>>> GetAll();
        Task<ResultDto<CatDocumentosLegalesResponseDto>> Create(CatDocumentosLegalesUpdateDto dto);
        Task<ResultDto<CatDocumentosLegalesResponseDto>> Update(CatDocumentosLegalesUpdateDto dto);
        Task<ResultDto<CatDocumentosLegalesDeleteDto>> Delete(CatDocumentosLegalesDeleteDto dto);
    }
}
