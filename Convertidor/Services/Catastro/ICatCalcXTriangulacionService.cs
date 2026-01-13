using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public interface ICatCalcXTriangulacionService
    {
        Task<ResultDto<List<CatCalcXTriangulacionResponseDto>>> GetAll();
        Task<ResultDto<CatCalcXTriangulacionResponseDto>> Create(CatCalcXTriangulacionUpdateDto dto);
        Task<ResultDto<CatCalcXTriangulacionResponseDto>> Update(CatCalcXTriangulacionUpdateDto dto);
        Task<ResultDto<CatCalcXTriangulacionDeleteDto>> Delete(CatCalcXTriangulacionDeleteDto dto);
    }
}
