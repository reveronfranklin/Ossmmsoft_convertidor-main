using System.Net;
using Convertidor.Dtos;
using Convertidor.Dtos.Sis;
using Microsoft.AspNetCore.Mvc;

namespace AppService.Api.Controllers
{


    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AppReportController : ControllerBase
    {
      
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public AppReportController(IConfiguration configuration,HttpClient httpClient)
        {
            _configuration = configuration;
            _client = httpClient;
        }

        public async Task<byte[]> ToArrayAsync(Stream stream)
        {
            var memoryStream = new MemoryStream();
            await memoryStream.CopyToAsync(stream);
            var result = memoryStream.ToArray();

            return result;
        }
        
       
        
        [HttpGet()]
        //[HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ReportTest()
        {
           



            string urlBase = "http://192.168.1.124:7779/reports/rwservlet?destype=cache&desformat=pdf&server=samiAIO_webpre&report=PRE_PUC2.rdf&userid=system/s4m1apps@samiapps";
           
            _client.BaseAddress = new Uri(urlBase);

            // la url final
            string url = urlBase;




            try
            {
                var result = await _client.GetByteArrayAsync(_client.BaseAddress);
                //string resultContent = await result.Content.ReadAsStringAsync();
                string nameFile =DateTime.Now.ToString() + ".pdf";
               
                
                var settings = _configuration.GetSection("Settings").Get<Settings>();

               
                var ruta = @settings.ExcelFiles;  
                var fileName = "report.pdf";
                string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);
                
               
                //string imageFullName = @"D:\Moore\Adjuntos\ListRecibos_Report.pdf";
                string imageFullName = ruta + "/" + fileName;
                //creo el fichero
               
                System.IO.File.WriteAllBytes(imageFullName, result);

                return Ok(fileName);




            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        
 
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Report(ReportRequestDto requestDto)
        {
           
            //string urlBase = "http://192.168.1.124:7779/reports/rwservlet?destype=cache&desformat=pdf&server=samiAIO_webpre&report=PRE_PUC2.rdf&userid=system/s4m1apps@samiapps";
            //string urlBase = "http://192.168.1.124:7779/reports/rwservlet?destype=cache&desformat=HTML&report=RH_RECIBOS_NOMINA.rdf&P_TIPO_NOMINA=10&P_TIPO_GENERACION=3&userid=sis/sis@samiapps";
            
            var settingUrl = _configuration.GetSection("settings").Get<Settings>();
            var urlString = @settingUrl.UrlReport;

            string urlBase = urlString;
            if (string.IsNullOrEmpty(requestDto.ReportUrl ))
            {
                requestDto.ReportUrl = "/rh/reports/RH_RECIBOS_NOMINA.rdf&userid=sis/sis@samiappsrs&desname=RCNOM&CODIGO_EMPRESA=13&P_TIPO_NOMINA=10&P_TIPO_GENERACION=3&P_FECHA_PAGO=’31/07/2023’";

            }
          
            
            
            string url = $"{urlBase}{requestDto.ReportUrl}";
            //string urlBase = requestDto.ReportUrl;
            _client.BaseAddress = new Uri(url);
            
            try
            {
                var result = await _client.GetByteArrayAsync(_client.BaseAddress);
                //string resultContent = await result.Content.ReadAsStringAsync();
                string nameFile =DateTime.Now.ToString() + ".pdf";
                var settings = _configuration.GetSection("Settings").Get<Settings>();
                var ruta = @settings.ExcelFiles;  
                var fileName =requestDto.ReportName;
                string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);
                //string imageFullName = @"D:\Moore\Adjuntos\ListRecibos_Report.pdf";
                string imageFullName = ruta + "/" + fileName;
                //creo el fichero
               
                System.IO.File.WriteAllBytes(imageFullName, result);

                return Ok(fileName);

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }



    }
}