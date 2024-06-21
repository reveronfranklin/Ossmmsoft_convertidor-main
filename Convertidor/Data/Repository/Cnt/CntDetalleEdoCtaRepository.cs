using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntDetalleEdoCtaRepository : ICntDetalleEdoCtaRepository
    {
        private readonly DataContextCnt _context;

        public CntDetalleEdoCtaRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_DETALLE_EDO_CTA>> GetAll() 
        {
            try 
            {
            
                var result = await _context.CNT_DETALLE_EDO_CTA.DefaultIfEmpty().ToListAsync();
                return result;

            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;

            }
        
        }

        public async Task<List<CNT_DETALLE_EDO_CTA>> GetByCodigoEstadoCuenta(int codigoEstadoCuenta)
        {
            try
            {
                var result = await _context.CNT_DETALLE_EDO_CTA.DefaultIfEmpty().Where(x => x.CODIGO_ESTADO_CUENTA == codigoEstadoCuenta).ToListAsync();

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
