using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.PreOrdenPago;
using Convertidor.Services.Adm;
using Convertidor.Services.Destino.ADM;
using NATS.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmPreOrdenPagoController : ControllerBase
    {
       
        private readonly IAdmPreOrdenPagoService _service;

        public AdmPreOrdenPagoController(IAdmPreOrdenPagoService service)
        {

            _service = service;


        }


      
        
      

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmPreOdenPagoCreateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateLote(List<AdmPreOdenPagoCreateDto> dto)
        {
            var result = await _service.CreateLote(dto);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(AdmPreOrdenPagoFilterDto dto)
        {
            var result = await _service.GetAll(dto);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAll()
        {
            var result = await _service.DeleteAll();
            return Ok(result);
        }

      
        
        
       
        
        

    }
}
