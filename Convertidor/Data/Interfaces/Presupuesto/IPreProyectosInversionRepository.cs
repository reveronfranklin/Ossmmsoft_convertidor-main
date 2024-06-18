using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
    public interface IPreProyectosInversionRepository
    {
        Task<PRE_PROYECTOS_INVERSION> GetByCodigo(int codigoProyectoInv);
        Task<List<PRE_PROYECTOS_INVERSION>> GetAll();
        Task<ResultDto<PRE_PROYECTOS_INVERSION>> Add(PRE_PROYECTOS_INVERSION entity);
        Task<ResultDto<PRE_PROYECTOS_INVERSION>> Update(PRE_PROYECTOS_INVERSION entity);
        Task<string> Delete(int codigoProyectoInv);
        Task<int> GetNextKey();
    }
}
