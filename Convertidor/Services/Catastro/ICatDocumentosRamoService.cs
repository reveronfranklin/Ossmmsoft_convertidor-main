using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatDocumentosRamoService
    {
        Task<ResultDto<List<CatDocumentosRamoResponseDto>>> GetAll();
    }
}
