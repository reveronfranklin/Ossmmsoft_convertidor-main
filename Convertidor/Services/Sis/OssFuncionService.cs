using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;


namespace Convertidor.Services.Sis
{
    
	public class OssFuncionService: IOssFuncionService
    {

      
        private readonly IOssFuncionRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public OssFuncionService(IOssFuncionRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository)
		{
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
       
       
        public  async Task<OssFuncionResponseDto> MapOssFuncionDto(OssFuncion dtos)
        {
            OssFuncionResponseDto itemResult = new OssFuncionResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.Id = dtos.Id;
                itemResult.Funcion = dtos.Funcion;
                itemResult.Descripcion = dtos.Descripcion;
                itemResult.IdVariable = dtos.IdVariable;
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<OssFuncionResponseDto>> MapListOssFuncionDto(List<OssFuncion> dtos)
        {
            List<OssFuncionResponseDto> result = new List<OssFuncionResponseDto>();
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
                        var itemResult =  await MapOssFuncionDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
      
        public async Task<ResultDto<OssFuncionResponseDto?>> Update(OssFuncionUpdateDto dto)
        {

            ResultDto<OssFuncionResponseDto?> result = new ResultDto<OssFuncionResponseDto?>(null);
            try
            {
                var modulo = await _repository.GetByCodigo(dto.Id);
                if (modulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Funcion no existe";
                    return result;
                }
              
                
                if (String.IsNullOrEmpty(dto.Funcion) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.Descripcion) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Invalido";
                    return result;
                }
                

                modulo.Descripcion = dto.Descripcion;
              
                modulo.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                modulo.CodigoEmpresa = conectado.Empresa;
                modulo.UsuarioUpd = conectado.Usuario;
                await _repository.Update(modulo);
                var resultDto = await  MapOssFuncionDto(modulo);
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

        public async Task<ResultDto<OssFuncionResponseDto>> Create(OssFuncionUpdateDto dto)
        {

            ResultDto<OssFuncionResponseDto> result = new ResultDto<OssFuncionResponseDto>(null);
            try
            {
               
                var moduloValidate = await _repository.GetByFuncion(dto.Funcion);
                if (  moduloValidate != null )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Funcion existe";
                    return result;
                }

              
                if (String.IsNullOrEmpty(dto.Funcion) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Funcion Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.Descripcion) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Invalido";
                    return result;
                }
               
                
                OssFuncion entity = new OssFuncion();
                entity.Id = await _repository.GetNextKey();
                entity.Funcion = dto.Funcion;
                entity.Descripcion = dto.Descripcion;
             
                
                entity.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CodigoEmpresa = conectado.Empresa;
                entity.FechaIns = DateTime.Now;
                entity.UsuarioIns = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapOssFuncionDto(created.Data);
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
 
        public async Task<ResultDto<OssFuncionDeleteDto>> Delete(OssFuncionDeleteDto dto)
        {

            ResultDto<OssFuncionDeleteDto> result = new ResultDto<OssFuncionDeleteDto>(null);
            try
            {

                var funcion = await _repository.GetByCodigo(dto.Id);
                if (funcion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Funcion No existe";
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

        public async Task<ResultDto<OssFuncionResponseDto>> GetByCodigo(OssFuncionFilterDto dto)
        { 
            ResultDto<OssFuncionResponseDto> result = new ResultDto<OssFuncionResponseDto>(null);
            try
            {

                var funcion = await _repository.GetByCodigo(dto.Id);
                if (funcion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Funcion no existe";
                    return result;
                }
                
                var resultDto =  await MapOssFuncionDto(funcion);
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

          
        public async Task<ResultDto<List<OssFuncionResponseDto>>> GetAll()
        {
            ResultDto<List<OssFuncionResponseDto>> result = new ResultDto<List<OssFuncionResponseDto>>(null);
            try
            {

                var funcion = await _repository.GetByAll();
                if (funcion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListOssFuncionDto(funcion);
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

