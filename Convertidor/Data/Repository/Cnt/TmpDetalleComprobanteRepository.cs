using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class TmpDetalleComprobanteRepository : ITmpDetalleComprobanteRepository
    {
        private readonly DataContextCnt _context;

        public TmpDetalleComprobanteRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<TMP_DETALLE_COMPROBANTE>> GetAll()
        {
            try
            {
                var result = await _context.TMP_DETALLE_COMPROBANTE.DefaultIfEmpty().ToListAsync();
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
