using System.Globalization;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Atp;
using Oracle.ManagedDataAccess.Client;

namespace Convertidor.Data.Repository.Rh
{
    public class RhTmpRetencionesIncesRepository : IRhTmpRetencionesIncesRepository
    {
		
        private readonly DataContext _context;

        public RhTmpRetencionesIncesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_TMP_RETENCIONES_INCES>> GetByProcesoId(int procesoId)
        {
            try
            {
                var result = await _context.RH_TMP_RETENCIONES_INCES.DefaultIfEmpty().Where(e => e.PROCESO_ID == procesoId).ToListAsync();
        
                return (List<RH_TMP_RETENCIONES_INCES>)result; 
            }
            catch (Exception ex)
            {
                //var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task Add(int procesoId, FilterRetencionesDto filter)
        {

            
            

            try
            {
                
                
               
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nRH.RH_P_RETENCION_INCES({procesoId},{filter.TipoNomina},{filter.FechaDesde},{filter.FechaHasta});\nEND;";

                var resultDiario =  _context.Database.ExecuteSqlInterpolated(xqueryDiario);

                

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
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM RH.RH_TMP_RETENCIONES_INCES WHERE PROCESO_ID= {procesoId};\nEND;";

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

