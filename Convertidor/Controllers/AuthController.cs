using AppService.Api.Utility;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Convertidor.Data.Entities;
using Convertidor.Data.Interfaces;
using Convertidor.Services;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using IronPdf;
using Convertidor.Dtos;
using Convertidor.Services.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Sis;
using Convertidor.Dtos.Sis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SisUsuariosController : ControllerBase
    {

        private readonly ISisUsuarioServices _service;



        public SisUsuariosController(ISisUsuarioServices service)
        {

            _service = service;



        }

   

       
       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>  Login(LoginDto dto)
        {
            var result = await _service.Login(dto);
            return Ok(result);
        }

      


     

      

    }
}
