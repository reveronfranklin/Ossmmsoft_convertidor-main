using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;
using NuGet.Protocol;


namespace Convertidor.Services.Sis
{
    
	public class OssAuthPermissionsService: IOssAuthPermissionsService
    {
        private readonly IOssAuthPermissionRepository _repository;
        private readonly IOssAuthContentTypeRepository _ossAuthContentTypeRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public OssAuthPermissionsService(IOssAuthPermissionRepository repository,
                                        IOssAuthContentTypeRepository ossAuthContentTypeRepository,
                                      ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _ossAuthContentTypeRepository = ossAuthContentTypeRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
        
       
        public  async Task<AuthPermissionResponseDto> MapDto(AUTH_PERMISSION entity)
        {
            AuthPermissionResponseDto itemResult = new AuthPermissionResponseDto();
            
            try
            {
                if (entity == null)
                {
                    return itemResult;
                }
                itemResult.Id = entity.ID;
             
                itemResult.Codename = entity.CODENAME;
                itemResult.Name = entity.NAME;
                itemResult.ContentTypeId = entity.CONTENT_TYPE_ID;
                itemResult.Model = "";
                itemResult.AppLabel = "";
                var contentType = await _ossAuthContentTypeRepository.GetByID(itemResult.ContentTypeId);
                if (contentType != null)
                {
                    itemResult.Model = contentType.MODEL;
                    itemResult.AppLabel = contentType.APP_LABEL;
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

        public List<string> GetListCodeName()
        {
            List<string> result = new List<string> { "add", "change", "view","delete" };

            return result;

        }
        
        public async Task< List<AuthPermissionResponseDto>> MapList(List<AUTH_PERMISSION> dtos)
        {
            List<AuthPermissionResponseDto> result = new List<AuthPermissionResponseDto>();
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
      
      
        public async Task<ResultDto<AuthPermissionResponseDto>> Update(AuthPermissionUpdateDto dto)
        {

            ResultDto<AuthPermissionResponseDto> result = new ResultDto<AuthPermissionResponseDto>(null);
            try
            {
                var permission = await _repository.GetByID(dto.Id);
                if (permission == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Permission no existe";
                    return result;
                }
              
                
                var contentType = await _ossAuthContentTypeRepository.GetByID(dto.ContentTypeId);
                if (contentType == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Model  no existe";
                    return result;
                }
                
                if (String.IsNullOrEmpty(dto.Codename) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codename Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.Name) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Name Invalido";
                    return result;
                }

                var listCodeName = GetListCodeName();
                var codeName = listCodeName.Where(x => x == dto.Codename).FirstOrDefault();
                string concatenatedString = string.Join("-", listCodeName);
                if (String.IsNullOrEmpty(codeName) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Codename Invalido, Los valores Validos son:{concatenatedString} ";
                    return result;
                }
                

                permission.NAME = dto.Name;
                permission.CODENAME = dto.Codename;
                permission.CONTENT_TYPE_ID = dto.ContentTypeId;
                permission.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                permission.CODIGO_EMPRESA = conectado.Empresa;
                permission.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(permission);
                var resultDto = await  MapDto(permission);
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

        public async Task<ResultDto<AuthPermissionResponseDto>> Create(AuthPermissionUpdateDto dto)
        {

            ResultDto<AuthPermissionResponseDto> result = new ResultDto<AuthPermissionResponseDto>(null);
            try
            {
               
                var contentType = await _ossAuthContentTypeRepository.GetByID(dto.ContentTypeId);
                if (contentType == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Model  no existe";
                    return result;
                }
                
                if (String.IsNullOrEmpty(dto.Codename) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codename Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.Name) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Name Invalido";
                    return result;
                }
                var listCodeName = GetListCodeName();
                var codeName = listCodeName.Where(x => x == dto.Codename).FirstOrDefault();
                string concatenatedString = string.Join("-", listCodeName);
                if (String.IsNullOrEmpty(codeName) )
                {
                    result.Data = null;
                    result.IsValid = false;
                 
                    result.Message = $"Codename Invalido, Los valores Validos son:{concatenatedString} ";
                    return result;
                }
                
                AUTH_PERMISSION entity = new AUTH_PERMISSION();
            
                entity.CONTENT_TYPE_ID = dto.ContentTypeId;
                entity.NAME = dto.Name;
                entity.CODENAME = dto.Codename;
                
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
 
        public async Task<ResultDto<AuthPermissionDeleteDto>> Delete(AuthPermissionDeleteDto dto)
        {

            ResultDto<AuthPermissionDeleteDto> result = new ResultDto<AuthPermissionDeleteDto>(null);
            try
            {

                var contentType = await _repository.GetByID(dto.Id);
                if (contentType == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Variable No existe";
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

        public async Task<ResultDto<AuthPermissionResponseDto>> GetById(AuthPermissionFilterDto dto)
        { 
            ResultDto<AuthPermissionResponseDto> result = new ResultDto<AuthPermissionResponseDto>(null);
            try
            {

                var variable = await _repository.GetByID(dto.Id);
                if (variable == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo no existe";
                    return result;
                }
                
                var resultDto =  await MapDto(variable);
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
        
        public async Task<ResultDto<List<AuthPermissionResponseDto>>> GetAll()
        {
            ResultDto<List<AuthPermissionResponseDto>> result = new ResultDto<List<AuthPermissionResponseDto>>(null);
            try
            {

                var permissions = await _repository.GetALL();
                if (permissions == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapList(permissions);
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

