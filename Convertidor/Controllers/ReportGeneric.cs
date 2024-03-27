using System.Data;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
// HTML to PDF

//Excel reference

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReportGenericController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public ReportGenericController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       
     

        [HttpGet]
        [Route("[action]")] 
        public async Task<IActionResult> GetAll()
        {
            
            //https://www.youtube.com/watch?v=F6dkjsmuw6I
            //Convert SQL query result to a list of objects in C# | DataReader to generic list
            try
            {
                XElement list = null;
                string newJson = "";
                string connString = @"Data Source=10.211.55.4:1521/XE;User Id=SIS;Password=SIS;Validate Connection=true;";
                using (OracleConnection cn = new OracleConnection(connString))
                {
                    cn.Open();

                    var queryString = @"SELECT CODIGO_SOURCE,REPORTE,CODIGO_REPORTE  FROM SIS.SIS_SOURCE";
                    //list = Query(queryString,cn,"SIS_SOURCE");
                    newJson =ReaderToJson(queryString, cn);
                }
                //DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(newJson, (typeof(DataTable)));
              
          
            
         
                return Ok(newJson);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    
        public static string XmlToJson(string xml)
        {
            var doc = XDocument.Parse(xml);
            return JsonConvert.SerializeXNode(doc);
         
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
        
        
        
        public XElement Query(string query,OracleConnection cn ,string recordName) 
        {
            try
            {
              
                OracleCommand q = new OracleCommand(query, cn);
                var reader = q.ExecuteReader();
                XElement resultNode = new XElement("DATA");
                while (reader.Read())
                {   
                    XElement row = new XElement(recordName); 
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        
                       
                        row.Add( new XElement(reader.GetName(i), reader.GetValue(i)));
                    }
                    resultNode.Add(row);
                }
                reader.Close();

                return resultNode; 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
            
        }
    
    }
}
