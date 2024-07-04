using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntAuxiliaresPucRepository : ICntAuxiliaresPucRepository
    {
        private readonly DataContextCnt _context;

        public CntAuxiliaresPucRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_AUXILIARES_PUC>> GetAll()
        {
            try
            {
                var result = await _context.CNT_AUXILIARES_PUC.DefaultIfEmpty().ToListAsync();
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
