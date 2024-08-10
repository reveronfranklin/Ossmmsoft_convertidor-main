using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;


namespace Convertidor.Services.Sis
{
    
	public class OssAuthGroupService: IOssAuthGroupService
    {
        private readonly IOssAuthGroupRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public OssAuthGroupService(IOssAuthGroupRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
        
       
        public  async Task<AuthGroupResponseDto> MapDto(AUTH_GROUP entity)
        {
            AuthGroupResponseDto itemResult = new AuthGroupResponseDto();
            
            try
            {
                if (entity == null)
                {
                    return itemResult;
                }
                itemResult.Id = entity.ID;
             
                itemResult.Name = entity.NAME;
            
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(entity);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<AuthGroupResponseDto>> MapList(List<AUTH_GROUP> dtos)
        {
            List<AuthGroupResponseDto> result = new List<AuthGroupResponseDto>();
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
      
      
        public async Task<ResultDto<AuthGroupResponseDto>> Update(AuthGroupUpdateDto dto)
        {

            ResultDto<AuthGroupResponseDto> result = new ResultDto<AuthGroupResponseDto>(null);
            try
            {
                var group = await _repository.GetByID(dto.Id);
                if (group == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Grupo no existe";
                    return result;
                }
              
                
                if (String.IsNullOrEmpty(dto.Name) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Name Invalido";
                    return result;
                }
               
                

                group.NAME = dto.Name;
              
               
                group.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                group.CODIGO_EMPRESA = conectado.Empresa;
                group.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(group);
                var resultDto = await  MapDto(group);
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

        public async Task<ResultDto<AuthGroupResponseDto>> Create(AuthGroupUpdateDto dto)
        {

            ResultDto<AuthGroupResponseDto> result = new ResultDto<AuthGroupResponseDto>(null);
            try
            {
               
                if (String.IsNullOrEmpty(dto.Name) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Invalido";
                    return result;
                }
               
                
                AUTH_GROUP entity = new AUTH_GROUP();
            
                entity.NAME = dto.Name;
         
                
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
 
        public async Task<ResultDto<AuthGroupDeleteDto>> Delete(AuthGroupDeleteDto dto)
        {

            ResultDto<AuthGroupDeleteDto> result = new ResultDto<AuthGroupDeleteDto>(null);
            try
            {

                var group = await _repository.GetByID(dto.Id);
                if (group == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Grupo No existe";
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

        public async Task<ResultDto<AuthGroupResponseDto>> GetById(AuthGroupFilterDto dto)
        { 
            ResultDto<AuthGroupResponseDto> result = new ResultDto<AuthGroupResponseDto>(null);
            try
            {

                var grup = await _repository.GetByID(dto.Id);
                if (grup == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo no existe";
                    return result;
                }
                
                var resultDto =  await MapDto(grup);
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
        
        public async Task<ResultDto<List<AuthGroupResponseDto>>> GetAll()
        {
            ResultDto<List<AuthGroupResponseDto>> result = new ResultDto<List<AuthGroupResponseDto>>(null);
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

