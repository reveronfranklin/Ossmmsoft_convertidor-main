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
    
	public class OssFormulaService: IOssFormulaService
    {

      
        private readonly IOssFormulaRepository _repository;
        private readonly IOssVariableRepository _variableRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public OssFormulaService(IOssFormulaRepository repository,
                                        IOssVariableRepository variableRepository,
                                        ISisUsuarioRepository sisUsuarioRepository)
		{
            _repository = repository;
            _variableRepository = variableRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
       
       
        public  async Task<OssFormulaResponseDto> MapOssFormulaDto(OssFormula dtos)
        {
            OssFormulaResponseDto itemResult = new OssFormulaResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.Id = dtos.Id;
                itemResult.CodeVariable = dtos.CodeVariable;
                itemResult.IdVariable = dtos.IdVariable;
                itemResult.Formula = dtos.Formula;
                itemResult.FormulaDescripcion = dtos.FormulaDescripcion;
                itemResult.OrdenCalculo = dtos.OrdenCalculo;
                itemResult.CodigoExterno = dtos.CodigoExterno;
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

        public async Task< List<OssFormulaResponseDto>> MapListOssFormulaDto(List<OssFormula> dtos)
        {
            List<OssFormulaResponseDto> result = new List<OssFormulaResponseDto>();
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
                        var itemResult =  await MapOssFormulaDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
      
        public async Task<ResultDto<OssFormulaResponseDto?>> Update(OssFormulaUpdateDto dto)
        {

            ResultDto<OssFormulaResponseDto?> result = new ResultDto<OssFormulaResponseDto?>(null);
            try
            {
                var formula = await _repository.GetById(dto.Id);
                if (formula == null)
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
                
                if (String.IsNullOrEmpty(dto.Formula) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Formula Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.FormulaDescripcion) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.CodigoExterno) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Externo Invalida";
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
                formula.Formula = dto.Formula;
                formula.FormulaDescripcion = dto.FormulaDescripcion;
                formula.IdVariable = dto.Id;
                formula.CodeVariable =dto.CodeVariable;
                formula.CodigoExterno = dto.CodigoExterno;
                formula.ModuloId = dto.ModuloId;
                
                formula.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                formula.CodigoEmpresa = conectado.Empresa;
                formula.UsuarioUpd = conectado.Usuario;
                await _repository.Update(formula);
                var resultDto = await  MapOssFormulaDto(formula);
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

        public async Task<ResultDto<OssFormulaResponseDto>> Create(OssFormulaUpdateDto dto)
        {

            ResultDto<OssFormulaResponseDto> result = new ResultDto<OssFormulaResponseDto>(null);
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
                
                if (String.IsNullOrEmpty(dto.Formula) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Formula Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.FormulaDescripcion) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.CodigoExterno) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Externo Invalida";
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
                
                OssFormula entity = new OssFormula();
                entity.Id = await _repository.GetNextKey();
                entity.Formula = dto.Formula;
                entity.FormulaDescripcion = dto.FormulaDescripcion;
                entity.IdVariable = dto.Id;
                entity.CodeVariable =dto.CodeVariable;
                entity.CodigoExterno = dto.CodigoExterno;
                entity.ModuloId = dto.ModuloId;
                
                entity.FechaUpd = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CodigoEmpresa = conectado.Empresa;
                entity.FechaIns = DateTime.Now;
                entity.UsuarioIns = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapOssFormulaDto(created.Data);
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
 
        public async Task<ResultDto<OssFormulaDeleteDto>> Delete(OssFormulaDeleteDto dto)
        {

            ResultDto<OssFormulaDeleteDto> result = new ResultDto<OssFormulaDeleteDto>(null);
            try
            {

                var formula = await _repository.GetById(dto.Id);
                if (formula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Formula No existe";
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

        public async Task<ResultDto<OssFormulaResponseDto>> GetById(OssFormulaFilterDto dto)
        { 
            ResultDto<OssFormulaResponseDto> result = new ResultDto<OssFormulaResponseDto>(null);
            try
            {

                var formula = await _repository.GetById(dto.Id);
                if (formula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo no existe";
                    return result;
                }
                
                var resultDto =  await MapOssFormulaDto(formula);
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


        public async Task<ResultDto<List<OssFormulaResponseDto>>> GetAll()
        {
            ResultDto<List<OssFormulaResponseDto>> result = new ResultDto<List<OssFormulaResponseDto>>(null);
            try
            {

                var formulas = await _repository.GetByAll();
                if (formulas == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListOssFormulaDto(formulas);
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

