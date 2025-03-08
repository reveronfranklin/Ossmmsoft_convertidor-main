using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Bm.Mobil;
using Convertidor.Services.Bm;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class Bm1Controller : ControllerBase
    {
       
        private readonly IBM_V_BM1Service _service;

        public Bm1Controller(IBM_V_BM1Service service)
        {

            _service = service;


        }

       
     

        [HttpGet]
        [Route("[action]")] 
        public async Task<IActionResult> GetAll()
        {
            
            var result = await _service.GetAll(DateTime.Now.AddYears(-20), DateTime.Now);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")] 
        public async Task<IActionResult> GetListICP()
        {
            var result = await _service.GetICP();
            return Ok(result);
        }
        
        [HttpGet]
        [Route("[action]")] 
        public async Task<IActionResult> GetPlacas()
        {
            var result = await _service.GetPlacas();
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")] 
        public async Task<IActionResult> GetByListIcp(Bm1Filter filter)
        {
            var result = await _service.GetByListIcp(filter);
            return Ok(result);
        }
        
        
        
        [HttpPost]
        [Route("[action]")] 
        public async Task<IActionResult>  GetProductMobil(ProductFilterDto filter)
        {
            var result = await _service.GetProductMobil(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")] 
        public async Task<IActionResult>  GetProductMobilById(ProductFilterDto filter)
        {
            var result = await _service.GetProductMobilById(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")] 
        public async Task<IActionResult>  UpdateProductMobil(ProductResponse dto)
        {
            ProductFilterDto filter = new ProductFilterDto();
            filter.CodigoBien = dto.Id;
            var result = await _service.GetProductMobilById(filter);
            result.Descripcion = dto.Descripcion;
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")] 
        public async Task<IActionResult>  CreateProductMobil(ProductResponse dto)
        {
            ProductFilterDto filter = new ProductFilterDto();
            filter.CodigoBien = dto.Id;
            var result = await _service.GetProductMobilById(filter);
            return Ok(result);
        }
      
    
        
    }
}
