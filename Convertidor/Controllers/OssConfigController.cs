using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Services.Sis;
using Convertidor.Dtos.Sis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OssConfigController : ControllerBase
    {
       
        private readonly IOssConfigServices _service;

        public OssConfigController(IOssConfigServices service)
        {

            _service = service;


        }

       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetListByClave(FilterClave filter)
        {
                var result = await _service.GetListByClave(filter.Clave);
                return Ok(result);
        }


    }
}
