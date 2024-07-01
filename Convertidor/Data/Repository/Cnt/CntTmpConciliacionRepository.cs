using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntTmpConciliacionRepository : ICntTmpConciliacionRepository
    {
        private readonly DataContextCnt _context;

        public CntTmpConciliacionRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_TMP_CONCILIACION>> GetAll()
        {
            try
            {
                var result = await _context.CNT_TMP_CONCILIACION.DefaultIfEmpty().ToListAsync();
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
