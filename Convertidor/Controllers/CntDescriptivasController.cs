using Convertidor.Dtos.Cnt;
using Convertidor.Services.Cnt;
using Microsoft.AspNetCore.Mvc;

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CntDescriptivasController : ControllerBase
    {
       
        private readonly ICntDescriptivasService _service;

        public CntDescriptivasController(ICntDescriptivasService service)
        {

            _service = service;


        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetTreeDescriptiva()
        {
            var result = await _service.GetTreeDescriptiva();
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByTitulo(FilterCntTituloDto dto)
        {
            var result = await _service.GetByTitulo(dto.TituloId);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByCodigoTitulo(FilterCntTituloDto dto)
        {
            var result = await _service.GetByCodigoTitulo(dto.Codigo);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(CntDescriptivasUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(CntDescriptivasUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(CntDescriptivasDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
