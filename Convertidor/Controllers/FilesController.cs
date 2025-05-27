
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;
using Microsoft.AspNetCore.StaticFiles;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FilesController : ControllerBase
    {
       
        private readonly IBmBienesFotoService _service;
        private readonly IConfiguration _configuration;

        public FilesController(IBmBienesFotoService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }
        

       
        [HttpGet]
        [Route("[action]/{nroPlaca}/{foto}")]
        public async Task<IActionResult> GetImage(string nroPlaca,string foto)
        {

            try
            {
                var settings = _configuration.GetSection("Settings").Get<Settings>();
                var destino = @settings.BmFiles;
                var filePatch = $"{destino}{nroPlaca}/{foto}";

                if (!System.IO.File.Exists(filePatch))
                {
                    foto = "no-product-image.png";
                    filePatch = $"{destino}/{foto}";
                }
                var provider = new FileExtensionContentTypeProvider();
                if (provider.TryGetContentType(filePatch,out var contenttype))
                {
                    contenttype = "image/png";
                }
                var bytes =await System.IO.File.ReadAllBytesAsync(filePatch);
                return File(bytes, contenttype, Path.GetFileName(filePatch));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
         

        }
        
        [HttpGet]
        [Route("[action]/{fileName}")]
        public async Task<IActionResult> GetPdfFiles(string fileName)
        {
            
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.ExcelFiles;
            var filePatch = $"{destino}/{fileName}";

            if (!System.IO.File.Exists(filePatch))
            {
                fileName = "NO_DATA.pdf";
                filePatch = $"{destino}/{fileName}";
            }
            var provider = new FileExtensionContentTypeProvider();
            if (provider.TryGetContentType(filePatch,out var contenttype))
            {
                contenttype = "application/pdf";
            }
            var bytes =await System.IO.File.ReadAllBytesAsync(filePatch);
            var result = File(bytes, contenttype, Path.GetFileName(filePatch));
            return result;


        }
        
        [HttpGet]
        [Route("[action]/{fileName}")]
        public async Task<IActionResult> GetTxtFiles(string fileName)
        {
            
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.ExcelFiles;
            var filePatch = $"{destino}/{fileName}";

            if (!System.IO.File.Exists(filePatch))
            {
                fileName = "NO_DATA.txt";
                filePatch = $"{destino}/{fileName}";
            }
            var provider = new FileExtensionContentTypeProvider();
            if (provider.TryGetContentType(filePatch,out var contenttype))
            {
                contenttype = "application/txt";
            }
            var bytes =await System.IO.File.ReadAllBytesAsync(filePatch);
            var result = File(bytes, contenttype, Path.GetFileName(filePatch));
            return result;


        }
        
     

    }
}
