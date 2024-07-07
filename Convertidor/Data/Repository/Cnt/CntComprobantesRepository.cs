using Convertidor.Data.Entities;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntComprobantesRepository : ICntComprobantesRepository
    {
        private readonly DataContextCnt _context;

        public CntComprobantesRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_COMPROBANTES>> GetAll()
        {
            try
            {
                var result = await _context.CNT_COMPROBANTES.DefaultIfEmpty().ToListAsync();
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
