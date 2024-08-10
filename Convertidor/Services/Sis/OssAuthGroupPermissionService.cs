using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;


namespace Convertidor.Services.Sis
{
    
	public class OssAuthGroupPermissionService: IOssAuthGroupPermissionService
    {
        private readonly IOssAuthGroupPermissionsRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IOssAuthGroupService _ossAuthGroupService;
        private readonly IOssAuthPermissionsService _ossAuthPermissionsService;


        public OssAuthGroupPermissionService(IOssAuthGroupPermissionsRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IOssAuthGroupService  ossAuthGroupService,
                                      IOssAuthPermissionsService ossAuthPermissionsService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _ossAuthGroupService = ossAuthGroupService;
            _ossAuthPermissionsService = ossAuthPermissionsService;
        }
        
       
        public  async Task<AuthGroupPermisionResponseDto> MapDto(AUTH_GROUP_PERMISSIONS entity)
        {
            AuthGroupPermisionResponseDto itemResult = new AuthGroupPermisionResponseDto();
            
            try
            {
                if (entity == null)
                {
                    return itemResult;
                }
                itemResult.Id = entity.ID;
             
                itemResult.GroupId = entity.GROUP_ID;
                itemResult.GroupName = "";
                AuthGroupFilterDto filter = new AuthGroupFilterDto();
                filter.Id = itemResult.GroupId;
                var group = await _ossAuthGroupService.GetById(filter);
                if (group.Data != null)
                {
                    itemResult.GroupName = group.Data.Name;
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

        public async Task< List<AuthGroupPermisionResponseDto>> MapList(List<AUTH_GROUP_PERMISSIONS> dtos)
        {
            List<AuthGroupPermisionResponseDto> result = new List<AuthGroupPermisionResponseDto>();
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
      
      
     
        public async Task<ResultDto<AuthGroupPermisionResponseDto>> Create(AuthGroupPermissionUpdateDto dto)
        {

            ResultDto<AuthGroupPermisionResponseDto> result = new ResultDto<AuthGroupPermisionResponseDto>(null);
            try
            {

                var groupPermission = await _repository.GetByGroupPermission(dto.GroupId, dto.PermissionId);
                if (groupPermission != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe este permiso en este grupo";
                    return result;
                }
                
                AuthGroupFilterDto filter = new AuthGroupFilterDto();
                filter.Id = dto.GroupId;
                var group = await _ossAuthGroupService.GetById(filter);
                if (group.Data == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Grupo  no existe";
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
               
                
                AUTH_GROUP_PERMISSIONS entity = new AUTH_GROUP_PERMISSIONS();
            
                entity.PERMISSION_ID = dto.PermissionId;
                entity.GROUP_ID = dto.GroupId;
                
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
 
        public async Task<ResultDto<AuthGroupPermissionDeleteDto>> Delete(AuthGroupPermissionDeleteDto dto)
        {

            ResultDto<AuthGroupPermissionDeleteDto> result = new ResultDto<AuthGroupPermissionDeleteDto>(null);
            try
            {

                var group = await _repository.GetByID(dto.Id);
                if (group == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Grupo/Permiso No existe";
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

        public async Task<ResultDto<AuthGroupPermisionResponseDto>> GetById(AuthGroupPermissionFilterDto dto)
        { 
            ResultDto<AuthGroupPermisionResponseDto> result = new ResultDto<AuthGroupPermisionResponseDto>(null);
            try
            {

                var groupPermission = await _repository.GetByID(dto.Id);
                if (groupPermission == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Group/Permission no existe";
                    return result;
                }
                
                var resultDto =  await MapDto(groupPermission);
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
        
        public async Task<ResultDto<List<AuthGroupPermisionResponseDto>>> GetByGroup(AuthGroupPermissionFilterDto dto)
        { 
            ResultDto<List<AuthGroupPermisionResponseDto>> result = new ResultDto<List<AuthGroupPermisionResponseDto>>(null);
            try
            {

                var groupPermission = await _repository.GetByGroup(dto.GroupId);
                if (groupPermission == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existen Permisos para este Grupo";
                    return result;
                }
                
                var resultDto =  await MapList(groupPermission);
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

        public async Task<ResultDto<List<AuthGroupPermisionResponseDto>>> GetAll()
        {
            ResultDto<List<AuthGroupPermisionResponseDto>> result = new ResultDto<List<AuthGroupPermisionResponseDto>>(null);
            try
            {

                var variables = await _repository.GetALL();
                if (variables == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapList(variables);
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

