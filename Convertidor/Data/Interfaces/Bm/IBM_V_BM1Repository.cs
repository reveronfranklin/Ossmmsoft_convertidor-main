using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBM_V_BM1Repository
{
    Task<List<BM_V_BM1>> GetAll();

    Task<List<BM_V_BM1>> GetByPlaca(int codigoBien);
}