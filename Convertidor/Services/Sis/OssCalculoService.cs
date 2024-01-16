using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Rh;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Rh;
using Convertidor.Services.Sis;
using Convertidor.Utility;
using NPOI.SS.Formula.Functions;


namespace Convertidor.Services.Sis
{
    
	public class OssCalculoService: IOssCalculoService
    {

      
        private readonly IOssCalculoRepository _repository;
        private readonly IOssVariableRepository _variableRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IOssModeloCalculoRepository _ossModeloCalculoRepository;
        private readonly IOssConfigServices _ossConfigService;
    


        public OssCalculoService(IOssCalculoRepository repository,
                                        IOssVariableRepository variableRepository,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IOssModeloCalculoRepository ossModeloCalculoRepository,
                                        IOssConfigServices ossConfigService)
		{
            _repository = repository;
            _variableRepository = variableRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _ossModeloCalculoRepository = ossModeloCalculoRepository;
            _ossConfigService = ossConfigService;
           
        }

        
       
       
        public  async Task<OssCalculoResponseDto> MapOssCalculoDto(OssCalculo dtos)
        {
            OssCalculoResponseDto itemResult = new OssCalculoResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.Id = dtos.Id;
                itemResult.IdCalculo = dtos.IdCalculo;
                itemResult.CodeVariable = dtos.CodeVariable;
                itemResult.IdVariable = dtos.IdVariable;
                itemResult.Formula = dtos.Formula;
                itemResult.FormulaDescripcion = dtos.FormulaDescripcion;
                itemResult.FormulaValor = dtos.FormulaValor;
                itemResult.OrdenCalculo = dtos.OrdenCalculo;
                itemResult.Valor = dtos.Valor;
                itemResult.Query = dtos.Query;
                itemResult.CodeVariableExterno = dtos.CodeVariableExterno;
                itemResult.IdCalculoExterno = dtos.IdCalculoExterno;
                itemResult.ModuloId = (int)dtos.ModuloId;
                itemResult.IdModeloCaculo = (int)dtos.IdModeloCalculo;
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<OssCalculoResponseDto>> MapListOssCalculoDto(List<OssCalculo> dtos)
        {
            List<OssCalculoResponseDto> result = new List<OssCalculoResponseDto>();
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
                        var itemResult =  await MapOssCalculoDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
      
        public async Task<ResultDto<OssCalculoResponseDto?>> Update(OssCalculoUpdateDto dto)
        {

            ResultDto<OssCalculoResponseDto?> result = new ResultDto<OssCalculoResponseDto?>(null);
            try
            {
                var calculo = await _repository.GetById(dto.Id);
                if (calculo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Formula no existe";
                    return result;
                }

                var variable = await _variableRepository.GetById(dto.IdVariable);
                if (variable == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "variable no existe";
                    return result;
                }

                if (variable.Code != dto.CodeVariable)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo de Variable Invalido";
                    return result;
                }
                
                
                var modeloCalculo = await _ossModeloCalculoRepository.GetByCodigo(dto.IdModeloCalculo);
                if (modeloCalculo==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modelo de Calculo Invalido";
                    return result;
                }
                if (dto.OrdenCalculo == null) dto.OrdenCalculo = 0;
                if (dto.OrdenCalculo<0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Orden de Calculo Invalida";
                    return result;
                }
                calculo.IdVariable = dto.Id;
                calculo.CodeVariable =dto.CodeVariable;
                calculo.Formula = dto.Formula;
                calculo.FormulaDescripcion = dto.FormulaDescripcion;
                calculo.FormulaValor = dto.FormulaValor;
                calculo.Valor = dto.Valor;
                calculo.Query = dto.Query;
                calculo.OrdenCalculo = dto.OrdenCalculo;
                calculo.CodeVariableExterno = dto.CodeVariableExterno;
                calculo.IdCalculoExterno = dto.IdCalculoExterno;
                calculo.IdModeloCalculo = dto.IdModeloCalculo;
                calculo.ModuloId = dto.ModuloId;
                
                calculo.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                calculo.CodigoEmpresa = conectado.Empresa;
                calculo.UsuarioUpd = conectado.Usuario;
                await _repository.Update(calculo);
                var resultDto = await  MapOssCalculoDto(calculo);
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

        public async Task<ResultDto<OssCalculoResponseDto>> Create(OssCalculoUpdateDto dto,int idCalculo)
        {

            ResultDto<OssCalculoResponseDto> result = new ResultDto<OssCalculoResponseDto>(null);
            try
            {
               
                var variable = await _variableRepository.GetById(dto.IdVariable);
                if (variable == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "variable no existe";
                    return result;
                }

                if (variable.Code != dto.CodeVariable)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo de Variable Invalido";
                    return result;
                }
                
                
                var modeloCalculo = await _ossModeloCalculoRepository.GetByCodigo(dto.IdModeloCalculo);
                if (modeloCalculo==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modelo de Calculo Invalido";
                    return result;
                }
                if (dto.OrdenCalculo == null) dto.OrdenCalculo = 0;
                if (dto.OrdenCalculo<0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Orden de Calculo Invalida";
                    return result;
                }
              
                
                OssCalculo entity = new OssCalculo();
                entity.Id = await _repository.GetNextKey();
                entity.IdCalculo = idCalculo;
                entity.IdVariable = dto.Id;
                entity.CodeVariable =dto.CodeVariable;
                entity.Formula = dto.Formula;
                entity.FormulaDescripcion = dto.FormulaDescripcion;
                entity.FormulaValor = dto.FormulaValor;
                entity.Valor = dto.Valor;
                entity.Query = dto.Query;
                entity.OrdenCalculo = dto.OrdenCalculo;
                entity.CodeVariableExterno = dto.CodeVariableExterno;
                entity.IdCalculoExterno = dto.IdCalculoExterno;
                entity.IdModeloCalculo = dto.IdModeloCalculo;
                entity.ModuloId = dto.ModuloId;
                
                entity.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CodigoEmpresa = conectado.Empresa;
                entity.FechaIns = DateTime.Now;
                entity.UsuarioIns = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapOssCalculoDto(created.Data);
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
 
        public async Task<ResultDto<OssCalculoDeleteDto>> Delete(OssCalculoDeleteDto dto)
        {

            ResultDto<OssCalculoDeleteDto> result = new ResultDto<OssCalculoDeleteDto>(null);
            try
            {

                var calculo = await _repository.GetById(dto.Id);
                if (calculo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Calculo No existe";
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

        public async Task<ResultDto<OssCalculoResponseDto>> GetById(OssFormulaFilterDto dto)
        { 
            ResultDto<OssCalculoResponseDto> result = new ResultDto<OssCalculoResponseDto>(null);
            try
            {

                var calculo = await _repository.GetById(dto.Id);
                if (calculo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Calculo no existe";
                    return result;
                }
                
                var resultDto =  await MapOssCalculoDto(calculo);
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

        public async Task<ResultDto<List<OssCalculoResponseDto>>> GetByIdCalculo(OssFormulaFilterDto dto)
        { 
            ResultDto<List<OssCalculoResponseDto>> result = new ResultDto<List<OssCalculoResponseDto>>(null);
            try
            {

                var calculo = await _repository.GetByIdCalculo(dto.IdCalculo);
                if (calculo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                
                var resultDto =  await MapListOssCalculoDto(calculo);
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

        

        public async Task<int> GetNextCalculo()
        {
            try
            {
               
                
                var result = await _ossConfigService.GetNextByClave("IdCalculo");
               

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

       
        
    }
}

