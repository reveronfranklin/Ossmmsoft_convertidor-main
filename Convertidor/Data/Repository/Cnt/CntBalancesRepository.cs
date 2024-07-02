using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntBalancesRepository : ICntBalancesRepository
    {
        private readonly DataContextCnt _context;

        public CntBalancesRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_BALANCES>> GetAll()
        {
            try
            {
                var result = await _context.CNT_BALANCES.DefaultIfEmpty().ToListAsync();
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
