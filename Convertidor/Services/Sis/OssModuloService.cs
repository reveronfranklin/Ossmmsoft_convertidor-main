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
    
	public class OssModuloService: IOssModuloService
    {

      
        private readonly IOssModuloRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public OssModuloService(IOssModuloRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository)
		{
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
       
       
        public  async Task<OssModuloResponseDto> MapOssModuloDto(OssModulo dtos)
        {
            OssModuloResponseDto itemResult = new OssModuloResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.Id = dtos.Id;
                itemResult.Codigo = dtos.Codigo;
                itemResult.Descripcion = dtos.Descripcion;
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<OssModuloResponseDto>> MapListOssModuloDto(List<OssModulo> dtos)
        {
            List<OssModuloResponseDto> result = new List<OssModuloResponseDto>();
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
                        var itemResult =  await MapOssModuloDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
      
        public async Task<ResultDto<OssModuloResponseDto?>> Update(OssModuloUpdateDto dto)
        {

            ResultDto<OssModuloResponseDto?> result = new ResultDto<OssModuloResponseDto?>(null);
            try
            {
                var modulo = await _repository.GetByCodigo(dto.Id);
                if (modulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo no existe";
                    return result;
                }
              
                
                if (String.IsNullOrEmpty(dto.Codigo) )
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
                var resultDto = await  MapOssModuloDto(modulo);
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

        public async Task<ResultDto<OssModuloResponseDto>> Create(OssModuloUpdateDto dto)
        {

            ResultDto<OssModuloResponseDto> result = new ResultDto<OssModuloResponseDto>(null);
            try
            {
               
                var moduloValidate = await _repository.GetByCodigoLargo(dto.Codigo);
                if (  moduloValidate != null )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo existe";
                    return result;
                }

              
                if (String.IsNullOrEmpty(dto.Codigo) )
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
               
                
                OssModulo entity = new OssModulo();
                entity.Id = await _repository.GetNextKey();
                entity.Codigo = dto.Codigo;
                entity.Descripcion = dto.Descripcion;
             
                
                entity.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CodigoEmpresa = conectado.Empresa;
                entity.FechaIns = DateTime.Now;
                entity.UsuarioIns = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapOssModuloDto(created.Data);
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
 
        public async Task<ResultDto<OssModuloDeletedto>> Delete(OssModuloDeletedto dto)
        {

            ResultDto<OssModuloDeletedto> result = new ResultDto<OssModuloDeletedto>(null);
            try
            {

                var modulo = await _repository.GetByCodigo(dto.Id);
                if (modulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo No existe";
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

        public async Task<ResultDto<OssModuloResponseDto>> GetByCodigo(OssModuloFilterDto dto)
        { 
            ResultDto<OssModuloResponseDto> result = new ResultDto<OssModuloResponseDto>(null);
            try
            {

                var modulo = await _repository.GetByCodigo(dto.Id);
                if (modulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo no existe";
                    return result;
                }
                
                var resultDto =  await MapOssModuloDto(modulo);
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

          
        public async Task<ResultDto<List<OssModuloResponseDto>>> GetAll()
        {
            ResultDto<List<OssModuloResponseDto>> result = new ResultDto<List<OssModuloResponseDto>>(null);
            try
            {

                var modulo = await _repository.GetByAll();
                if (modulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListOssModuloDto(modulo);
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

