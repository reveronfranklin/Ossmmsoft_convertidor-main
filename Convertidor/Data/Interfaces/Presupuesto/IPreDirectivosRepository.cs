using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreDirectivosRepository
    {


        Task<PRE_DIRECTIVOS> GetByCodigo(int codigoDirectivo);
        Task<List<PRE_DIRECTIVOS>> GetAll();
        Task<ResultDto<PRE_DIRECTIVOS>> Add(PRE_DIRECTIVOS entity);
        Task<ResultDto<PRE_DIRECTIVOS>> Update(PRE_DIRECTIVOS entity);
        Task<string> Delete(int codigoDirectivo);
        Task<int> GetNextKey();
    }
}

