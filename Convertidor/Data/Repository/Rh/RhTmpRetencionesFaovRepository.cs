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

      
        public async Task Add(int procesoId, int tipoNomina, string fechaDesde, string fechaHasta)
        {
            try
            {
                // 1. Parsear las fechas en formato "dd/MM/yyyy" (sin depender de la cultura del sistema)
                //var desde = DateTime.ParseExact(fechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //var hasta = DateTime.ParseExact(fechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                // 2. Usar parámetros de Oracle para evitar problemas de formato
                using (var connection = new OracleConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new OracleCommand("RH.RH_P_RETENCION_FAOV", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Parámetros con tipos explícitos (evita problemas de formato)
                        command.Parameters.Add("P_PROCESO_ID", OracleDbType.Int32).Value = procesoId;
                        command.Parameters.Add("P_CODIGO_TIPO_NOMINA", OracleDbType.Int32).Value = tipoNomina;
                        command.Parameters.Add("P_FECHA_DESDE", OracleDbType.Varchar2).Value = fechaDesde;
                        command.Parameters.Add("P_FECHA_HASTA", OracleDbType.Varchar2).Value = fechaHasta;

                        // Ejecutar el procedimiento
                        await command.ExecuteNonQueryAsync();
                    }
                }

                Console.WriteLine("Procedimiento ejecutado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Relanzar la excepción para manejo superior
            }
        }
        
        public async Task AddOld(int procesoId, int tipoNomina,string fechaDesde, string fechaHasta)
        {

         
            try
            {
                // 1. Parsear las cadenas de entrada a objetos DateTime
                //    Asegurarse de que el parseo es estricto al formato "dd/MM/yyyy"
                var desde = DateTime.ParseExact(fechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var hasta = DateTime.ParseExact(fechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var desdeFormateado = desde.ToString("dd-MMM-yy", new CultureInfo("es-ES")).ToUpper();
                var hastaFormateado = hasta.ToString("dd-MMM-yy", new CultureInfo("es-ES")).ToUpper();

                var newQuery = $"CALL RH.RH_P_RETENCION_FAOV({procesoId},{tipoNomina},{desdeFormateado},{hastaFormateado})";
                FormattableString xqueryDiario =$"CALL RH.RH_P_RETENCION_FAOV({procesoId},{tipoNomina},{desdeFormateado},{hastaFormateado})";
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

