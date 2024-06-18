using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
    public interface IPreEquiposRepository
    {
        Task<PRE_EQUIPOS> GetByCodigo(int codigoEquipo);
        Task<List<PRE_EQUIPOS>> GetAll();
        Task<ResultDto<PRE_EQUIPOS>> Add(PRE_EQUIPOS entity);
        Task<ResultDto<PRE_EQUIPOS>> Update(PRE_EQUIPOS entity);
        Task<string> Delete(int codigoEquipo);
        Task<int> GetNextKey();
    }
}
