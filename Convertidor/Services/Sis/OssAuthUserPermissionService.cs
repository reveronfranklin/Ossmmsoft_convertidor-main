using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;


namespace Convertidor.Services.Sis
{
    
	public class OssAuthUserPermissionService: IOssAuthUserPermissionService
    {
        private readonly IOssAuthUserPermissionsRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IOssAuthPermissionsService _ossAuthPermissionsService;


        public OssAuthUserPermissionService(IOssAuthUserPermissionsRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IOssAuthPermissionsService ossAuthPermissionsService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _ossAuthPermissionsService = ossAuthPermissionsService;
        }
        
       
        public  async Task<AuthUserPermisionResponseDto> MapDto(AUTH_USER_USER_PERMISSIONS entity)
        {
            AuthUserPermisionResponseDto itemResult = new AuthUserPermisionResponseDto();
            
            try
            {
                if (entity == null)
                {
                    return itemResult;
                }
                itemResult.Id = entity.ID;
             
                itemResult.UserId = entity.USER_ID;
                itemResult.UserName = "";
                var user = await _sisUsuarioRepository.GetByCodigo(entity.USER_ID);
                if (user != null)
                {
                    itemResult.UserName = user.USUARIO;
                    itemResult.Login = user.LOGIN;
                }
                itemResult.PermisionId = entity.PERMISSION_ID;
                itemResult.DescriptionPermision = "";
                AuthPermissionFilterDto filterPermission = new AuthPermissionFilterDto();
                filterPermission.Id = itemResult.PermisionId;
                var permission = await _ossAuthPermissionsService.GetById(filterPermission);
                if (permission.Data != null)
                {
                    itemResult.DescriptionPermision = permission.Data.SearchText;
                }
            
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(entity);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<AuthUserPermisionResponseDto>> MapList(List<AUTH_USER_USER_PERMISSIONS> dtos)
        {
            List<AuthUserPermisionResponseDto> result = new List<AuthUserPermisionResponseDto>();
            if (dtos.Count > 0)
            {
                foreach (var item in dtos)
                {
                    if (item == null)
                    {
                        var detener = "";
                    }
                    else
                    {
                        var itemResult =  await MapDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
      
     
        public async Task<ResultDto<AuthUserPermisionResponseDto>> Create(AuthUserPermisionUpdateDto dto)
        {

            ResultDto<AuthUserPermisionResponseDto> result = new ResultDto<AuthUserPermisionResponseDto>(null);
            try
            {

                var userPermission = await _repository.GetByUserPermision(dto.UserId, dto.PermissionId);
                if (userPermission != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe este permiso en este usuario";
                    return result;
                }
                
                var user = await _sisUsuarioRepository.GetByCodigo(dto.UserId);
                if (user == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario  no existe";
                    return result;
                }
               
                AuthPermissionFilterDto filterPermission = new AuthPermissionFilterDto();
                filterPermission.Id = dto.PermissionId;
                var permission = await _ossAuthPermissionsService.GetById(filterPermission);
                if (permission.Data == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Permiso  no existe";
                    return result;
                }
               
                
                AUTH_USER_USER_PERMISSIONS entity = new AUTH_USER_USER_PERMISSIONS();
            
                entity.PERMISSION_ID = dto.PermissionId;
                entity.USER_ID = dto.UserId;
                
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDto(created.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";

                }
                else
                {

                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                }

                return result;  

              



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }
 
        public async Task<ResultDto<AuthUserPermisionDeleteDto>> Delete(AuthUserPermisionDeleteDto dto)
        {

            ResultDto<AuthUserPermisionDeleteDto> result = new ResultDto<AuthUserPermisionDeleteDto>(null);
            try
            {

                var userPermissions = await _repository.GetByID(dto.Id);
                if (userPermissions == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario/Permiso No existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.Id);

                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<AuthUserPermisionResponseDto>> GetById(AuthUserPermisionFilterDto dto)
        { 
            ResultDto<AuthUserPermisionResponseDto> result = new ResultDto<AuthUserPermisionResponseDto>(null);
            try
            {

                var userPermission = await _repository.GetByID(dto.Id);
                if (userPermission == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Group/Permission no existe";
                    return result;
                }
                
                var resultDto =  await MapDto(userPermission);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }
        
        public async Task<ResultDto<List<AuthUserPermisionResponseDto>>> GetByUser(AuthUserPermisionFilterDto dto)
        { 
            ResultDto<List<AuthUserPermisionResponseDto>> result = new ResultDto<List<AuthUserPermisionResponseDto>>(null);
            try
            {

                var userPermission = await _repository.GetByUser(dto.UserId);
                if (userPermission == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existen Permisos para este Usuario";
                    return result;
                }
                
                var resultDto =  await MapList(userPermission);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ResultDto<List<AuthUserPermisionResponseDto>>> GetAll()
        {
            ResultDto<List<AuthUserPermisionResponseDto>> result = new ResultDto<List<AuthUserPermisionResponseDto>>(null);
            try
            {

                var userPermision = await _repository.GetALL();
                if (userPermision == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapList(userPermision);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
           
        }

        
    }
}

