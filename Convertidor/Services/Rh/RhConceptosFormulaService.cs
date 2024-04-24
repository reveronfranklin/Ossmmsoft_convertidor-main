using System.Globalization;
using Convertidor.Data.Repository.Rh;
using Convertidor.Utility;


namespace Convertidor.Services.Adm
{
    
	public class RhConceptosFormulaService: IRhConceptosFormulaService
    {

      
        private readonly IRhConceptosFormulaRepository _repository;
        private readonly IRhDescriptivaRepository _repositoryRhDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;


        public RhConceptosFormulaService(IRhConceptosFormulaRepository repository,
                                        IRhDescriptivaRepository repositoryRhDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                        IRhConceptosRepository rhConceptosRepository)
		{
            _repository = repository;
            _repositoryRhDescriptiva = repositoryRhDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhConceptosRepository = rhConceptosRepository;
        }


        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            try
            {
                FechaDesdeObj.Year = fecha.Year.ToString();
                string month = "00" + fecha.Month.ToString();
                string day = "00" + fecha.Day.ToString();
                FechaDesdeObj.Month = month.Substring(month.Length - 2);
                FechaDesdeObj.Day = day.Substring(day.Length - 2);
                return FechaDesdeObj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return FechaDesdeObj;
            }
           
         
    
          
        }
       
      
        public  async Task<RhConceptosFormulaResponseDto> MapRhConceptosFormulaDto(RH_FORMULA_CONCEPTOS dtos)
        {
            RhConceptosFormulaResponseDto itemResult = new RhConceptosFormulaResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.CodigoConcepto = dtos.CODIGO_CONCEPTO;
                itemResult.CodigoFormulaConcepto = dtos.CODIGO_FORMULA_CONCEPTO;
                itemResult.FechaDesde = (DateTime)dtos.FECHA_DESDE;
                itemResult.FechaDesdeString =  itemResult.FechaDesde.ToString("u"); 
                FechaDto FechaDesdeObj = GetFechaDto((DateTime)dtos.FECHA_DESDE);
                itemResult.FechaDesdeObj = FechaDesdeObj;
                itemResult.FechaHasta = (DateTime)dtos.FECHA_HASTA;
                itemResult.FechaHastaString =    itemResult.FechaHasta.ToString("u"); 
                FechaDto FechaHastaObj = GetFechaDto((DateTime)dtos.FECHA_HASTA);
                itemResult.FechaHastaObj = FechaHastaObj;
                if (dtos.PORCENTAJE == null) dtos.PORCENTAJE = 0;
                itemResult.Porcentaje = dtos.PORCENTAJE;
                if (dtos.PORCENTAJE_PATRONAL == null) dtos.PORCENTAJE_PATRONAL = 0;
                itemResult.PorcentajePatronal = dtos.PORCENTAJE_PATRONAL;
                if (dtos.MONTO_TOPE == null) dtos.MONTO_TOPE = 0;
                itemResult.MontoTope = dtos.MONTO_TOPE;
                itemResult.TipoSueldo = dtos.TIPO_SUELDO;
                itemResult.TipoSueldoDescripcion = "";
                var tipoSueldoDescripcion = GetListTipoSueldo().Where(x => x.Codigo== dtos.TIPO_SUELDO).FirstOrDefault();
                if (tipoSueldoDescripcion != null)
                {
                    itemResult.TipoSueldoDescripcion = tipoSueldoDescripcion.Decripcion;
                }
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<RhConceptosFormulaResponseDto>> MapListConceptosFormulaDto(List<RH_FORMULA_CONCEPTOS> dtos)
        {
            List<RhConceptosFormulaResponseDto> result = new List<RhConceptosFormulaResponseDto>();
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
                        var itemResult =  await MapRhConceptosFormulaDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
        public List<ListTipoSueldo> GetListTipoSueldo()
        {
            List<ListTipoSueldo> result = new List<ListTipoSueldo>();
            ListTipoSueldo result1 = new ListTipoSueldo();
            result1.Codigo = "SB";
            result1.Decripcion = "Sueldo Basico";
            ListTipoSueldo result2 = new ListTipoSueldo();
            result2.Codigo = "SI";
            result2.Decripcion = "Sueldo Integral";
            result.Add(result1); //SUELDO BASICO
            result.Add(result2); // SUELDO INTEGRAL
            return result;
        }
        public async Task<ResultDto<RhConceptosFormulaResponseDto?>> Update(RhConceptosFormulaUpdateDto dto)
        {

            ResultDto<RhConceptosFormulaResponseDto?> result = new ResultDto<RhConceptosFormulaResponseDto?>(null);
            try
            {

                var conceptoFormula = await _repository.GetByCodigo(dto.CodigoFormulaConcepto);
                if (conceptoFormula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto Formula no existe";
                    return result;
                }
                
                var concepto = await _rhConceptosRepository.GetByCodigo(dto.CodigoConcepto);
                if (concepto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto no existe";
                    return result;
                }
                var tipoSueldo = GetListTipoSueldo().Where(x => x.Codigo== dto.TipoSueldo).FirstOrDefault();
                if (tipoSueldo==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Sueldo Invalido";
                    return result;
                    
                }
                
                if (!DateValidate.IsDate(dto.FechaDesdeString))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha desde No VAlida";
                    return result;
                }
                
            
                if (DateValidate.IsDate(dto.FechaDesdeString))
                {
                    var fechaDesde = Convert.ToDateTime(dto.FechaDesdeString, CultureInfo.InvariantCulture);
              
                    conceptoFormula.FECHA_DESDE = fechaDesde;
                }
                
                if (DateValidate.IsDate(dto.FechaHastaString))
                {
                    var fechaHasta = Convert.ToDateTime(dto.FechaHastaString, CultureInfo.InvariantCulture);
                    if (fechaHasta.Year <= 1900)
                    {
                        conceptoFormula.FECHA_HASTA = null;
                    }
                    else
                    {
                        conceptoFormula.FECHA_HASTA = fechaHasta;
                    }
                    conceptoFormula.FECHA_HASTA = fechaHasta;
                }
                else
                {
                    conceptoFormula.FECHA_HASTA = null;
                }
                
                conceptoFormula.TIPO_SUELDO = dto.TipoSueldo;
              
                conceptoFormula.PORCENTAJE = dto.Porcentaje;
                conceptoFormula.PORCENTAJE_PATRONAL = dto.PorcentajePatronal;
                conceptoFormula.MONTO_TOPE = dto.MontoTope;
                conceptoFormula.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                conceptoFormula.CODIGO_EMPRESA = conectado.Empresa;
                conceptoFormula.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(conceptoFormula);
                var resultDto = await  MapRhConceptosFormulaDto(conceptoFormula);
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

        public async Task<ResultDto<RhConceptosFormulaResponseDto>> Create(RhConceptosFormulaUpdateDto dto)
        {

            ResultDto<RhConceptosFormulaResponseDto> result = new ResultDto<RhConceptosFormulaResponseDto>(null);
            try
            {
               
             

              
                var concepto = await _rhConceptosRepository.GetByCodigo(dto.CodigoConcepto);
                if (concepto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto no existe";
                    return result;
                }
                var tipoSueldo = GetListTipoSueldo().Where(x => x.Codigo== dto.TipoSueldo).FirstOrDefault();
                if (tipoSueldo==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Sueldo Invalido";
                    return result;
                    
                }
               
                
                RH_FORMULA_CONCEPTOS entity = new RH_FORMULA_CONCEPTOS();
                entity.CODIGO_FORMULA_CONCEPTO = await _repository.GetNextKey();
                entity.CODIGO_CONCEPTO = dto.CodigoConcepto;
                entity.TIPO_SUELDO = dto.TipoSueldo;
                entity.FECHA_DESDE = dto.FechaDesde;
                entity.FECHA_HASTA = dto.FechaHasta;
                entity.PORCENTAJE = dto.Porcentaje;
                entity.PORCENTAJE_PATRONAL = dto.PorcentajePatronal;
                entity.MONTO_TOPE = dto.MontoTope;
        
              
                
                entity.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRhConceptosFormulaDto(created.Data);
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
 
        public async Task<ResultDto<RhConceptosFormulaDeleteDto>> Delete(RhConceptosFormulaDeleteDto dto)
        {

            ResultDto<RhConceptosFormulaDeleteDto> result = new ResultDto<RhConceptosFormulaDeleteDto>(null);
            try
            {

                var conceptoFormula = await _repository.GetByCodigo(dto.CodigoFormulaConcepto);
                if (conceptoFormula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto Formula No existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoFormulaConcepto);

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

        public async Task<ResultDto<RhConceptosFormulaResponseDto>> GetByCodigo(RhConceptosFormulaFilterDto dto)
        { 
            ResultDto<RhConceptosFormulaResponseDto> result = new ResultDto<RhConceptosFormulaResponseDto>(null);
            try
            {

                var conceptoFormula = await _repository.GetByCodigo(dto.CodigoFormulaConcepto);
                if (conceptoFormula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto Formula no existe";
                    return result;
                }
                
                var resultDto =  await MapRhConceptosFormulaDto(conceptoFormula);
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

        public async Task<ResultDto<List<RhConceptosFormulaResponseDto>>> GetAllByConcepto(RhConceptosFormulaFilterDto dto)
        {
            ResultDto<List<RhConceptosFormulaResponseDto>> result = new ResultDto<List<RhConceptosFormulaResponseDto>>(null);
            try
            {

                var conceptoAcumula = await _repository.GetByCodigoConcepto(dto.CodigoConcepto);
                if (conceptoAcumula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListConceptosFormulaDto(conceptoAcumula);
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
        
        public async Task<ResultDto<List<RhConceptosFormulaResponseDto>>> GetAll()
        {
            ResultDto<List<RhConceptosFormulaResponseDto>> result = new ResultDto<List<RhConceptosFormulaResponseDto>>(null);
            try
            {

                var conceptoFormula = await _repository.GetByAll();
                if (conceptoFormula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListConceptosFormulaDto(conceptoFormula);
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

