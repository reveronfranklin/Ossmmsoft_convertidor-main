using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntBancoArchivoRepository
    {
        Task<List<CNT_BANCO_ARCHIVO>> GetAll();
    }
}
