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
    public class OssVariableController : ControllerBase
    {
       
        private readonly IOssVariableService _service;

        public OssVariableController(IOssVariableService service)
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
        public async Task<IActionResult> GetByCodigo(OssVariableFilterDto dto)
        {
            var result = await _service.GetByCodigo(dto);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetById(OssVariableFilterDto dto)
        {
            var result = await _service.GetById(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(OssVariableUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(OssVariableUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(OssVariableDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
