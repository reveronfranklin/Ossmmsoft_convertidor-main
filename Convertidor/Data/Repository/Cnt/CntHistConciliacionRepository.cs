using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntHistConciliacionRepository : ICntHistConciliacionRepository
    {
        private readonly DataContextCnt _context;

        public CntHistConciliacionRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_HIST_CONCILIACION>> GetAll()
        {
            try
            {
                var result = await _context.CNT_HIST_CONCILIACION.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<CNT_HIST_CONCILIACION>> GetByCodigoConciliacion(int codigoConciliacion)
        {
            try
            {
                var result = await _context.CNT_HIST_CONCILIACION.DefaultIfEmpty().Where(x => x.CODIGO_CONCILIACION == codigoConciliacion).ToListAsync();

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
