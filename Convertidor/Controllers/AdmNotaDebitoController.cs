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
    public class AdmNotaDebitoController : ControllerBase
    {
       
        private readonly IAdmNotaDebitoPagoElectronicService _service;

        public AdmNotaDebitoController(IAdmNotaDebitoPagoElectronicService service)
        {

            _service = service;


        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByLote(PagoFilterDto dto)
        {
            var result = await _service.GetNotaDebitoPagoElectronicoByLote(dto.CodigoLote);
            return Ok(result);
        }

  
     
     

    }
}
