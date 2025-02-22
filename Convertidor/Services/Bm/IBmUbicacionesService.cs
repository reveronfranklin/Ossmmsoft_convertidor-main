using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm;

public interface IBmUbicacionesService
{
    Task<ResultDto<List<BmUbicacionesResponseDto>>> GetAll();
}