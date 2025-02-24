using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm;

public interface IBmUbicacionesResponsableRepository
{
    Task<List<BM_V_UBICA_RESPONSABLE>> GetAll();
    Task<List<BM_V_UBICA_RESPONSABLE>> GetByUsuarioResponsable(string usuarioResponsable);

}