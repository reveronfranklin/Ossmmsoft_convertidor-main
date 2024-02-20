using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhPersonaController : ControllerBase
    {
       
        private readonly IRhPersonaService _service;

        public RhPersonaController(IRhPersonaService service)
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



        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllSimple()
        {
            var result = await _service.GetAllSimple();
            return Ok(result);
        }
       



        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByPersona(PersonaFilterDto filter)
        {
            var result = await _service.GetByCodigoPersona(filter.CodigoPersona);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetPersona(PersonaFilterDto filter)
        {
            var result = await _service.GetPersona(filter.CodigoPersona);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhPersonaUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhPersonaUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhPersonaDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }


    }
}
