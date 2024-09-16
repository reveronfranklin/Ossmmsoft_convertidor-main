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
    public class AdmDetalleSolicitudController : ControllerBase
    {
       
        private readonly IAdmDetalleSolicitudService _service;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ISisUsuarioServices _sisUsuarioServices;
        private readonly IAuthModelUserServices _authModelUserServices;

        public AdmDetalleSolicitudController(IAdmDetalleSolicitudService service,
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
            ResultDto<List<AdmDetalleSolicitudResponseDto>> result = new ResultDto<List<AdmDetalleSolicitudResponseDto>>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmDetalleSolicitudes, ActionType.View);
            if (userValid.IsValid == false)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = userValid.Message;
                return Ok(result);
            }
            result = await _service.GetAll();
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async  Task<IActionResult> GetByCodigoSolicitud(AdmSolicitudesFilterDto filter)
        {
            ResultDto<List<AdmDetalleSolicitudResponseDto>> result = new ResultDto<List<AdmDetalleSolicitudResponseDto>>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmDetalleSolicitudes, ActionType.View);
            userValid.IsValid = true;
            if (userValid.IsValid == false)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = userValid.Message;
                return Ok(result);
            }
            result =await  _service.GetByCodigoSolicitud(filter);
            return Ok(result);
        }



        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmDetalleSolicitudUpdateDto dto)
        {
            ResultDto<AdmDetalleSolicitudResponseDto> result = new ResultDto<AdmDetalleSolicitudResponseDto>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmDetalleSolicitudes, ActionType.Change);
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
        public async Task<IActionResult> Create(AdmDetalleSolicitudUpdateDto dto)
        {
            ResultDto<AdmDetalleSolicitudResponseDto> result = new ResultDto<AdmDetalleSolicitudResponseDto>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmDetalleSolicitudes, ActionType.Add);
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
        public async Task<IActionResult> Delete(AdmDetalleSolicitudDeleteDto dto)
        {
            ResultDto<AdmDetalleSolicitudDeleteDto> result = new ResultDto<AdmDetalleSolicitudDeleteDto>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmDetalleSolicitudes, ActionType.Delete);
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
