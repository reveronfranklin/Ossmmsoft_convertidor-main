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
    public class AdmOrdenesPagoPendientesPagoController : ControllerBase
    {
        private readonly IAdmOrdenesPagoPorPagarService _service;


        public AdmOrdenesPagoPendientesPagoController(IAdmOrdenesPagoPorPagarService service)
        {
            _service = service;
        }

       

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(AdmOrdenPagoPendientePagoFilterDto filter)
        {
            var result = await _service.GetAll(filter);
            return Ok(result);
        }


      



    }
}
