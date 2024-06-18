using Convertidor.Data.Entities.Bm;

namespace Convertidor.Data.Interfaces.Bm
{
    public interface IBmDescriptivaRepository
	{

        Task<BM_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId);
        Task<List<BM_DESCRIPTIVAS>> GetByTituloId(int tituloId);
        Task<List<BM_DESCRIPTIVAS>> GetAll();
        Task<bool> GetByIdAndTitulo(int tituloId, int id);
        Task<BM_DESCRIPTIVAS> GetByCodigo(int descripcionId);
        Task<ResultDto<BM_DESCRIPTIVAS>> Add(BM_DESCRIPTIVAS entity);
        Task<ResultDto<BM_DESCRIPTIVAS>> Update(BM_DESCRIPTIVAS entity);
        Task<string> Delete(int descripcionId);
        Task<int> GetNextKey();
        Task<BM_DESCRIPTIVAS> GetByCodigoDescriptivaTexto(string codigo);
        Task<List<BM_DESCRIPTIVAS>> GetByFKID(int descripcionIdFk);
    }
}

