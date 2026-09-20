using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;


namespace Convertidor.Data.Repository.Adm
{
    public class AdmCompromisosPendientesRepository : IAdmCompromisosPendientesRepository
    {
        private readonly DataContextAdm _context;
        public AdmCompromisosPendientesRepository(DataContextAdm context)
        {
            _context = context;
        }
  
        // Una orden pendiente tambien reserva el compromiso, aunque aun tenga saldo.
        private IQueryable<ADM_V_COMPROMISO_PENDIENTE> PendientesDisponibles()
        {
            return _context.ADM_V_COMPROMISO_PENDIENTE.Where(p =>
                p.MONTO_POR_CAUSAR > 0 &&
                !_context.ADM_COMPROMISO_OP.Any(c =>
                    c.CODIGO_IDENTIFICADOR == p.CODIGO_IDENTIFICADOR &&
                    c.ORIGEN_COMPROMISO_ID == p.ORIGEN_COMPROMISO_ID &&
                    _context.ADM_ORDEN_PAGO.Any(o =>
                        o.CODIGO_ORDEN_PAGO == c.CODIGO_ORDEN_PAGO &&
                        o.CODIGO_PRESUPUESTO == p.CODIGO_PRESUPUESTO &&
                        (o.STATUS == null || o.STATUS != "AN"))));
        }

        public async Task<List<ADM_V_COMPROMISO_PENDIENTE>> GetCompromisosPendientesPorCodigoPresupuesto(int codigoPresupuesto)
        {
            try
            {
                var result = await PendientesDisponibles()
                    .Where(e =>  e.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        
        public async Task<ADM_V_COMPROMISO_PENDIENTE> GetCompromisosPendientesPorCodigoCompromiso(int codigoCompromiso)
        {
            try
            {
                var result = await PendientesDisponibles()
                    .Where(e =>  e.CODIGO_IDENTIFICADOR==codigoCompromiso).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        
   


    
    }
}
