using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Enum;
using Convertidor.Services.Adm;
using Convertidor.Services.Sis;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmPucSolicitudController : ControllerBase
    {
       
        private readonly IAdmPucSolicitudService _service;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ISisUsuarioServices _sisUsuarioServices;
        private readonly IAuthModelUserServices _authModelUserServices;

        public AdmPucSolicitudController(IAdmPucSolicitudService service,
                                         ISisUsuarioRepository sisUsuarioRepository,
                                         ISisUsuarioServices sisUsuarioServices,
                                         IAuthModelUserServices authModelUserServices)
        {
            _service = service;
            _sisUsuarioRepository = sisUsuarioRepository;
            _sisUsuarioServices = sisUsuarioServices;
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
        public async Task<IActionResult> GetByDetalleSolicitud(AdmPucSolicitudFilterDto filter)
        {
            
            ResultDto<List<AdmPucSolicitudResponseDto>> result = new ResultDto<List<AdmPucSolicitudResponseDto>>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmPucSolicitud, ActionType.View);
            if (userValid.IsValid == false)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = userValid.Message;
                return Ok(result);
            }
            result = await _service.GetByDetalleSolicitud(filter);
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmPucSolicitudUpdateDto dto)
        {
            ResultDto<AdmPucSolicitudResponseDto> result = new ResultDto<AdmPucSolicitudResponseDto>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmPucSolicitud, ActionType.Change);
            if (userValid.IsValid == false)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = userValid.Message;
                return Ok(result);
            }
            result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AdmPucSolicitudUpdateDto dto)
        {
            
            ResultDto<AdmPucSolicitudResponseDto> result = new ResultDto<AdmPucSolicitudResponseDto>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmPucSolicitud, ActionType.Add);
            if (userValid.IsValid == false)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = userValid.Message;
                return Ok(result);
            }
            result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(AdmPucSolicitudDeleteDto dto)
        {
            ResultDto<AdmPucSolicitudDeleteDto> result = new ResultDto<AdmPucSolicitudDeleteDto>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmPucSolicitud, ActionType.Delete);
            if (userValid.IsValid == false)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = userValid.Message;
                return Ok(result);
            }
            result = await _service.Delete(dto);
            return Ok(result);

        }

    }
}
