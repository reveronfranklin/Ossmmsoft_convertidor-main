using System.Data;
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
        private readonly IRhMovNominaService _rhMovNominaService;
        private readonly IOssFormulaService _ossFormulaService;
        private readonly IRhConceptosService _rhConceptosService;
        private readonly IOssConfigRepository _ossConfigRepository;


        public OssCalculoService(IOssCalculoRepository repository,
                                        IOssVariableRepository variableRepository,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IOssModeloCalculoRepository ossModeloCalculoRepository,
                                        IOssConfigServices ossConfigService,
                                        IRhMovNominaService rhMovNominaService,
                                        IOssFormulaService ossFormulaService,
                                        IRhConceptosService rhConceptosService,
                                        IOssConfigRepository ossConfigRepository)
		{
            _repository = repository;
            _variableRepository = variableRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _ossModeloCalculoRepository = ossModeloCalculoRepository;
            _ossConfigService = ossConfigService;
            _rhMovNominaService = rhMovNominaService;
            _ossFormulaService = ossFormulaService;
            _rhConceptosService = rhConceptosService;
            _ossConfigRepository = ossConfigRepository;
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
                itemResult.FormulaValor = dtos.FormulaValor;
                itemResult.OrdenCalculo = dtos.OrdenCalculo;
                itemResult.Valor = dtos.Valor;
                itemResult.Query = dtos.Query;
                itemResult.CodeVariableExterno = dtos.CodeVariableExterno;
                itemResult.IdCalculoExterno = dtos.IdCalculoExterno;
                itemResult.ModuloId = (int)dtos.ModuloId;
                itemResult.IdModeloCalculo = (int)dtos.IdModeloCalculo;
                itemResult.AcumulaAlTotal = dtos.AcumulaAlTotal;
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }
        public  async Task<OssCalculoUpdateDto> MapOssCalculoResponseToUpdateDto(OssCalculoResponseDto dtos)
        {
            OssCalculoUpdateDto itemResult = new OssCalculoUpdateDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.Id = dtos.Id;
                itemResult.IdCalculo = (int)dtos.IdCalculo;
                itemResult.CodeVariable = dtos.CodeVariable.ToString();
                itemResult.IdVariable = (int)dtos.IdVariable;
                itemResult.Formula = dtos.Formula;
                itemResult.FormulaValor = dtos.FormulaValor;
                itemResult.OrdenCalculo = dtos.OrdenCalculo;
                itemResult.Valor = dtos.Valor;
                itemResult.Query = dtos.Query;
                itemResult.CodeVariableExterno = dtos.CodeVariableExterno;
                itemResult.IdCalculoExterno = dtos.IdCalculoExterno;
                itemResult.ModuloId = (int)dtos.ModuloId;
                itemResult.IdModeloCalculo = (int)dtos.IdModeloCalculo;
                itemResult.AcumulaAlTotal = dtos.AcumulaAlTotal;
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
                calculo.IdVariable = dto.IdVariable;
                calculo.CodeVariable =dto.CodeVariable;
                calculo.Formula = dto.Formula;

                calculo.FormulaValor = dto.FormulaValor;
                calculo.Valor = dto.Valor;
                calculo.Query = dto.Query;
                calculo.OrdenCalculo = (int)dto.OrdenCalculo;
                calculo.CodeVariableExterno = dto.CodeVariableExterno;
                calculo.IdCalculoExterno = dto.IdCalculoExterno;
                calculo.IdModeloCalculo = dto.IdModeloCalculo;
                calculo.ModuloId = dto.ModuloId;
                if (dto.AcumulaAlTotal == null) dto.AcumulaAlTotal = 0;
                calculo.AcumulaAlTotal = dto.AcumulaAlTotal;
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
                entity.IdVariable = dto.IdVariable;
                entity.CodeVariable =dto.CodeVariable;
                entity.Formula = dto.Formula;
                entity.FormulaValor = dto.FormulaValor;
                entity.Valor = dto.Valor;
                entity.Query = dto.Query;
                entity.OrdenCalculo = (int)dto.OrdenCalculo;
                entity.CodeVariableExterno = dto.CodeVariableExterno;
                entity.IdCalculoExterno = dto.IdCalculoExterno;
                entity.IdModeloCalculo = dto.IdModeloCalculo;
                entity.ModuloId = dto.ModuloId;
                if (dto.AcumulaAlTotal == null) dto.AcumulaAlTotal = 0;
                entity.AcumulaAlTotal = dto.AcumulaAlTotal;
          
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CodigoEmpresa = conectado.Empresa;
                entity.FechaIns = DateTime.Now;
                entity.UsuarioUpd = conectado.Usuario;
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

        public async Task<ResultDto<OssCalculoResponseDto>> GetById(OssCalculoFilterDto dto)
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

        public async Task<ResultDto<List<OssCalculoResponseDto>>> GetByIdCalculo(int idCalculo)
        { 
            ResultDto<List<OssCalculoResponseDto>> result = new ResultDto<List<OssCalculoResponseDto>>(null);
            try
            {

                var calculo = await _repository.GetByIdCalculo(idCalculo);
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


        public async Task<decimal> GetTotalByIdCalculo(int idCalculo)
        {
            decimal result = 0;
            var calculo = await GetByIdCalculo(idCalculo);
            if (calculo.IsValid && calculo.Data.Count > 0)
            {
                foreach (var item in calculo.Data.Where(c=>c.AcumulaAlTotal==1).ToList())
                {
                    int i = 0;
                    string s = item.Valor;  
                    bool isNumeric = int.TryParse(s, out i);
                    if (isNumeric)
                    {
                        
                        var convertDecimal = Convert.ToDecimal(item.Valor);
                        result = result + convertDecimal;

                    }
                    
                    
                   
                }
            }

            return result;


        }
        
        
        public async Task CarcularFormulas(int calculoId)
        {
             
            var calculo = await GetByIdCalculo(calculoId);
            foreach (var item in calculo.Data)
            {
                var resultItem = await CalculateFormula(item);
            }
                        
        }
        
        public async Task UpdateEntradas(RhMovNominaFilterDto dto,int calculoId)
        {
             //ACTUALIZAR ENTRADAS=====> CODIGOTIPONOMINA CODIGOPERSONA CODIGOCONCEPTO
                        
            var calculo = await GetByIdCalculo(calculoId);
            if (calculo !=null && calculo.Data.Count > 0)
            {
                foreach (var item in calculo.Data)
                {

                        if (item.CodeVariable == "CODIGOTIPONOMINA")
                        {
                            _repository.ExecuteQueryUpdateValor(dto.CodigoTipoNomina.ToString(),
                                item.Id,"E");
                          
                        }
                        if (item.CodeVariable == "CODIGOPERSONA")
                        {
                            _repository.ExecuteQueryUpdateValor(dto.CodigoPersona.ToString(),
                                item.Id,"E");
                         
                        }
                        if (item.CodeVariable == "CODIGOCONCEPTO")
                        {
                            _repository.ExecuteQueryUpdateValor(dto.CodigoConcepto.ToString(),
                                item.Id,"E");
                            
                        }
                        if (item.CodeVariable == "CODIGOPERIODO")
                        {
                            _repository.ExecuteQueryUpdateValor(dto.CodigoPeriodo.ToString(),
                                item.Id,"E");
                            
                        }
                        if (item.CodeVariable == "SIGLASTIPONOMINA")
                        {
                            _repository.ExecuteQueryUpdateValor(dto.SiglasTipoNomina.ToString(),
                                item.Id,"E");
                           
                        }
                        if (item.CodeVariable == "FECHANOMINA")
                        {
                            _repository.ExecuteQueryUpdateValor(dto.FechaNomina.ToString(),
                                item.Id,"E");
                            
                        }
                }
            }
        }

        public async Task<ResultDto<List<OssCalculoResponseDto>>> CalculoTipoNominaPersonaConcepto(
            RhMovNominaFilterDto dto)
        {
            int calculoId=await CrearCalculoTipoNominaPersonaConcepto(dto);
            await UpdateEntradas(dto, calculoId);
            await CalculateFormulas(calculoId);
            var total = await GetTotalByIdCalculo(calculoId);
            var movNominaUpdated = await _rhMovNominaService.UpdateCalculo(dto.CodigoMovNomina, calculoId,total);
            var calculo = await GetByIdCalculo(calculoId);
            return calculo;

        }
        public async Task CalculateFormulas(int calculoId)
        {
            try
            {


                var calculosFormulas = await _repository.GetFormulasByIdCalculo(calculoId);
                if (calculosFormulas.Count > 0)
                {
                    foreach (var item in calculosFormulas)
                    {
                        string valueFormula = await this.GetValueFormula(calculoId, item.Formula);
                        string query = "";
                        query = "update  sis.OSS_CALCULO set valor= ";
                        query = query + valueFormula + " where id= " + item.Id.ToString();
                        _repository.ExecuteQueryUpdateValor(valueFormula,item.Id,"'F'");
                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                string message = ex.Message;
             
            }
        }

        
        public async Task<int> CrearCalculoTipoNominaPersonaConcepto(RhMovNominaFilterDto dto)
        { 
            int result = 0;
            ResultDto<List<OssCalculoResponseDto>> calculoResult = new ResultDto<List<OssCalculoResponseDto>>(null);
            try
            {
                var calculoId = await GetNextCalculo();
                var movNomina = await _rhMovNominaService.GetByTipoNominaPersonaConcepto(dto);
                if (movNomina.Data != null)     
                {
                    var concepto = await _rhConceptosService.GetByTipoNominaConcepto(dto.CodigoTipoNomina, dto.CodigoConcepto);
                    OssFormulaFilterDto formulaFilter = new OssFormulaFilterDto();
                    formulaFilter.ModeloCalculo = 1;
                    var formula = await _ossFormulaService.GetByIdModeloCalculo(formulaFilter);
                    if (formula.Data.Count > 0)
                    {
                        foreach (var item in formula.Data)
                        {
                            OssCalculoUpdateDto createCalculo = new OssCalculoUpdateDto();
                         
                            createCalculo.IdCalculo = calculoId;
                            createCalculo.IdVariable = item.IdVariable;
                            createCalculo.CodeVariable =item.CodeVariable;
                            createCalculo.Formula = item.Formula;
                            createCalculo.FormulaValor = "";
                            createCalculo.Valor = "";
                            createCalculo.Query = "";
                            createCalculo.OrdenCalculo = item.OrdenCalculo;
                            createCalculo.CodeVariableExterno = $"{dto.CodigoTipoNomina.ToString()}-{dto.CodigoConcepto}";
                            createCalculo.IdCalculoExterno = "";
                            createCalculo.IdModeloCalculo = item.IdModeloCalculo;
                            createCalculo.ModuloId = item.ModuloId;
                            createCalculo.AcumulaAlTotal = item.AcumulaAlTotal;
                            var resultCreate = await Create(createCalculo,calculoId);
                            
                        }
                       

                        
                        
                    }
                }

              
                result=calculoId;
               
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        
        
        public async Task<ResultDto<OssCalculoResponseDto> > GetByIdCalculoCode(int calculoId,string codeVariable)
        { 
            ResultDto<OssCalculoResponseDto> result = new ResultDto<OssCalculoResponseDto> (null);
            try
            {

                var calculo = await _repository.GetByIdCalculoCode(calculoId,codeVariable);
                if (calculo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
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

        public async Task<Decimal> CalculateFormula(OssCalculoResponseDto calculo)
        {
            try
            {


                Decimal result = 0M;
                if (calculo == null)
                {
                    result = 0M;
                    return result;
                }

                string valueFormula = await this.GetValueFormula((int)calculo.IdCalculo, calculo.Formula);
                string query = "";
                query = "update  sis.OSS_CALCULO set valor= ";
                query = query + valueFormula + " where id= " + calculo.Id.ToString();

                //object obj = new DataTable().Compute(valueFormula, "");
                //obj.ToString();
                //result = Convert.ToDecimal(obj.ToString());
                _repository.ExecuteQueryUpdateValor(valueFormula,calculo.Id,"F");
                //FormattableString xquery = $"UPDATE  SIS.OSS_CALCULO SET VALOR= {valueFormula} WHERE ID= { calculo.Id.ToString()}";
                //var result = _context.Database.ExecuteSqlInterpolated(xquery);
                var calculoUpdated = await _repository.GetById(calculo.Id);
               
                calculoUpdated.FormulaValor = valueFormula;
                calculoUpdated.Query = query;
                await _repository.Update(calculoUpdated);
                result = 1;
                return result;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return 0M;
            }
        }

        public async Task<string> GetValueFormula(int calculoId, string formula)
        {
            string newFormula = "";
            List<string> listString = GetListString(formula, "[", "]");
            newFormula = formula;
            foreach (string item in listString)
            {
                var codeVariableCode = await GetByIdCalculoCode(calculoId, item);
                
                if (codeVariableCode.Data != null )
                {
                    
                    int i = 0;
                    string s = codeVariableCode.Data.Valor;  
                    bool result = int.TryParse(s, out i);
                    if (result)
                    {
                        newFormula = newFormula.Replace("[" + item + "]", codeVariableCode.Data.Valor.ToString());

                    }
                    else
                    {
                        var valor = codeVariableCode.Data.Valor;
                        valor = $"'{valor}'";
                        newFormula = newFormula.Replace("[" + item + "]", valor);

                    }
                       
                }
                else
                {
                    var config = await _ossConfigRepository.GetByClave(item);

                    if (config != null)
                    {
                        newFormula = newFormula.Replace("[" + item + "]", config.VALOR);
                    }
                    else
                    {
                        newFormula = newFormula.Replace("[" + item + "]", "0");
                    }
                       
                }
            }
            //newFormula = newFormula.Replace(",", ".");
            string valueFormula = newFormula;
            newFormula = (string)null;
            return valueFormula;
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

        public List<string> GetListString(string str, string initialDelimeter, string endDelimeter)
        {


            int longitudStr = 0;
            int delimitadorInicialPosicion = 0;
            int delimitadorFinalPosicion = 0;
            string valor = "";
            string newValue = "";

            var myList = new List<string>();
            var n = str.IndexOf(initialDelimeter);
            var contiene = str.Contains(initialDelimeter);
            while (str.Contains(initialDelimeter))
            {
                delimitadorInicialPosicion = str.IndexOf(initialDelimeter);
                delimitadorFinalPosicion = str.IndexOf(endDelimeter);
                valor = str.Substring(delimitadorInicialPosicion + 1, (delimitadorFinalPosicion - delimitadorInicialPosicion) - 1);
                longitudStr = str.Length;
                newValue = str.Substring(delimitadorFinalPosicion + 1, (longitudStr - delimitadorFinalPosicion) - 1);
                myList.Add(valor);
                str = newValue;

            }

            return myList;

        }
        
    }
}

