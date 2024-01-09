using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmTitulosController : ControllerBase
    {
       
        private readonly IAdmTituloService _service;

        public AdmTitulosController(IAdmTituloService service)
        {

            _service = service;


        }

       
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetTreeTitulos()
        {
                var result = await _service.GetTreeTitulos();
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
        public async Task<IActionResult> Update(AdmTitulosUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmTitulosUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmTitulosDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }





    }
}
