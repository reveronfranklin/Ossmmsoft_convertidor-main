using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDocumentosRamoService
    {
        Task<ResultDto<List<CatDocumentosRamoResponseDto>>> GetAll();
        Task<ResultDto<CatDocumentosRamoResponseDto>> Create(CatDocumentosRamoUpdateDto dto);
        Task<ResultDto<CatDocumentosRamoResponseDto>> Update(CatDocumentosRamoUpdateDto dto);
        Task<ResultDto<CatDocumentosRamoDeleteDto>> Delete(CatDocumentosRamoDeleteDto dto);
    }
}
