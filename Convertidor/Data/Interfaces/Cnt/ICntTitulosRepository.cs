using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntTitulosRepository
	{

        Task<CNT_TITULOS> GetByCodigo(int tituloId);
        Task<List<CNT_TITULOS>> GetAll();
        Task<ResultDto<CNT_TITULOS>> Add(CNT_TITULOS entity);
        Task<ResultDto<CNT_TITULOS>> Update(CNT_TITULOS entity);
        Task<string> Delete(int tituloId);
        Task<int> GetNextKey();
        Task<CNT_TITULOS> GetByCodigoString(string codigo);
    }
}

