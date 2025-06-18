using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Globalization; // Necesario para CultureInfo
namespace Convertidor.Data.Repository.Rh
{
    public class RhTmpRetencionesFaovRepository : IRhTmpRetencionesFaovRepository
    {
		
        private readonly DataContext _context;

        public RhTmpRetencionesFaovRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_TMP_RETENCIONES_FAOV>> GetByProcesoId(int procesoId)
        {
            try
            {
                var result = await _context.RH_TMP_RETENCIONES_FAOV.DefaultIfEmpty().Where(e => e.PROCESO_ID == procesoId).ToListAsync();
        
                return (List<RH_TMP_RETENCIONES_FAOV>)result; 
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
                // 1. Parsear las cadenas de entrada a objetos DateTime
                //    Asegurarse de que el parseo es estricto al formato "dd/MM/yyyy"
                var desde = DateTime.ParseExact(fechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var hasta = DateTime.ParseExact(fechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                // 2. Formatear los objetos DateTime a cadenas "dd/MM/yyyy" usando InvariantCulture
                //    Esto es CRUCIAL para garantizar el formato esperado por el SP
                string fechaDesdeFormatoSP = desde.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                string fechaHastaFormatoSP = hasta.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);


                var qry =
                    $"DECLARE \nBEGIN\nRH_P_RETENCION_FAOV({procesoId},{tipoNomina},{fechaDesdeFormatoSP},{fechaHastaFormatoSP});\nEND;";
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nRH_P_RETENCION_FAOV({procesoId},{tipoNomina},{fechaDesdeFormatoSP},{fechaHastaFormatoSP});\nEND;";
                //FormattableString xqueryDiario = $"CALL RH_P_RETENCION_FAOV({procesoId},{tipoNomina},{fechaDesde},{fechaHasta});";
                Console.WriteLine(xqueryDiario.ToString());
                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);

                

            }
            catch (Exception ex)
            {
                var mess = ex.Message;

               Console.WriteLine(mess);
            }


        }

        public async Task<string> Delete(int procesoId)
        {

            try
            {
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM RH.RH_TMP_RETENCIONES_FAOV WHERE PROCESO_ID= {procesoId};\nEND;";

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

