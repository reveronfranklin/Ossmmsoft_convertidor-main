using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDescriptivasRepository
    {
        Task<List<CAT_DESCRIPTIVAS>> GetByTitulo(int tituloId);
       
    }
}
