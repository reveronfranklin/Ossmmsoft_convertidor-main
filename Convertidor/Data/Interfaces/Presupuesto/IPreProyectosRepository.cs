using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
    public interface IPreProyectosRepository
    {
        Task<PRE_PROYECTOS> GetByCodigo(int codigoProyecto);
        Task<List<PRE_PROYECTOS>> GetAll();
        Task<ResultDto<PRE_PROYECTOS>> Add(PRE_PROYECTOS entity);
        Task<ResultDto<PRE_PROYECTOS>> Update(PRE_PROYECTOS entity);
        Task<string> Delete(int codigoProyecto);
        Task<int> GetNextKey();
    }
}
