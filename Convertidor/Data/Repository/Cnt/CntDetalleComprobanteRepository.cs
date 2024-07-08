using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntDetalleComprobanteRepository : ICntDetalleComprobanteRepository
    {
        private readonly DataContextCnt _context;

        public CntDetalleComprobanteRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_DETALLE_COMPROBANTE>> GetAll()
        {
            try
            {
                var result = await _context.CNT_DETALLE_COMPROBANTE.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<CNT_DETALLE_COMPROBANTE>> GetByCodigoComprobante(int codigoComprobante)
        {
            try
            {
                var result = await _context.CNT_DETALLE_COMPROBANTE.DefaultIfEmpty().Where(x => x.CODIGO_COMPROBANTE == codigoComprobante).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
    }
}
