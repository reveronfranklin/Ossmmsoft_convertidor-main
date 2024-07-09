using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntTmpAnaliticoRepository : ICntTmpAnaliticoRepository
    {
        private readonly DataContextCnt _context;

        public CntTmpAnaliticoRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_TMP_ANALITICO>> GetAll()
        {
            try
            {
                var result = await _context.CNT_TMP_ANALITICO.DefaultIfEmpty().ToListAsync();
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
