using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntDetalleLibroRepository : ICntDetalleLibroRepository
    {
        private readonly DataContextCnt _context;

        public CntDetalleLibroRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_DETALLE_LIBRO>> GetAll() 
        {
            try 
            {
            
                var result = await _context.CNT_DETALLE_LIBRO.DefaultIfEmpty().ToListAsync();
                return result;

            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;

            }
        
        }

        public async Task<List<CNT_DETALLE_LIBRO>> GetByCodigoLibro(int codigoLibro)
        {
            try
            {
                var result = await _context.CNT_DETALLE_LIBRO.DefaultIfEmpty().Where(x => x.CODIGO_LIBRO == codigoLibro).ToListAsync();

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
