using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDocumentosLegalesService
    {
        Task<ResultDto<List<CatDocumentosLegalesResponseDto>>> GetAll();
        Task<ResultDto<CatDocumentosLegalesResponseDto>> Create(CatDocumentosLegalesUpdateDto dto);
    }
}
