using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;
using ADM_ORDEN_PAGO = Convertidor.Data.EntitiesDestino.ADM.ADM_ORDEN_PAGO;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmCompromisosPendientesRepository : IAdmCompromisosPendientesRepository
    {
        private readonly DataContextAdm _context;
        public AdmCompromisosPendientesRepository(DataContextAdm context)
        {
            _context = context;
        }
  
        public async Task<List<ADM_V_COMPROMISO_PENDIENTE>> GetCompromisosPendientesPorCodigoPresupuesto(int codigoPresupuesto)
        {
            try
            {
                var result = await _context.ADM_V_COMPROMISO_PENDIENTE
                    .Where(e =>  e.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();

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
