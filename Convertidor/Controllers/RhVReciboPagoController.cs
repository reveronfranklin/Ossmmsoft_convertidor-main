using Convertidor.Services.Rh.Report.Example;
using Convertidor.Services.Sis;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhVReciboPagoController : ControllerBase
    {

        private readonly IRhVReciboPagoService _service;
        private readonly IReportReciboPagoService _reciboPagoService;
        private readonly IEmailServices _emailService;

        public RhVReciboPagoController(IRhVReciboPagoService service,IReportReciboPagoService reciboPagoService,IEmailServices emailService)
        {

            _service = service;
            _reciboPagoService = reciboPagoService;
            _emailService = emailService;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByFilter(FilterReciboPagoDto dto)
        {
            var result = await _service.GetByFilter(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateReportReciboPago(FilterReciboPagoDto dto)
        {
            await _service.CreateReportReciboPago(dto.CodigoPeriodo, dto.CodigoTipoNomina, dto.CodigoPersona);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GeneratePdf(FilterRepoteNomina filter)
        {
            var filePath = $"ReporteReciboPago-{filter.CodigoTipoNomina}-{filter.CodigoPeriodo}.pdf";
            await _reciboPagoService.GeneratePdf(filter);

            if (filter.SendEmail)
            {
                EmailDto request = new EmailDto();
                request.FilePath = filePath;
                //request.To = "jhonny.cordoba@gmail.com";
                request.To = "reveron.franklin@moore.com.ve.com";
                request.Subject="Probando envio Recibo de pago";
              
                request.Content = "Esto es una prueba de envio del recibo de pago";
                _emailService.SendEmail(request);
            }
            return Ok(filePath);
        }

    }
        


    
}
