using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntAuxiliaresRepository : ICntAuxiliaresRepository
    {
        private readonly DataContextCnt _context;

        public CntAuxiliaresRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_AUXILIARES>> GetAll()
        {
            try
            {
                var result = await _context.CNT_AUXILIARES.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<CNT_AUXILIARES>> GetByCodigoMayor(int codigoMayor)
        {
            try
            {
                var result = await _context.CNT_AUXILIARES.DefaultIfEmpty().Where(x => x.CODIGO_MAYOR == codigoMayor).ToListAsync();

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
