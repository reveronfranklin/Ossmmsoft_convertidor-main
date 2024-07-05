using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntPeriodosRepository : ICntPeriodosRepository
    {
        private readonly DataContextCnt _context;

        public CntPeriodosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_PERIODOS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_PERIODOS.DefaultIfEmpty().ToListAsync();
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
