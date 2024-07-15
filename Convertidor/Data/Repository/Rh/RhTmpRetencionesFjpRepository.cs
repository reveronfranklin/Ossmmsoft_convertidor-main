using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace Convertidor.Data.Repository.Rh
{
    public class RhTmpRetencionesFjpRepository : IRhTmpRetencionesFjpRepository
    {
		
        private readonly DataContext _context;

        public RhTmpRetencionesFjpRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_TMP_RETENCIONES_FJP>> GetByProcesoId(int procesoId)
        {
            try
            {
                var result = await _context.RH_TMP_RETENCIONES_FJP.DefaultIfEmpty().Where(e => e.PROCESO_ID == procesoId).ToListAsync();
        
                return (List<RH_TMP_RETENCIONES_FJP>)result; 
            }
            catch (Exception ex)
            {
                //var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task Add(int procesoId, int tipoNomina,string fechaDesde, string fechaHasta)
        {

           

            try
            {
                var desde =DateTime.ParseExact(fechaDesde, "dd/MM/yyyy", null);
                var hasta= DateTime.ParseExact(fechaDesde, "dd/MM/yyyy", null);

                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nRH.RH_P_RETENCION_FJP({procesoId},{tipoNomina},{desde},{hasta});\nEND;";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);

                

            }
            catch (Exception ex)
            {
                var mess = ex.Message;

                throw;
            }


        }

        public async Task<string> Delete(int procesoId)
        {

            try
            {
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM RH.RH_TMP_RETENCIONES_FJP WHERE PROCESO_ID= {procesoId};\nEND;";

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

