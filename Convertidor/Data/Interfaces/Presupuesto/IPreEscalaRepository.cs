using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
    public interface IPreEscalaRepository
    {
        Task<PRE_ESCALA> GetByCodigo(int codigoEscala);
        Task<List<PRE_ESCALA>> GetAll();
        Task<ResultDto<PRE_ESCALA>> Add(PRE_ESCALA entity);
        Task<ResultDto<PRE_ESCALA>> Update(PRE_ESCALA entity);
        Task<string> Delete(int codigoEscala);
        Task<int> GetNextKey();
    }
}
