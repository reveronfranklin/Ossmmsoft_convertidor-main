using Convertidor.Dtos.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBmConteoHistoricoService
{
    Task<ResultDto<List<BmConteoHistoricoResponseDto>>> GetAll();
    Task<ResultDto<BmConteoHistoricoResponseDto>> GetByCodigoConteo(int codigoConteo);
    Task CreateReportConteoHistorico(int codigoConteo);
}