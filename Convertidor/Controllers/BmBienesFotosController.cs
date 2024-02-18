
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Data.Entities.Bm;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BmBienesFotosController : ControllerBase
    {
       
        private readonly IBmBienesFotoService _service;

        public BmBienesFotosController(IBmBienesFotoService service)
        {

            _service = service;
           
        }
        

        [HttpGet]
        [Route("[action]")]

        public async Task<ActionResult> ProcesarFotos() 
        {
            var result = await _service.CopiarArchivos();
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByNumeroPlaca(BmBienesFotoFilterDto dto)
        {
            var result = await _service.GetByNumeroPlaca(dto.NumeroPlaca);
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(BmBienesFotoUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }
        [HttpPost]
        //[Route("[action]/{id:int}")] 
        [Route("[action]/{id}")]
        public async Task<IActionResult> AddImage(int id, [FromForm]List<IFormFile> files)
        {
            var result = await _service.AddImage(id,files);
            return Ok(result);
        }   
        
        [HttpPost]
        [Route("[action]")] 
        public async Task<IActionResult> AddImageModel([FromForm] BmBienesimageUpdateDto files)
        {
            var result = await _service.AddImageModel(files);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(BmBienesFotoUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(BmBienesFotoDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
