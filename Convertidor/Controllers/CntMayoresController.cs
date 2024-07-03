using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;

// HTML to PDF
using Convertidor.Services.Cnt;
using Microsoft.AspNetCore.Mvc;


namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CntMayoresController : ControllerBase
    {

        private readonly ICntMayoresService _service;

        public CntMayoresController(ICntMayoresService service)
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

        public async Task<IActionResult> Create (CntMayoresUpdateDto dto) 
        {
            var result = await _service.Create(dto);
            return Ok(result);

        }
    }
}
