using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBM_V_BM1Repository
{
    Task<List<BM_V_BM1>> GetAll();
    Task<List<BM_V_BM1>> GetAllByCodigoIcp(int codigoIcp);
    List<ICPGetDto> GetICP();
}