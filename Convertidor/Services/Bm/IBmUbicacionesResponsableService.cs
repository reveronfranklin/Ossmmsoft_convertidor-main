using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm;

public interface IBmUbicacionesResponsableService
{
    Task<ResultDto<List<BmUbicacionesResponsablesResponseDto>>> GetAll();
    Task<ResultDto<List<BmUbicacionesResponsablesResponseDto>>> GetByUsuarioResponsable(string usuario);

}