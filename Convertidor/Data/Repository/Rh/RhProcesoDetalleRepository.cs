using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhProcesoDetalleRepository: IRhProcesoDetalleRepository
    {
		
        private readonly DataContext _context;

        public RhProcesoDetalleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<RH_PROCESOS_DETALLES> GetByCodigo(int codigoProcesoDetalle)
        {
            try
            {
                var result = await _context.RH_PROCESOS_DETALLES.DefaultIfEmpty().Where(e => e.CODIGO_DETALLE_PROCESO == codigoProcesoDetalle).FirstOrDefaultAsync();
        
                return (RH_PROCESOS_DETALLES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RH_PROCESOS_DETALLES>> GetByCodigoProceso(int codigoProceso)
        {
            try
            {
                var result = await _context.RH_PROCESOS_DETALLES.DefaultIfEmpty().Where(e => e.CODIGO_PROCESO == codigoProceso).ToListAsync();

                return (List<RH_PROCESOS_DETALLES>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RH_PROCESOS_DETALLES>> GetAll()
        {
            try
            {
                var result = await _context.RH_PROCESOS_DETALLES.DefaultIfEmpty().ToListAsync();

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

