using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmOrdenesPagoPorPagarRepository:IAdmOrdenesPagoPorPagarRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmOrdenesPagoPorPagarRepository(DataContextAdm context)
        {
            _context = context;
        }
      
       

      

        public async Task<List<ADM_V_OP_POR_PAGAR>> GetAll(int codigoPresupuesto)
        {
            try
            {
                var result = await _context.ADM_V_OP_POR_PAGAR
                    .Where(x=>x.CODIGO_PRESUPUESTO==codigoPresupuesto)
                    .OrderByDescending(x=>x.CODIGO_ORDEN_PAGO)
                    .DefaultIfEmpty().ToListAsync();

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

