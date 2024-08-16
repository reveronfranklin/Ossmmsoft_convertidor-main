using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDescriptivasRepository
    {
        Task<List<CAT_DESCRIPTIVAS>> GetByTitulo(int tituloId);
        Task<List<CAT_DESCRIPTIVAS>> GetAll();
        Task<List<CAT_DESCRIPTIVAS>> GetByFKID(int descripcionIdFk);
        Task<CAT_DESCRIPTIVAS> GetByCodigo(int descripcionId);
        Task<CAT_DESCRIPTIVAS> GetByCodigoDescriptivaTexto(string codigo);
        Task<ResultDto<CAT_DESCRIPTIVAS>> Add(CAT_DESCRIPTIVAS entity);
        Task<int> GetNextKey();
    }
}
