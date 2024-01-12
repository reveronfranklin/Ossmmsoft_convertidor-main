﻿using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Convertidor.Utility;
using NPOI.SS.Formula.Functions;


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
                itemResult.FechaDesde = dtos.FECHA_DESDE;
                itemResult.FechaDesdeString = dtos.FECHA_DESDE.ToString("u"); 
                FechaDto FechaDesdeObj = GetFechaDto(dtos.FECHA_DESDE);
                itemResult.FechaDesdeObj = FechaDesdeObj;
                itemResult.FechaHasta = dtos.FECHA_HASTA;
                itemResult.FechaHastaString = dtos.FECHA_HASTA.ToString("u"); 
                FechaDto FechaHastaObj = GetFechaDto(dtos.FECHA_HASTA);
                itemResult.FechaHastaObj = FechaHastaObj;
                itemResult.Porcentaje = dtos.PORCENTAJE;
                itemResult.PorcentajePatronal = dtos.PORCENTAJE_PATRONAL;
                itemResult.Porcentaje = dtos.PORCENTAJE;
                itemResult.MontoTope = dtos.MONTO_TOPE;
                itemResult.TipoSueldo = dtos.TIPO_SUELDO;
                
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
      
        public List<string> GetListTipoSueldo()
        {
            List<string> result = new List<string>();
            result.Add("SB"); //SUELDO BASICO
            result.Add("SI"); // SUELDO INTEGRAL
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
                var sexo = GetListTipoSueldo().Where(x => x== dto.TipoSueldo).FirstOrDefault();
                if (String.IsNullOrEmpty(sexo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Sueldo Invalido";
                    return result;
                    
                }
                conceptoFormula.TIPO_SUELDO = dto.TipoSueldo;
                conceptoFormula.FECHA_DESDE = dto.FechaDesde;
                conceptoFormula.FECHA_HASTA = dto.FechaHasta;
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
                var sexo = GetListTipoSueldo().Where(x => x== dto.TipoSueldo).FirstOrDefault();
                if (String.IsNullOrEmpty(sexo))
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

