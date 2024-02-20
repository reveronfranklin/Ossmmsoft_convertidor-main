using System.Globalization;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;
using NPOI.SS.Formula.Functions;


namespace Convertidor.Services.Adm
{
    
	public class RhConceptosAcumuladoService: IRhConceptosAcumuladoService
    {

      
        private readonly IRhConceptosAcumulaRepository _repository;
        private readonly IRhDescriptivaRepository _repositoryRhDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;


        public RhConceptosAcumuladoService(IRhConceptosAcumulaRepository repository,
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
       
       
        public  async Task<RhConceptosAcumulaResponseDto> MapRhConceptosAcumulaDto(RH_CONCEPTOS_ACUMULA dtos)
        {
            RhConceptosAcumulaResponseDto itemResult = new RhConceptosAcumulaResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.CodigoConcepto = dtos.CODIGO_CONCEPTO;
                itemResult.CodigoConceptoAcumula = dtos.CODIGO_CONCEPTO_ACUMULA;
                itemResult.FechaDesde = dtos.FECHA_DESDE;
                itemResult.FechaDesdeString = dtos.FECHA_DESDE.ToString("u"); 
                FechaDto FechaDesdeObj = GetFechaDto(dtos.FECHA_DESDE);
                itemResult.FechaDesdeObj = FechaDesdeObj;
                if (dtos.FECHA_HASTA != null)
                {
                    itemResult.FechaHasta = (DateTime)dtos.FECHA_HASTA;
                    itemResult.FechaHastaString =  itemResult.FechaHasta.ToString("u"); 
                    FechaDto FechaHastaObj = GetFechaDto((DateTime)dtos.FECHA_HASTA);
                    itemResult.FechaHastaObj = FechaHastaObj;
                }
            
                itemResult.TipoAcumuladoId = dtos.TIPO_ACUMULADO_ID;
                itemResult.TipoAcumuladoDescripcion = "";
                int tipoAcumuladoId = Int32.Parse(dtos.TIPO_ACUMULADO_ID);
                var tipoAcumulado = await _repositoryRhDescriptiva.GetByCodigoDescriptiva(tipoAcumuladoId);
                if (tipoAcumulado != null)
                {
                    itemResult.TipoAcumuladoDescripcion = tipoAcumulado.DESCRIPCION;
                }
                
                itemResult.CodigoConceptoAsociado = dtos.CODIGO_CONCEPTO_ASOCIADO;
                itemResult.CodigoConceptoAsociadoDescripcion = "";
                var conceptoAsociado = await _rhConceptosRepository.GetByCodigo(dtos.CODIGO_CONCEPTO_ASOCIADO);
                if (conceptoAsociado != null)
                {
                    itemResult.CodigoConceptoAsociadoDescripcion = conceptoAsociado.DESCRIPCION;
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

        public async Task< List<RhConceptosAcumulaResponseDto>> MapListConceptosAcumulaDto(List<RH_CONCEPTOS_ACUMULA> dtos)
        {
            List<RhConceptosAcumulaResponseDto> result = new List<RhConceptosAcumulaResponseDto>();
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
                        var itemResult =  await MapRhConceptosAcumulaDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }

        
        public async Task<ResultDto<RhConceptosAcumulaResponseDto?>> Update(RhConceptosAcumulaUpdateDto dto)
        {

            ResultDto<RhConceptosAcumulaResponseDto?> result = new ResultDto<RhConceptosAcumulaResponseDto?>(null);
            try
            {

                var conceptoAcumula = await _repository.GetByCodigo(dto.CodigoConceptoAcumula);
                if (conceptoAcumula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto Acumula no existe";
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

                int tipoAcumuladoId = dto.TipoAcumuladoId;
                var tipoAcumulado = await _repositoryRhDescriptiva.GetByIdAndTitulo(36,tipoAcumuladoId);
                if (tipoAcumulado==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Acumulado  Invalido";
                    return result;
                }

                if (dto.CodigoConceptoAsociado != null && dto.CodigoConceptoAsociado > 0)
                {
                    var conceptoAsociado = await _rhConceptosRepository.GetByCodigo(dto.CodigoConcepto);
                    if (conceptoAsociado==null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Concepto Asociado  Invalido";
                        return result;
                    }
                }
                if (!DateValidate.IsDate(dto.FechaDesdeString))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha desde No VAlida";
                    return result;
                }
                conceptoAcumula.TIPO_ACUMULADO_ID = dto.TipoAcumuladoId.ToString();
                
            
       
                if (DateValidate.IsDate(dto.FechaDesdeString))
                {
                    var fechaDesde = Convert.ToDateTime(dto.FechaDesdeString, CultureInfo.InvariantCulture);
              
                    conceptoAcumula.FECHA_DESDE = fechaDesde;
                }
                
                if (DateValidate.IsDate(dto.FechaHastaString))
                {
                    var fechaHasta = Convert.ToDateTime(dto.FechaHastaString, CultureInfo.InvariantCulture);
                    if (fechaHasta.Year <= 1900)
                    {
                        conceptoAcumula.FECHA_HASTA = null;
                    }
                    else
                    {
                        conceptoAcumula.FECHA_HASTA = fechaHasta;
                    }
                    conceptoAcumula.FECHA_HASTA = fechaHasta;
                }
                else
                {
                    conceptoAcumula.FECHA_HASTA = null;
                }
                
                conceptoAcumula.CODIGO_CONCEPTO_ASOCIADO = dto.CodigoConceptoAsociado;
               
              
                conceptoAcumula.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                conceptoAcumula.CODIGO_EMPRESA = conectado.Empresa;
                conceptoAcumula.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(conceptoAcumula);
                var resultDto = await  MapRhConceptosAcumulaDto(conceptoAcumula);
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

        public async Task<ResultDto<RhConceptosAcumulaResponseDto>> Create(RhConceptosAcumulaUpdateDto dto)
        {

            ResultDto<RhConceptosAcumulaResponseDto> result = new ResultDto<RhConceptosAcumulaResponseDto>(null);
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

                int tipoAcumuladoId = dto.TipoAcumuladoId;
                var tipoAcumulado = await _repositoryRhDescriptiva.GetByIdAndTitulo(36,tipoAcumuladoId);
                if (tipoAcumulado==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Acumulado  Invalido";
                    return result;
                }

                if (dto.CodigoConceptoAsociado != null && dto.CodigoConceptoAsociado > 0)
                {
                    var conceptoAsociado = await _rhConceptosRepository.GetByCodigo(dto.CodigoConcepto);
                    if (conceptoAsociado==null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Concepto Asociado  Invalido";
                        return result;
                    }
                }
                if (!DateValidate.IsDate(dto.FechaDesdeString))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha desde No VAlida";
                    return result;
                }
                
                
                RH_CONCEPTOS_ACUMULA entity = new RH_CONCEPTOS_ACUMULA();
                entity.CODIGO_CONCEPTO = dto.CodigoConcepto;
                entity.CODIGO_CONCEPTO_ACUMULA = await _repository.GetNextKey();
                entity.TIPO_ACUMULADO_ID = dto.TipoAcumuladoId.ToString();
                if (DateValidate.IsDate(dto.FechaDesdeString))
                {
                    var fechaDesde = Convert.ToDateTime(dto.FechaDesdeString, CultureInfo.InvariantCulture);
              
                    entity.FECHA_DESDE = fechaDesde;
                }
                if (DateValidate.IsDate(dto.FechaHastaString))
                {
                    var fechaHasta = Convert.ToDateTime(dto.FechaHastaString, CultureInfo.InvariantCulture);
                    if (fechaHasta.Year <= 1900)
                    {
                        entity.FECHA_HASTA = null;
                    }
                    else
                    {
                        entity.FECHA_HASTA = fechaHasta;
                    }
                    entity.FECHA_HASTA = fechaHasta;
                }
                else
                {
                    entity.FECHA_HASTA = null;
                }
            
                
              
                entity.CODIGO_CONCEPTO_ASOCIADO = dto.CodigoConceptoAsociado;
                
                entity.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRhConceptosAcumulaDto(created.Data);
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
 
        public async Task<ResultDto<RhConceptosAcumulaDeleteDto>> Delete(RhConceptosAcumulaDeleteDto dto)
        {

            ResultDto<RhConceptosAcumulaDeleteDto> result = new ResultDto<RhConceptosAcumulaDeleteDto>(null);
            try
            {

                var conceptoAcumula = await _repository.GetByCodigo(dto.CodigoConceptoAcumula);
                if (conceptoAcumula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Actividad no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoConceptoAcumula);

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

        public async Task<ResultDto<RhConceptosAcumulaResponseDto>> GetByCodigo(RhConceptosAcumulaFilterDto dto)
        { 
            ResultDto<RhConceptosAcumulaResponseDto> result = new ResultDto<RhConceptosAcumulaResponseDto>(null);
            try
            {

                var conceptoAcumula = await _repository.GetByCodigo(dto.CodigoConceptoAcumula);
                if (conceptoAcumula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto Acumulado no existe";
                    return result;
                }
                
                var resultDto =  await MapRhConceptosAcumulaDto(conceptoAcumula);
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

        public async Task<ResultDto<List<RhConceptosAcumulaResponseDto>>> GetAllByConcepto(RhConceptosAcumulaFilterDto dto)
        {
            ResultDto<List<RhConceptosAcumulaResponseDto>> result = new ResultDto<List<RhConceptosAcumulaResponseDto>>(null);
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
                 
                var resultDto = await  MapListConceptosAcumulaDto(conceptoAcumula);
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
        
        public async Task<ResultDto<List<RhConceptosAcumulaResponseDto>>> GetAll()
        {
            ResultDto<List<RhConceptosAcumulaResponseDto>> result = new ResultDto<List<RhConceptosAcumulaResponseDto>>(null);
            try
            {

                var conceptoAcumula = await _repository.GetByAll();
                if (conceptoAcumula == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListConceptosAcumulaDto(conceptoAcumula);
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

