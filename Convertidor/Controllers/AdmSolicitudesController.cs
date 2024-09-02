using Microsoft.AspNetCore.Mvc;

// HTML to PDF
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Sis;
using Convertidor.Enum;
using Convertidor.Services.Adm;
using Convertidor.Services.Sis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdmSolicitudesController : ControllerBase
    {
       
        private readonly IAdmSolicitudesService _service;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ISisUsuarioServices _sisUsuarioServices;
        private readonly IAuthModelUserServices _authModelUserServices;

        public AdmSolicitudesController(IAdmSolicitudesService service,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        ISisUsuarioServices sisUsuarioServices,
                                        IAuthModelUserServices authModelUserServices)
        {
            _service = service;
            _sisUsuarioRepository = sisUsuarioRepository;
            _sisUsuarioServices = sisUsuarioServices;
            _authModelUserServices = authModelUserServices;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByPresupuesto(AdmSolicitudesFilterDto filter)
        {
            ResultDto<List<AdmSolicitudesResponseDto>> result = new ResultDto<List<AdmSolicitudesResponseDto>>(null);
           var conectado = await _sisUsuarioRepository.GetConectado();

            Console.WriteLine(conectado);
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmSolicitudes, ActionType.View);
            if (userValid.IsValid == false)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = $"{userValid.Message} Usuario:{conectado.Usuario} {conectado.Empresa}";
                return Ok(result);
            }
            
            result = await _service.GetByPresupuesto(filter);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ValidateUser(ValidateUserDto filter)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            var conectado = await _sisUsuarioRepository.GetConectado();

            //conectado.Usuario = 530;
            var userValid = await _authModelUserServices.ValidUserModel(filter.Usuario, filter.Model,filter.Action);
            if (userValid.IsValid == false)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = userValid.Message;
                return Ok(result);
            }
            
            result = userValid;
            return Ok(result);
        }
        
        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetByPresupuestoPendiente(AdmSolicitudesFilterDto filter)
        {
            ResultDto<List<AdmSolicitudesResponseDto>> result = new ResultDto<List<AdmSolicitudesResponseDto>>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmSolicitudes, ActionType.View);
            if (userValid.IsValid == false)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = userValid.Message;
                return Ok(result);
            }
            result = await _service.GetByPresupuestoPendiente(filter);
            return Ok(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(AdmSolicitudesUpdateDto dto)
        {
            ResultDto<AdmSolicitudesResponseDto> result = new ResultDto<AdmSolicitudesResponseDto>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmSolicitudes, ActionType.Change);
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
        public async Task<IActionResult> Create(AdmSolicitudesUpdateDto dto)
        {

            ResultDto<AdmSolicitudesResponseDto> result = new ResultDto<AdmSolicitudesResponseDto>(null);
           var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmSolicitudes, ActionType.Add);
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
        public async Task<IActionResult> Delete(AdmSolicitudesDeleteDto dto)
        {
            ResultDto<AdmSolicitudesDeleteDto> result = new ResultDto<AdmSolicitudesDeleteDto>(null);
            var conectado = await _sisUsuarioRepository.GetConectado();
            var userValid = await _authModelUserServices.ValidUserModel(conectado.Usuario, AdmModels.AdmModelsName.AdmSolicitudes, ActionType.Delete);
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
