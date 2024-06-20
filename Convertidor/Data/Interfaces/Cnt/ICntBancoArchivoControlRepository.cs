using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntBancoArchivoControlRepository
    {
        Task<List<CNT_BANCO_ARCHIVO_CONTROL>> GetAll();
    }
}
