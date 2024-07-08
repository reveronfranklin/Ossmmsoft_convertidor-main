using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntHistAnaliticoRepository : ICntHistAnaliticoRepository
    {
        private readonly DataContextCnt _context;

        public CntHistAnaliticoRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_HIST_ANALITICO>> GetAll()
        {
            try
            {
                var result = await _context.CNT_HIST_ANALITICO.DefaultIfEmpty().ToListAsync();
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
