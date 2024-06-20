using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntBancoArchivoRepository
    {
        Task<List<CNT_BANCO_ARCHIVO>> GetAll();
        Task<CNT_BANCO_ARCHIVO> GetByCodigo(int codigoBancoArchivo);
        Task<ResultDto<CNT_BANCO_ARCHIVO>> Add(CNT_BANCO_ARCHIVO entity);
        Task<ResultDto<CNT_BANCO_ARCHIVO>> Update(CNT_BANCO_ARCHIVO entity);
        Task<int> GetNextKey();
    }
}
