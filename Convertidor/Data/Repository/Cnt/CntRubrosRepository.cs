using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntRubrosRepository : ICntRubrosRepository
    {
        private readonly DataContextCnt _context;

        public CntRubrosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_RUBROS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_RUBROS.DefaultIfEmpty().ToListAsync();
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
