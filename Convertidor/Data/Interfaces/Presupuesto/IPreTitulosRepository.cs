using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreTitulosRepository
	{

        Task<PRE_TITULOS> GetByCodigo(int tituloId);
        Task<List<PRE_TITULOS>> GetAll();
        Task<ResultDto<PRE_TITULOS>> Add(PRE_TITULOS entity);
        Task<ResultDto<PRE_TITULOS>> Update(PRE_TITULOS entity);
        Task<string> Delete(int tituloId);
        Task<int> GetNextKey();
        Task<PRE_TITULOS> GetByCodigoString(string codigo);
    }
}

