using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace Convertidor.Data.Repository.Rh
{
    public class RhTmpRetencionesSindRepository : IRhTmpRetencionesSindRepository
    {
		
        private readonly DataContext _context;

        public RhTmpRetencionesSindRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_TMP_RETENCIONES_SIND>> GetByProcesoId(int procesoId)
        {
            try
            {
                var result = await _context.RH_TMP_RETENCIONES_SIND.DefaultIfEmpty().Where(e => e.PROCESO_ID == procesoId).ToListAsync();
        
                return (List<RH_TMP_RETENCIONES_SIND>)result; 
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


                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nRH.RH_P_RETENCION_SIND({procesoId},{tipoNomina},{fechaDesde},{fechaHasta});\nEND;";

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
                var entity = await GetByProcesoId(procesoId);
                if (entity.Count>0)
                {
                    _context.RH_TMP_RETENCIONES_SIND.RemoveRange(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }

    }
}

