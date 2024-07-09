using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntTmpSaldosRepository : ICntTmpSaldosRepository
    {
        private readonly DataContextCnt _context;

        public CntTmpSaldosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_TMP_SALDOS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_TMP_SALDOS.DefaultIfEmpty().ToListAsync();
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
