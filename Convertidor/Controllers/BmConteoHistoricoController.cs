
using Microsoft.AspNetCore.Mvc;
using Convertidor.Dtos.Bm;
using Convertidor.Data.Interfaces.Bm;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmConteoHistoricoController : ControllerBase
    {
       
        private readonly IBmConteoHistoricoService _service;

        public BmConteoHistoricoController(IBmConteoHistoricoService service)
        {

            _service = service;
           
        }
        

        [HttpGet]
        [Route("[action]")]

        public async Task<ActionResult> GetAll() 
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateReportConteoHistorico(BmConteoFilterDto dto)
        {

            ResultDto<string> result = new ResultDto<string>("");
            await _service.CreateReportConteoHistorico(dto.CodigoBmConteo);
            result.Data = $"{dto.CodigoBmConteo}.pdf";
            result.IsValid = true;
            result.LinkData = result.Data;
            result.Message = "";
            
            
            return Ok(result);
        }


     


    }
}
