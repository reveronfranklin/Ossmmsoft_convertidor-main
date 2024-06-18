using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreNivelesPucRepository
    {
        Task<PRE_NIVELES_PUC> GetByCodigo(int codigoGrupo);
        Task<List<PRE_NIVELES_PUC>> GetAll();
        Task<ResultDto<PRE_NIVELES_PUC>> Add(PRE_NIVELES_PUC entity);
        Task<ResultDto<PRE_NIVELES_PUC>> Update(PRE_NIVELES_PUC entity);
        Task<string> Delete(int codigoGrupo);
        Task<int> GetNextKey();
    }
}

