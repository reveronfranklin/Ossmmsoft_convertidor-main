using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatTitulosRepository
    {
        Task<List<CAT_TITULOS>> GetAll();
        Task<CAT_TITULOS> GetByTitulo(int tituloId);
        Task<CAT_TITULOS> GetByCodigoString(string codigoTitulo);
        Task<ResultDto<CAT_TITULOS>> Add(CAT_TITULOS entity);
        Task<ResultDto<CAT_TITULOS>> Update(CAT_TITULOS entity);
        Task<string> Delete(int tituloId);
        Task<int> GetNextKey();
    }
}
