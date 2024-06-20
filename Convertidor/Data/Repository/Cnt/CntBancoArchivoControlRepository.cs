using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntBancoArchivoControlRepository :ICntBancoArchivoControlRepository
    {
        private readonly DataContextCnt _context;

        public CntBancoArchivoControlRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_BANCO_ARCHIVO_CONTROL>> GetAll()
        {
            try
            {
                var result = await _context.CNT_BANCO_ARCHIVO_CONTROL.DefaultIfEmpty().ToListAsync();
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
