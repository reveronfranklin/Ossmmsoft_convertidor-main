using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm
{
    public interface IBmArticulosRepository
	{

        Task<BM_ARTICULOS> GetByCodigoArticulo(int codigoArticulo);
        Task<List<BM_ARTICULOS>> GetAll();
        Task<BM_ARTICULOS> GetByCodigo(string codigo);
        Task<ResultDto<BM_ARTICULOS>> Add(BM_ARTICULOS entity);
        Task<ResultDto<BM_ARTICULOS>> Update(BM_ARTICULOS entity);
        Task<string> Delete(int codigoArticulo);
        Task<int> GetNextKey();
 
    }
}

