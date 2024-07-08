using Convertidor.Data.Entities;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntComprobantesRepository
    {
        Task<List<CNT_COMPROBANTES>> GetAll();
        Task<CNT_COMPROBANTES> GetByNumeroComprobante(string numeroComprobante);
        Task<ResultDto<CNT_COMPROBANTES>> Add(CNT_COMPROBANTES entity);
        Task<int> GetNextKey();

    }
}
