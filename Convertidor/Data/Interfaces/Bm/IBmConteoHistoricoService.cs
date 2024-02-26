using Convertidor.Dtos.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBmConteoHistoricoService
{
    Task<ResultDto<List<BmConteoResponseDto>>> GetAll();
}