using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
namespace Convertidor.Data.Repository.Sis
{
	public class SisSourceRepository: Interfaces.Sis.ISisSourceRepository
    {
		

        private readonly DataContextSis _context;
        private readonly IConfiguration _configuration;
        public SisSourceRepository(DataContextSis context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<SIS_SOURCE>> GetALL()
        {
            try
            {
                var result = await _context.SIS_SOURCE.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

     

        public async Task<SIS_SOURCE> GetById(int codigo)
        {
            try
            {
                var result = await _context.SIS_SOURCE.DefaultIfEmpty().Where(x => x.CODIGO_SOURCE == codigo).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<SIS_SOURCE>> Add(SIS_SOURCE entity)
        {
            ResultDto<SIS_SOURCE> result = new ResultDto<SIS_SOURCE>(null);
            try
            {

                entity.CODIGO_SOURCE = await GetNextKey();

                await _context.SIS_SOURCE.AddAsync(entity);
                await _context.SaveChangesAsync();


                result.Data = entity;
                result.IsValid = true;
                result.Message = "";
                return result;


            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
            
        }

        public async Task<ResultDto<SIS_SOURCE>> Update(SIS_SOURCE entity)
        {
            ResultDto<SIS_SOURCE> result = new ResultDto<SIS_SOURCE>(null);

            try
            {
                SIS_SOURCE entityUpdate = await GetById(entity.CODIGO_SOURCE);
                if (entityUpdate != null)
                {


                    _context.SIS_SOURCE.Update(entity);
                    await _context.SaveChangesAsync();
                    result.Data = entity;
                    result.IsValid = true;
                    result.Message = "";

                }
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }
        
        public async Task<ResultDto<string>> GetDataGenericReport(string queryString)
        {
            ResultDto<string> result = new ResultDto<string>(null);
            //https://www.youtube.com/watch?v=F6dkjsmuw6I
            //Convert SQL query result to a list of objects in C# | DataReader to generic list
            try
            {
               
                
                string newJson = "";
                var connString = _configuration.GetConnectionString("DefaultConnectionSIS");
                //string connString = @"Data Source=10.211.55.4:1521/XE;User Id=SIS;Password=SIS;Validate Connection=true;";
                using (OracleConnection cn = new OracleConnection(connString))
                {
                    cn.Open();

                   // var queryString = @"SELECT CODIGO_SOURCE,REPORTE,CODIGO_REPORTE  FROM SIS.SIS_SOURCE";
                 
                    newJson =ReaderToJson(queryString, cn);
                    cn.Close();
                }
                //DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(newJson, (typeof(DataTable)));

                result.Data = newJson;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception e)
            {
                result.Data = "";
                result.IsValid = false;
                result.Message = e.Message;
                return result;
            }

        }
        public static string ReaderToJson(string query,OracleConnection cn )
        { 
            OracleCommand q = new OracleCommand(query, cn);
            var rdr = q.ExecuteReader();
            
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.WriteStartArray();

                while (rdr.Read())
                {
                    jsonWriter.WriteStartObject();

                    int fields = rdr.FieldCount;

                    for (int i = 0; i < fields; i++)
                    {
                        jsonWriter.WritePropertyName(rdr.GetName(i));
                        jsonWriter.WriteValue(rdr[i]);
                    }

                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEndArray();

                return sw.ToString();
            }
        }
        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.SIS_SOURCE.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_SOURCE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_SOURCE + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }



    }

}

