using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Services.Rh;
using Convertidor.Dtos.Rh;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhHPeriodosNominaController : ControllerBase
    {
       
        private readonly IRhHPeriodoService _service;

        public RhHPeriodosNominaController(IRhHPeriodoService service)
        {

            _service = service;
           
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(PeriodoFilterDto filter)
        {
            var result = await _service.GetAll(filter);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByYear(PeriodoFilterDto filter)
        {
            var result = await _service.GetByYear(filter.Year);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhHPeriodosUpdate dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhHPeriodosUpdate dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhHPeriodosDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
