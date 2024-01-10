using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_V_MTR_UNIDAD_EJECUTORARepository : IPRE_V_MTR_UNIDAD_EJECUTORARepository
    {
	
        private readonly DataContextPre _context;

        public PRE_V_MTR_UNIDAD_EJECUTORARepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<List<PRE_V_MTR_UNIDAD_EJECUTORA>> GetAll()
        {
            try
            {

                var result = await _context.PRE_V_MTR_UNIDAD_EJECUTORA.DefaultIfEmpty().ToListAsync();
                return (List<PRE_V_MTR_UNIDAD_EJECUTORA>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_V_MTR_UNIDAD_EJECUTORA>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {

                var result = await _context.PRE_V_MTR_UNIDAD_EJECUTORA.DefaultIfEmpty().Where( x=>x.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();
                return (List<PRE_V_MTR_UNIDAD_EJECUTORA>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        


    }
}

