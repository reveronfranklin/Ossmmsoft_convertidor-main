using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
    public interface IPreEjecucionPresupuestariaRepository
    {
        Task<PRE_EJECUCION_PRESUPUESTARIA> GetByCodigo(int codigoEjePresupuestaria);
        Task<List<PRE_EJECUCION_PRESUPUESTARIA>> GetAll();
        Task<ResultDto<PRE_EJECUCION_PRESUPUESTARIA>> Add(PRE_EJECUCION_PRESUPUESTARIA entity);
        Task<ResultDto<PRE_EJECUCION_PRESUPUESTARIA>> Update(PRE_EJECUCION_PRESUPUESTARIA entity);
        Task<string> Delete(int codigoEjePresupuestaria);
        Task<int> GetNextKey();
    }
}
