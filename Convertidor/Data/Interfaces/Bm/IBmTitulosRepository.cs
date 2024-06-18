using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm
{
	public interface IBmTitulosRepository
	{

        Task<BM_TITULOS> GetByCodigo(int tituloId);
        Task<List<BM_TITULOS>> GetAll();
        Task<ResultDto<BM_TITULOS>> Add(BM_TITULOS entity);
        Task<ResultDto<BM_TITULOS>> Update(BM_TITULOS entity);
        Task<string> Delete(int tituloId);
        Task<int> GetNextKey();
        Task<BM_TITULOS> GetByCodigoString(string codigo);
    }
}

