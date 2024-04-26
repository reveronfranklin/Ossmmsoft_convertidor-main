using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreObjetivosRepository
    {
        Task<PRE_OBJETIVOS> GetByCodigo(int codigoObjetivo);
        Task<List<PRE_OBJETIVOS>> GetAll();
        Task<ResultDto<PRE_OBJETIVOS>> Add(PRE_OBJETIVOS entity);
        Task<ResultDto<PRE_OBJETIVOS>> Update(PRE_OBJETIVOS entity);
        Task<string> Delete(int codigoObjetivo);
        Task<int> GetNextKey();
    }
}

