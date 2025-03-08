using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm;

public interface IBmPlacasCuarentenaService
{
    Task<ResultDto<List<BmPlacaCuarentenaResponseDto>>> GetAll();
    Task<ResultDto<BmPlacaCuarentenaResponseDto>> GetByCodigo(int codigo);
    Task<ResultDto<BmPlacaCuarentenaResponseDto>> Create(BmPlacaCuarentenaUpdateDto dto);
    Task<ResultDto<BmPlacaCuarentenaDeleteDto>> Delete(BmPlacaCuarentenaDeleteDto dto);
    
}