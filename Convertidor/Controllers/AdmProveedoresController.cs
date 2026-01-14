using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm;
using Convertidor.Dtos.Adm.Proveedores;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmProveedoresController : ControllerBase
    {
       
        private readonly IAdmProveedoresService _service;
        private readonly IADM_V_PAGAR_A_LA_OP_TERCEROSServices _proveedoresService;

        public AdmProveedoresController(IAdmProveedoresService service,IADM_V_PAGAR_A_LA_OP_TERCEROSServices proveedoresService)
        {
            _service = service;
            _proveedoresService = proveedoresService;
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
        public async Task<IActionResult> GetAll(AdmProveedoresFilterDto filter)
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllProveedoresContactos()
        {
            var result = await _proveedoresService.GetAll();
            return Ok(result);
        }
        
   
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByCodigo(AdmProveedorFilterDto dto)
        {
            var result = await _service.GetByCodigo(dto);
            return Ok(result);
        }
        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmProveedorUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmProveedorUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmProveedorDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }
          [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Inactivar(AdmProveedorDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }
         [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Activar(AdmProveedorDeleteDto dto)
        {
            var result = await _service.Activar(dto);
            return Ok(result);

        }

    }
}
