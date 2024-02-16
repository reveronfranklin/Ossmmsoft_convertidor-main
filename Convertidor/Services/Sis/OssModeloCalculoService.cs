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
    
	public class OssModeloCalculoService: IOssModeloCalculoService
    {

      
        private readonly IOssModeloCalculoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public OssModeloCalculoService(IOssModeloCalculoRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository)
		{
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
       
       
        public  async Task<OssModeloCalculoResponseDto> MapOssModeloDto(OssModeloCalculo dtos)
        {
            OssModeloCalculoResponseDto itemResult = new OssModeloCalculoResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.Id = dtos.Id;
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

        public async Task< List<OssModeloCalculoResponseDto>> MapListOssModeloDto(List<OssModeloCalculo> dtos)
        {
            List<OssModeloCalculoResponseDto> result = new List<OssModeloCalculoResponseDto>();
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
                        var itemResult =  await MapOssModeloDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
      
        public async Task<ResultDto<OssModeloCalculoResponseDto?>> Update(OssModeloCalculoUpdateDto dto)
        {

            ResultDto<OssModeloCalculoResponseDto?> result = new ResultDto<OssModeloCalculoResponseDto?>(null);
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
                
                if (String.IsNullOrEmpty(dto.Descripcion) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalido";
                    return result;
                }
                

                modulo.Descripcion = dto.Descripcion;
              
                modulo.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                modulo.CodigoEmpresa = conectado.Empresa;
                modulo.UsuarioUpd = conectado.Usuario;
                await _repository.Update(modulo);
                var resultDto = await  MapOssModeloDto(modulo);
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

        public async Task<ResultDto<OssModeloCalculoResponseDto>> Create(OssModeloCalculoUpdateDto dto)
        {

            ResultDto<OssModeloCalculoResponseDto> result = new ResultDto<OssModeloCalculoResponseDto>(null);
            try
            {
                
                
                if (String.IsNullOrEmpty(dto.Descripcion) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Invalido";
                    return result;
                }
               
                
                OssModeloCalculo entity = new OssModeloCalculo();
                entity.Id = await _repository.GetNextKey();
                entity.Descripcion = dto.Descripcion;
             
                
                entity.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CodigoEmpresa = conectado.Empresa;
                entity.FechaIns = DateTime.Now;
                entity.UsuarioIns = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapOssModeloDto(created.Data);
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
 
        public async Task<ResultDto<OssModeloCalculoDeleteDto>> Delete(OssModeloCalculoDeleteDto dto)
        {

            ResultDto<OssModeloCalculoDeleteDto> result = new ResultDto<OssModeloCalculoDeleteDto>(null);
            try
            {

                var modelo = await _repository.GetByCodigo(dto.Id);
                if (modelo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modelo No existe";
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

        public async Task<ResultDto<OssModeloCalculoResponseDto>> GetByCodigo(OssModeloCalculoFilterDto dto)
        { 
            ResultDto<OssModeloCalculoResponseDto> result = new ResultDto<OssModeloCalculoResponseDto>(null);
            try
            {

                var modelo = await _repository.GetByCodigo(dto.Id);
                if (modelo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo no existe";
                    return result;
                }
                
                var resultDto =  await MapOssModeloDto(modelo);
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

          
        public async Task<ResultDto<List<OssModeloCalculoResponseDto>>> GetAll()
        {
            ResultDto<List<OssModeloCalculoResponseDto>> result = new ResultDto<List<OssModeloCalculoResponseDto>>(null);
            try
            {

                var modelos = await _repository.GetByAll();
                if (modelos == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListOssModeloDto(modelos);
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

