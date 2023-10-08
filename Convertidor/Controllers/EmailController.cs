using System.Net;
using Convertidor.Dtos;
using Convertidor.Services.Sis;
using Microsoft.AspNetCore.Mvc;

namespace AppService.Api.Controllers
{


    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
      
        private readonly IEmailServices _service;
    
        public EmailController(IEmailServices service)
        {
            _service = service;
          
        }


       
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SendEmail(EmailDto request)
        {
           
            

            _service.SendEmail(request);

            return Ok();
        }

       
         
 

    }
}