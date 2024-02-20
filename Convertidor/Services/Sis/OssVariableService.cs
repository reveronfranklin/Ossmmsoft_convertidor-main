using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Sis;
using Convertidor.Utility;
using NPOI.SS.Formula.Functions;


namespace Convertidor.Services.Sis
{
    
	public class OssVariableService: IOssVariableService
    {

      
        private readonly IOssVariableRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public OssVariableService(IOssVariableRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository)
		{
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
       
       
        public  async Task<OssVariableResponseDto> MapOssVariableDto(OssVariable dtos)
        {
            OssVariableResponseDto itemResult = new OssVariableResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.Id = dtos.Id;
                itemResult.Code = dtos.Code;
                itemResult.Descripcion = dtos.Descripcion;
                itemResult.Longitud = dtos.Longitud;
                itemResult.LongitudRedondeo = dtos.LongitudRedondeo;
                itemResult.LongitudTruncado =dtos.LongitudTruncado;
                itemResult.LongitudDecimal = dtos.LongitudDecimal;
                itemResult.ModuloId = dtos.ModuloId;
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<OssVariableResponseDto>> MapListOssVariableDto(List<OssVariable> dtos)
        {
            List<OssVariableResponseDto> result = new List<OssVariableResponseDto>();
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
                        var itemResult =  await MapOssVariableDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
      
        public async Task<ResultDto<OssVariableResponseDto?>> Update(OssVariableUpdateDto dto)
        {

            ResultDto<OssVariableResponseDto?> result = new ResultDto<OssVariableResponseDto?>(null);
            try
            {
                var variable = await _repository.GetById(dto.Id);
                if (variable == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Variable no existe";
                    return result;
                }
              
                
                if (String.IsNullOrEmpty(dto.Code) )
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
                    result.Message = "Descripcion Invalida";
                    return result;
                }
                

                variable.Descripcion = dto.Descripcion;
                variable.Longitud = dto.Longitud;
                variable.LongitudRedondeo = dto.LongitudRedondeo;
                variable.LongitudTruncado =dto.LongitudTruncado;
                variable.LongitudDecimal = dto.LongitudDecimal;
                variable.ModuloId = dto.ModuloId;
                variable.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                variable.CodigoEmpresa = conectado.Empresa;
                variable.UsuarioUpd = conectado.Usuario;
                await _repository.Update(variable);
                var resultDto = await  MapOssVariableDto(variable);
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

        public async Task<ResultDto<OssVariableResponseDto>> Create(OssVariableUpdateDto dto)
        {

            ResultDto<OssVariableResponseDto> result = new ResultDto<OssVariableResponseDto>(null);
            try
            {
               
                var moduloValidate = await _repository.GetByCodigo(dto.Code);
                if (  moduloValidate != null )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Variable existe";
                    return result;
                }

              
                if (String.IsNullOrEmpty(dto.Code) )
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
                    result.Message = "Descripcion Invalido";
                    return result;
                }
               
                
                OssVariable entity = new OssVariable();
                entity.Id = await _repository.GetNextKey();
                entity.Code = dto.Code;
                entity.Descripcion = dto.Descripcion;
                entity.Longitud = dto.Longitud;
                entity.LongitudRedondeo = dto.LongitudRedondeo;
                entity.LongitudTruncado =dto.LongitudTruncado;
                entity.LongitudDecimal = dto.LongitudDecimal;
                entity.ModuloId = dto.ModuloId;
                
                entity.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CodigoEmpresa = conectado.Empresa;
                entity.FechaIns = DateTime.Now;
                entity.UsuarioIns = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapOssVariableDto(created.Data);
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
 
        public async Task<ResultDto<OssVariableDeleteDto>> Delete(OssVariableDeleteDto dto)
        {

            ResultDto<OssVariableDeleteDto> result = new ResultDto<OssVariableDeleteDto>(null);
            try
            {

                var variable = await _repository.GetById(dto.Id);
                if (variable == null)
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

        public async Task<ResultDto<OssVariableResponseDto>> GetById(OssVariableFilterDto dto)
        { 
            ResultDto<OssVariableResponseDto> result = new ResultDto<OssVariableResponseDto>(null);
            try
            {

                var variable = await _repository.GetById(dto.Id);
                if (variable == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo no existe";
                    return result;
                }
                
                var resultDto =  await MapOssVariableDto(variable);
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

        public async Task<ResultDto<OssVariableResponseDto>> GetByCodigo(OssVariableFilterDto dto)
        { 
            ResultDto<OssVariableResponseDto> result = new ResultDto<OssVariableResponseDto>(null);
            try
            {

                var variable = await _repository.GetByCodigo(dto.Code);
                if (variable == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo no existe";
                    return result;
                }
                
                var resultDto =  await MapOssVariableDto(variable);
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
  
        public async Task<ResultDto<List<OssVariableResponseDto>>> GetAll()
        {
            ResultDto<List<OssVariableResponseDto>> result = new ResultDto<List<OssVariableResponseDto>>(null);
            try
            {

                var variables = await _repository.GetByAll();
                if (variables == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListOssVariableDto(variables);
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

