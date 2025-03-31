using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;


namespace Convertidor.Services.Sis
{
    
	public class SisBancoService: ISisBancoService
    {
        private readonly ISisBancoRepository _repository;
        private readonly ISisCuentaBancoRepository _cuentaBancoRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public SisBancoService(ISisBancoRepository repository,
                                ISisCuentaBancoRepository cuentaBancoRepository,
                                      ISisUsuarioRepository sisUsuarioRepository)
		{
            _repository = repository;
            _cuentaBancoRepository = cuentaBancoRepository;
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
       
       
        public  async Task<SisBancoResponseDto> MapSisBancoDto(SIS_BANCOS dtos)
        {
            SisBancoResponseDto itemResult = new SisBancoResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.CodigoBanco = dtos.CODIGO_BANCO;
                itemResult.Nombre = dtos.NOMBRE;
                itemResult.CodigoInterbancario = dtos.CODIGO_INTERBANCARIO;
                itemResult.SearchText=itemResult.Nombre;
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<SisBancoResponseDto>> MapListOssVariableDto(List<SIS_BANCOS> dtos)
        {
            List<SisBancoResponseDto> result = new List<SisBancoResponseDto>();
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
                        var itemResult =  await MapSisBancoDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
      
        public async Task<ResultDto<SisBancoResponseDto>> Update(SisBancoUpdateDto dto)
        {

            ResultDto<SisBancoResponseDto> result = new ResultDto<SisBancoResponseDto>(null);
            try
            {
                var banco = await _repository.GetById(dto.CodigoBanco);
                if (banco == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Banco no existe";
                    return result;
                }
              
                
                if (String.IsNullOrEmpty(dto.Nombre) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.CodigoInterbancario) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Interbancario Invalido";
                    return result;
                }
                

                banco.NOMBRE = dto.Nombre;
                banco.CODIGO_INTERBANCARIO = dto.CodigoInterbancario;
    
                banco.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                banco.CODIGO_EMPRESA = conectado.Empresa;
                banco.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(banco);
                var resultDto = await  MapSisBancoDto(banco);
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

        public async Task<ResultDto<SisBancoResponseDto>> Create(SisBancoUpdateDto dto)
        {

            ResultDto<SisBancoResponseDto> result = new ResultDto<SisBancoResponseDto>(null);
            try
            {
              
                if (String.IsNullOrEmpty(dto.Nombre) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.CodigoInterbancario) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Interbancario Invalido";
                    return result;
                }
               
               

                var findBanco= await _repository.GetByCodigoInterbancario(dto.CodigoInterbancario);
                if (findBanco != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Interbancario Invalido, ya existe un banco con este codigo";
                    return result;
                }
                
                SIS_BANCOS entity = new SIS_BANCOS();
                entity.CODIGO_BANCO = await _repository.GetNextKey();
                entity.NOMBRE = dto.Nombre;
                entity.CODIGO_INTERBANCARIO = dto.CodigoInterbancario;
              
                
           
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapSisBancoDto(created.Data);
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
 
        public async Task<ResultDto<SisBancoDeleteDto>> Delete(SisBancoDeleteDto dto)
        {

            ResultDto<SisBancoDeleteDto> result = new ResultDto<SisBancoDeleteDto>(null);
            try
            {

                var banco = await _repository.GetById(dto.CodigoBanco);
                if (banco == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Banco No existe";
                    return result;
                }
                var bancooExisteEnCuenta= await _cuentaBancoRepository.ExisteBanco(dto.CodigoBanco);
                if (bancooExisteEnCuenta==true)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Banco Tiene una cuenta relacionada";
                    return result;
                }
                var deleted = await _repository.Delete(dto.CodigoBanco);

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

     

     
        public async Task<ResultDto<List<SisBancoResponseDto>>> GetAll(SisBancoFilterDto filter)
        {
            ResultDto<List<SisBancoResponseDto>> result = new ResultDto<List<SisBancoResponseDto>>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                filter.CodigEmpresa = conectado.Empresa;
                var bancos = await _repository.GetAll(filter);
                if (bancos.Data == null)
                {
                    result.Data = null;
                    result.CantidadRegistros=0;
                    result.Page = 0;
                    result.TotalPage = 0;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListOssVariableDto(bancos.Data);
                result.Data = resultDto;
                result.CantidadRegistros=bancos.CantidadRegistros;
                result.Page = bancos.Page;
                result.TotalPage = bancos.TotalPage;
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

