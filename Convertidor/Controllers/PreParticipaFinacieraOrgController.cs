using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PreParticipaFinacieraOrgController : ControllerBase
    {

        private readonly IPreParticipaFinacieraOrgService _service;

        public PreParticipaFinacieraOrgController(IPreParticipaFinacieraOrgService service)
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
        public async Task<IActionResult> Update(PreParticipaFinacieraOrgUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PreParticipaFinacieraOrgUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(PreParticipaFinacieraOrgDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);


        }
    }
}
