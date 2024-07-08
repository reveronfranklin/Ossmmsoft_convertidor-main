using Convertidor.Data.Entities;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntComprobantesRepository
    {
        Task<List<CNT_COMPROBANTES>> GetAll();
        Task<CNT_COMPROBANTES> GetByNumeroComprobante(string numeroComprobante);
        Task<CNT_COMPROBANTES> GetByCodigo(int codigoComprobante);
        Task<ResultDto<CNT_COMPROBANTES>> Add(CNT_COMPROBANTES entity);
        Task<ResultDto<CNT_COMPROBANTES>> Update(CNT_COMPROBANTES entity);
        Task<string> Delete(int codigoComprobante);
        Task<int> GetNextKey();

    }
}
