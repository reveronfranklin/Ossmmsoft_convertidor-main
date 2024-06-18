using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PreResumenSaldoRepository : IPreResumenSaldoRepository
    {
	

        private readonly DataContextPre _context;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreResumenSaldoRepository(DataContextPre context, ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

   
        public async Task<List<PRE_RESUMEN_SALDOS>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {

                await DeleteByPresupuesto(codigoPresupuesto);
                
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nPRE.PRE_P_RESUMEN_PARTIDAS({codigoPresupuesto});\nEND;";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                
                var result = await _context.PRE_RESUMEN_SALDOS.DefaultIfEmpty().Where(x=> x.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
     
      

      
    
        
        public async Task<string> DeleteByPresupuesto(int codigoPresupuesto)
        {

            try
            {
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM PRE.PRE_RESUMEN_SALDOS WHERE CODIGO_PRESUPUESTO= {codigoPresupuesto};\nEND;";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
       

 

      


    }
}

