using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatCalcXTriangulacionService
    {
        Task<ResultDto<List<CatCalcXTriangulacionResponseDto>>> GetAll();
    }
}
