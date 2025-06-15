using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm
{
    public interface IBmReplicaRepository
    {
        Task<List<BM_ARTICULOS>> GetAllArticulos();
        Task ReplicarArticulos();
        Task ReplicarBienes();
        Task ReplicarMovimientosBienes();
        Task ReplicarDireccionesBienes();
        Task ReplicarClasificacionesBienes();
        Task ReplicarPersonas();
    }
}

