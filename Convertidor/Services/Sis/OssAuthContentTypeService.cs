using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;


namespace Convertidor.Services.Sis
{
    
	public class OssAuthContentTypeService: IOssAuthContentTypeService
    {
        private readonly IOssAuthContentTypeRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public OssAuthContentTypeService(IOssAuthContentTypeRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
        
       
        public  async Task<AuthContentTypeResponseDto> MapDto(AUTH_CONTENT_TYPE entity)
        {
            AuthContentTypeResponseDto itemResult = new AuthContentTypeResponseDto();
            
            try
            {
                if (entity == null)
                {
                    return itemResult;
                }
                itemResult.Id = entity.ID;
             
                itemResult.AppLabel = entity.APP_LABEL;
                itemResult.Model = entity.MODEL;
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(entity);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<AuthContentTypeResponseDto>> MapList(List<AUTH_CONTENT_TYPE> dtos)
        {
            List<AuthContentTypeResponseDto> result = new List<AuthContentTypeResponseDto>();
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
      
      
        public async Task<ResultDto<AuthContentTypeResponseDto>> Update(AuthContentTypeUpdateDto dto)
        {

            ResultDto<AuthContentTypeResponseDto> result = new ResultDto<AuthContentTypeResponseDto>(null);
            try
            {
                var contentType = await _repository.GetByID(dto.Id);
                if (contentType == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Content Type no existe";
                    return result;
                }
              
                
                if (String.IsNullOrEmpty(dto.Model) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modelo Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.AppLabel) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "App Invalida";
                    return result;
                }
                

                contentType.MODEL = dto.Model;
                contentType.APP_LABEL = dto.AppLabel;
               
                contentType.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                contentType.CODIGO_EMPRESA = conectado.Empresa;
                contentType.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(contentType);
                var resultDto = await  MapDto(contentType);
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

        public async Task<ResultDto<AuthContentTypeResponseDto>> Create(AuthContentTypeUpdateDto dto)
        {

            ResultDto<AuthContentTypeResponseDto> result = new ResultDto<AuthContentTypeResponseDto>(null);
            try
            {
               
                if (String.IsNullOrEmpty(dto.Model) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modelo Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.AppLabel) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "App Invalida";
                    return result;
                }
               
                
                AUTH_CONTENT_TYPE entity = new AUTH_CONTENT_TYPE();
            
                entity.MODEL = dto.Model;
                entity.APP_LABEL = dto.AppLabel;
                
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
 
        public async Task<ResultDto<AuthContentTypeDeleteDto>> Delete(AuthContentTypeDeleteDto dto)
        {

            ResultDto<AuthContentTypeDeleteDto> result = new ResultDto<AuthContentTypeDeleteDto>(null);
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

        public async Task<ResultDto<AuthContentTypeResponseDto>> GetById(AuthContentTypeFilterDto dto)
        { 
            ResultDto<AuthContentTypeResponseDto> result = new ResultDto<AuthContentTypeResponseDto>(null);
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
        
        public async Task<ResultDto<List<AuthContentTypeResponseDto>>> GetAll()
        {
            ResultDto<List<AuthContentTypeResponseDto>> result = new ResultDto<List<AuthContentTypeResponseDto>>(null);
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

