using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntBancoArchivoRepository : ICntBancoArchivoRepository
    {
        private readonly DataContextCnt _context;

        public CntBancoArchivoRepository(DataContextCnt context)
        {
            _context = context;
        }


        public async Task<List<CNT_BANCO_ARCHIVO>> GetAll() 
        {
            try
            {
                var result = await _context.CNT_BANCO_ARCHIVO.DefaultIfEmpty().ToListAsync();
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
