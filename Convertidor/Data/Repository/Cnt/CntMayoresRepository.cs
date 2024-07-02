using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntMayoresRepository : ICntMayoresRepository
    {
        private readonly DataContextCnt _context;

        public CntMayoresRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_MAYORES>> GetAll()
        {
            try
            {
                var result = await _context.CNT_MAYORES.DefaultIfEmpty().ToListAsync();
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
