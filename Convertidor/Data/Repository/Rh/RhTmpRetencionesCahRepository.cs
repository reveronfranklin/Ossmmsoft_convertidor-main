using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace Convertidor.Data.Repository.Rh
{
    public class RhTmpRetencionesCahRepository : IRhTmpRetencionesCahRepository
    {
		
        private readonly DataContext _context;

        public RhTmpRetencionesCahRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_TMP_RETENCIONES_CAH>> GetByProcesoId(int procesoId)
        {
            try
            {
                var result = await _context.RH_TMP_RETENCIONES_CAH.DefaultIfEmpty().Where(e => e.PROCESO_ID == procesoId).ToListAsync();
        
                return (List<RH_TMP_RETENCIONES_CAH>)result; 
            }
            catch (Exception ex)
            {
                //var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task Add(int procesoId, int tipoNomina,string fechaDesde, string fechaHasta)
        {

            var parameters = new OracleParameter[]
            {
                    new OracleParameter("procesoId", procesoId),
                    new OracleParameter("fechaDesde", fechaDesde),
                    new OracleParameter("fechaHasta", fechaHasta),
                    new OracleParameter("tipoNomina", tipoNomina)

            };

            try
            {


                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nRH.RH_P_RETENCION_CAH({procesoId},{tipoNomina},{fechaDesde},{fechaHasta});\nEND;";

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
                
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM RH.RH_TMP_RETENCIONES_CAH WHERE PROCESO_ID= {procesoId};\nEND;";

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

