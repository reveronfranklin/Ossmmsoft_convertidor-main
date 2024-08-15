using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatTitulosRepository
    {
        Task<List<CAT_TITULOS>> GetAll();
        Task<CAT_TITULOS> GetByCodigo(int tituloId);
        Task<CAT_TITULOS> GetByCodigoString(string codigoTitulo);
        Task<ResultDto<CAT_TITULOS>> Add(CAT_TITULOS entity);
        Task<int> GetNextKey();
    }
}
