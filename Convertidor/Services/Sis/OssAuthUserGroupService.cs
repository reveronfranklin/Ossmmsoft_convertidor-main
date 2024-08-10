using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;


namespace Convertidor.Services.Sis
{
    
	public class OssAuthUserGroupService: IOssAuthUserGroupService
    {
        private readonly IOssAuthUserGroupRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IOssAuthGroupRepository _ossAuthGroupRepository;


        public OssAuthUserGroupService(IOssAuthUserGroupRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IOssAuthGroupRepository ossAuthGroupRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _ossAuthGroupRepository = ossAuthGroupRepository;
        }
        
       
        public  async Task<AuthUserGroupResponseDto> MapDto(AUTH_USER_GROUPS entity)
        {
            AuthUserGroupResponseDto itemResult = new AuthUserGroupResponseDto();
            
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
                itemResult.GroupId = entity.GROUP_ID;
                itemResult.GroupName = "";
                var group = await _ossAuthGroupRepository.GetByID(itemResult.GroupId);
                if (group != null)
                {
                    itemResult.GroupName = group.NAME;
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

        public async Task< List<AuthUserGroupResponseDto>> MapList(List<AUTH_USER_GROUPS> dtos)
        {
            List<AuthUserGroupResponseDto> result = new List<AuthUserGroupResponseDto>();
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
        
        public async Task<ResultDto<AuthUserGroupResponseDto>> Create(AuthUserGroupUpdateDto dto)
        {

            ResultDto<AuthUserGroupResponseDto> result = new ResultDto<AuthUserGroupResponseDto>(null);
            try
            {

                var userGroup = await _repository.GetByUserGroup(dto.UserId, dto.GroupId);
                if (userGroup != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe este permiso en este usuario/grupo";
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
             
                var group = await _ossAuthGroupRepository.GetByID(dto.GroupId);
                if (group == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Grupo  no existe";
                    return result;
                }
               
                
                AUTH_USER_GROUPS entity = new AUTH_USER_GROUPS();
            
                entity.GROUP_ID = dto.GroupId;
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
 
        public async Task<ResultDto<AuthUserGroupDeleteDto>> Delete(AuthUserGroupDeleteDto dto)
        {

            ResultDto<AuthUserGroupDeleteDto> result = new ResultDto<AuthUserGroupDeleteDto>(null);
            try
            {

                var userGroup = await _repository.GetByID(dto.Id);
                if (userGroup == null)
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

        public async Task<ResultDto<AuthUserGroupResponseDto>> GetById(AuthUserGroupFilterDto dto)
        { 
            ResultDto<AuthUserGroupResponseDto> result = new ResultDto<AuthUserGroupResponseDto>(null);
            try
            {

                var userGroup = await _repository.GetByID(dto.Id);
                if (userGroup == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Group/Permission no existe";
                    return result;
                }
                
                var resultDto =  await MapDto(userGroup);
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
        
        public async Task<ResultDto<List<AuthUserGroupResponseDto>>> GetByUser(AuthUserGroupFilterDto dto)
        { 
            ResultDto<List<AuthUserGroupResponseDto>> result = new ResultDto<List<AuthUserGroupResponseDto>>(null);
            try
            {

                var userPermission = await _repository.GetByUser(dto.UserId);
                if (userPermission == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existen Grupos para este Usuario";
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

        public async Task<ResultDto<List<AuthUserGroupResponseDto>>> GetAll()
        {
            ResultDto<List<AuthUserGroupResponseDto>> result = new ResultDto<List<AuthUserGroupResponseDto>>(null);
            try
            {

                var userGroups = await _repository.GetALL();
                if (userGroups == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapList(userGroups);
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

