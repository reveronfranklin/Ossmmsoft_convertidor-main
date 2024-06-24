using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntEstadoCuentasRepository : ICntEstadoCuentasRepository
    {
        private readonly DataContextCnt _context;

        public CntEstadoCuentasRepository(DataContextCnt context)
        {
            _context = context;
        }


        public async Task<List<CNT_ESTADO_CUENTAS>> GetAll() 
        {
            try
            {
                var result = await _context.CNT_ESTADO_CUENTAS.DefaultIfEmpty().ToListAsync();
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
