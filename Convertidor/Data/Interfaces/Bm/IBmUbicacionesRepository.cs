using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBmUbicacionesRepository
{
    Task<List<BM_V_UBICACIONES>> GetAll();
    Task<BM_V_UBICACIONES> GetByCodigoDirBien(int codigoDirBien);
}