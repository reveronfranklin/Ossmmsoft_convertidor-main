using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntBancoArchivoControlRepository
    {
        Task<List<CNT_BANCO_ARCHIVO_CONTROL>> GetAll();
        Task<CNT_BANCO_ARCHIVO_CONTROL> GetByCodigo(int codigoBancoArchivoControl);
        Task<ResultDto<CNT_BANCO_ARCHIVO_CONTROL>> Add(CNT_BANCO_ARCHIVO_CONTROL entity);
        Task<ResultDto<CNT_BANCO_ARCHIVO_CONTROL>> Update(CNT_BANCO_ARCHIVO_CONTROL entity);
        Task<string> Delete(int codigoBancoArchivoControl);
        Task<int> GetNextKey();
    }
}
