using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Adm;
using Convertidor.Services.Sis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OssModuloController : ControllerBase
    {
       
        private readonly IOssModuloService _service;

        public OssModuloController(IOssModuloService service)
        {

            _service = service;


        }
        

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByCodigo(OssModuloFilterDto dto)
        {
            var result = await _service.GetByCodigo(dto);
            return Ok(result);
        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(OssModuloUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(OssModuloUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(OssModuloDeletedto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
