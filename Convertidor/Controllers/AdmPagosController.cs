using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Pagos;
using Convertidor.Services.Adm;
using Convertidor.Services.Adm.Pagos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmPagosController : ControllerBase
    {
       
        private readonly IAdmPagosService _service;

        public AdmPagosController(IAdmPagosService service)
        {

            _service = service;


        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByLote(AdmChequeFilterDto dto)
        {
            var result = await _service.GetByLote(dto);
            return Ok(result);
        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PagoCreateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateMonto(PagoUpdateMontoDto dto)
        {
            var result = await _service.UpdateMonto(dto);
            return Ok(result);
        }
      
     

    }
}
