using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Adm;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmDescriptivasController : ControllerBase
    {
       
        private readonly IAdmDescriptivasService _service;

        public AdmDescriptivasController(IAdmDescriptivasService service)
        {

            _service = service;


        }

       
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetTreeDescriptiva()
        {
                var result = await _service.GetTreeDecriptiva();
                return Ok(result);
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
        public async Task<IActionResult> GetAllByTitulo(FilterPreTituloDto dto)
        {
            var result = await _service.GetByTitulo(dto.TituloId);
            return Ok(result);
        }
            
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetSelectDescriptiva(FilterPreTituloDto dto)
        {
            var result = await _service.GetSelectDescriptiva(dto.TituloId);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByCodigoTitulo(FilterPreTituloDto dto)
        {
            var result = await _service.GetByCodigoTitulo(dto.Codigo);
            return Ok(result);
        }

        
   

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmDescriptivasUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmDescriptivasUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmDescriptivaDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
