using Convertidor.Data.Entities;

namespace Convertidor.Data.Interfaces
{
    public interface IPRE_INDICE_CAT_PRGRepository
    {
        Task<IEnumerable<PRE_INDICE_CAT_PRG>> GetByLastDay(int days);
        Task<IEnumerable<PRE_INDICE_CAT_PRG>> GetAll();
    }
}
