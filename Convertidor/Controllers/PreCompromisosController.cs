using Convertidor.Dtos.Adm;
using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Presupuesto;
using Convertidor.Enum;
using Convertidor.Services.Sis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PreCompromisosController : ControllerBase
    {
       
        private readonly IPreCompromisosService _service;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAuthModelUserServices _authModelUserServices;

        public PreCompromisosController(IPreCompromisosService service,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IAuthModelUserServices authModelUserServices)
        {
            _service = service;
            _sisUsuarioRepository = sisUsuarioRepository;
            _authModelUserServices = authModelUserServices;
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
        public async Task<IActionResult> GetByPresupuesto(PreCompromisosFilterDto filter)
        {
            var result = await _service.GetByPresupuesto(filter);
            return Ok(result);
        }
        
       

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(PreCompromisosUpdateDto dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PreCompromisosUpdateDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CrearCompromisoDesdeSolicitud(FilterCrearCompromisoDesdeSolictud filter)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmSolicitudes, ActionType.Aprobar);
            if (userValid.IsValid == false)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = userValid.Message;
                return Ok(result);
            }
            
            result = await _service.CrearCompromisoDesdeSolicitud(filter.CodigoSolicitud);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AnularDesdeSolicitud(FilterAnularCompromisoDesdeSolictud filter)
        {
            
            ResultDto<bool> result = new ResultDto<bool>(false);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmSolicitudes, ActionType.Anular);
            if (userValid.IsValid == false)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = userValid.Message;
                return Ok(result);
            }
            result = await _service.AnularDesdeSolicitud(filter.CodigoSolicitud);
            return Ok(result);
        }
        
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(PreCompromisosDeleteDto dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);

        }





    }
}
