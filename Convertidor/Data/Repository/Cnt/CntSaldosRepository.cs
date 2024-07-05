using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntSaldosRepository : ICntSaldosRepository
    {
        private readonly DataContextCnt _context;

        public CntSaldosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_SALDOS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_SALDOS.DefaultIfEmpty().ToListAsync();
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
