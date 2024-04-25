using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreMetasRepository
    {
        Task<PRE_METAS> GetByCodigo(int codigoMeta);
        Task<List<PRE_METAS>> GetAllByPresupuesto(int codigoPresupuesto);
        Task<ResultDto<PRE_METAS>> Add(PRE_METAS entity);
        Task<ResultDto<PRE_METAS>> Update(PRE_METAS entity);
        Task<string> Delete(int codigoMeta);
        Task<int> GetNextKey();
    }
}

