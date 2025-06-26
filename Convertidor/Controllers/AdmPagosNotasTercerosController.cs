using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Pagos;
using Convertidor.Services.Adm;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmPagosNotasTercerosController : ControllerBase
    {
       
        private readonly IAdmPagosNotasTercerosService _service;

        public AdmPagosNotasTercerosController(IAdmPagosNotasTercerosService service)
        {

            _service = service;


        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByLotePago(PagoTerceroFilterDto filterDto)
        {
            var result = await _service.GetByLotePago(filterDto);
            return Ok(result);
        }
       
     
     

    }
}
