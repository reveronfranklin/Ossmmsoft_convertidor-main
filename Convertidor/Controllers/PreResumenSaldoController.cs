using System.Text;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;
using Ganss.Excel;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreResumenSaldoController : ControllerBase
    {
       
        private readonly IPreResumenSaldoServices _service;
       

        public PreResumenSaldoController(IPreResumenSaldoServices service)
        {

            _service = service;

        }



       
       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  GetAllByPresupuesto(FilterResumenSaldo filter)
        {
            var result = await _service.GetAllByPresupuesto(filter.CodigoPresupuesto);
            return Ok(result);
        }

    

        
  

    }
}
