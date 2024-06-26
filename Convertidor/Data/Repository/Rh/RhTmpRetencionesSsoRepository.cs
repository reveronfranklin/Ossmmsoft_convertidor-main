﻿using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace Convertidor.Data.Repository.Rh
{
    public class RhTmpRetencionesSsoRepository : IRhTmpRetencionesSsoRepository
    {
		
        private readonly DataContext _context;

        public RhTmpRetencionesSsoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_TMP_RETENCIONES_SSO>> GetByProcesoId(int procesoId)
        {
            try
            {
                var result = await _context.RH_TMP_RETENCIONES_SSO.DefaultIfEmpty().Where(e => e.PROCESO_ID == procesoId).ToListAsync();
        
                return (List<RH_TMP_RETENCIONES_SSO>)result; 
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


                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nRH.RH_P_RETENCION_SSO({procesoId},{tipoNomina},{fechaDesde},{fechaHasta});\nEND;";

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
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM RH.RH_TMP_RETENCIONES_SSO WHERE PROCESO_ID= {procesoId};\nEND;";

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

