
using Microsoft.AspNetCore.Mvc;
using Convertidor.Services.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RhDocumentosAdjuntosController : ControllerBase
    {
       
        private readonly IRhDocumentosAdjuntosService _service;

        public RhDocumentosAdjuntosController(IRhDocumentosAdjuntosService service)
        {

            _service = service;
           
        }
        

      



        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByNumeroDocumento(RhDocumentoAdjuntoFilterDto dto)
        {
            var result = await _service.GetByNumeroDocumento(dto.CodigoDocumento);
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(RhDocumentosAdjuntosUpdateDto dto)
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
        //[Route("[action]/{id:int}")] 
        [Route("[action]/{id}")]
        public async Task<IActionResult> AddOneImage(int id, IFormFile file)
        {
            var result = await _service.AddOneImage(id,file);
            return Ok(result);
        }   
        
        [HttpPost]
        [Route("[action]")] 
        public async Task<IActionResult> AddImageModel([FromForm] RhDocumentosFilesUpdateDto files)
        {
            var result = await _service.AddImageModel(files);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(RhDocumentosAdjuntosUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(RhDocumentosAdjuntosDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

    }
}
