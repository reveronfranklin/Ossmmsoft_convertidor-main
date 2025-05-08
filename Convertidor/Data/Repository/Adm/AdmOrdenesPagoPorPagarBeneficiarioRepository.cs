using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmOrdenesPagoPorPagarBeneficiarioRepository:IAdmOrdenesPagoPorPagarBeneficiarioRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmOrdenesPagoPorPagarBeneficiarioRepository(DataContextAdm context)
        {
            _context = context;
        }
      
       

      

        public async Task<List<ADM_V_OP_POR_PAGAR_BENE>> GetByOrdenPago(int codigoOrdenPago)
        {
            try
            {
                var result = await _context.ADM_V_OP_POR_PAGAR_BENE
                    .Where(x=>x.CODIGO_ORDEN_PAGO == codigoOrdenPago)
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

