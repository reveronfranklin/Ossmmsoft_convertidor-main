using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmPagosNotasTercerosRepository: IAdmPagosNotasTercerosRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmPagosNotasTercerosRepository(DataContextAdm context)
        {
            _context = context;
        }
      
      

    
        public async Task<List<ADM_V_NOTAS_TERCEROS>> GetByLote(int codigoLote)
        {
            try
            {
                var result = await _context.ADM_V_NOTAS_TERCEROS.DefaultIfEmpty().Where(e => e.CODIGO_LOTE_PAGO == codigoLote).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<ADM_V_NOTAS_TERCEROS>> GetByLoteCodigoPago(int codigoLote,int codigoPago)
        {
            try
            {
                var result = await _context.ADM_V_NOTAS_TERCEROS.DefaultIfEmpty().Where(e => e.CODIGO_LOTE_PAGO == codigoLote && e.CODIGO_CHEQUE==codigoPago).ToListAsync();

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

