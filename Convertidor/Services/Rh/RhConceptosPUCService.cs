using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Rh;


namespace Convertidor.Services.Adm
{
    
	public class RhConceptosPUCService: IRhConceptosPUCService
    {

      
        private readonly IRhConceptosPUCRepository _repository;
        private readonly IRhDescriptivaRepository _repositoryRhDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;
        private readonly IPRE_PLAN_UNICO_CUENTASRepository _prePlanUnicoCuentasRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;


        public RhConceptosPUCService(IRhConceptosPUCRepository repository,
                                        IRhDescriptivaRepository repositoryRhDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                        IRhConceptosRepository rhConceptosRepository,
                                        IPRE_PLAN_UNICO_CUENTASRepository prePlanUnicoCuentasRepository,
                                        IPRE_PRESUPUESTOSRepository prePresupuestosRepository)
		{
            _repository = repository;
            _repositoryRhDescriptiva = repositoryRhDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhConceptosRepository = rhConceptosRepository;
            _prePlanUnicoCuentasRepository = prePlanUnicoCuentasRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
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
       
       
        public  async Task<RhConceptosPUCResponseDto> MapRhConceptosPUCDto(RH_CONCEPTOS_PUC dtos)
        {
            RhConceptosPUCResponseDto itemResult = new RhConceptosPUCResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.CodigoConcepto = dtos.CODIGO_CONCEPTO;
                itemResult.CodigoConceptoPUC = dtos.CODIGO_CONCEPTO_PUC;
                if (dtos.ESTATUS == null) dtos.CODIGO_PUC = 0;
                itemResult.Status = dtos.ESTATUS;
                itemResult.DescripcionStatus = "";
                if (dtos.ESTATUS == 1)
                {
                    itemResult.DescripcionStatus = "Activo";
                }
                else
                {
                    itemResult.DescripcionStatus = "Inactivo";
                }
                itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
                itemResult.PresupuestoDescripcion = "";
                var conectado = await _sisUsuarioRepository.GetConectado();
                var presupuesto = await _prePresupuestosRepository.GetByCodigo( conectado.Empresa,dtos.CODIGO_PRESUPUESTO);
                if (presupuesto != null)
                {
                    itemResult.PresupuestoDescripcion = presupuesto.DENOMINACION;
                }
                itemResult.CodigoPUC = dtos.CODIGO_PUC;
                itemResult.CodigoPUCDenominacion = "";
                itemResult.CodigoPUCConcat = "";
                var puc = await _prePlanUnicoCuentasRepository.GetByCodigo( dtos.CODIGO_PUC);
                if (puc != null)
                {
                    itemResult.CodigoPUCDenominacion = puc.DENOMINACION;
                    var pucConcat = puc.CODIGO_GRUPO + "-" + puc.CODIGO_NIVEL1 + "-" +  puc.CODIGO_NIVEL2 + "-" + puc.CODIGO_NIVEL3 + "-" + puc.CODIGO_NIVEL4 + "-" + puc.CODIGO_NIVEL5 + "-" + puc.CODIGO_NIVEL6;
                    itemResult.CodigoPUCConcat = pucConcat;
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

        public async Task< List<RhConceptosPUCResponseDto>> MapListConceptosPUCDto(List<RH_CONCEPTOS_PUC> dtos)
        {
            List<RhConceptosPUCResponseDto> result = new List<RhConceptosPUCResponseDto>();
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
                        var itemResult =  await MapRhConceptosPUCDto(item);
               
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
        public async Task<ResultDto<RhConceptosPUCResponseDto?>> Update(RhConceptosPUCUpdateDto dto)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            ResultDto<RhConceptosPUCResponseDto?> result = new ResultDto<RhConceptosPUCResponseDto?>(null);
            try
            {

                var conceptoPUC = await _repository.GetByCodigo(dto.CodigoConceptoPUC);
                if (conceptoPUC == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto PUC no existe";
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
                var presupuesto = await _prePresupuestosRepository.GetByCodigo( conectado.Empresa,dto.CodigoPresupuesto);
                if (presupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }
                var puc = await _prePlanUnicoCuentasRepository.GetByCodigo(dto.CodigoPUC);
                if (puc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "PUC no existe";
                    return result;
                }
                else
                {
                    if (puc.CODIGO_PRESUPUESTO != dto.CodigoPresupuesto)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = $"Presupuesto no pertenece a el PUC {puc.DENOMINACION}";
                        return result;
                    }
                }
                
                conceptoPUC.CODIGO_PUC = dto.CodigoPUC;
                conceptoPUC.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                conceptoPUC.ESTATUS = dto.Status;
           
                conceptoPUC.FECHA_UPD = DateTime.Now;
             
                conceptoPUC.CODIGO_EMPRESA = conectado.Empresa;
                conceptoPUC.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(conceptoPUC);
                var resultDto = await  MapRhConceptosPUCDto(conceptoPUC);
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

        public async Task<ResultDto<RhConceptosPUCResponseDto>> Create(RhConceptosPUCUpdateDto dto)
        {

            ResultDto<RhConceptosPUCResponseDto> result = new ResultDto<RhConceptosPUCResponseDto>(null);
            try
            {
               
                var conectado = await _sisUsuarioRepository.GetConectado();

              
              
                var concepto = await _rhConceptosRepository.GetByCodigo(dto.CodigoConcepto);
                if (concepto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto no existe";
                    return result;
                }
                var presupuesto = await _prePresupuestosRepository.GetByCodigo( conectado.Empresa,dto.CodigoPresupuesto);
                if (presupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }
                var puc = await _prePlanUnicoCuentasRepository.GetByCodigo(dto.CodigoPUC);
                if (puc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "PUC no existe";
                    return result;
                }
                else
                {
                    if (puc.CODIGO_PRESUPUESTO != dto.CodigoPresupuesto)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = $"Presupuesto no pertenece a el PUC {puc.DENOMINACION}";
                        return result;
                    }
                }

                
                RH_CONCEPTOS_PUC entity = new RH_CONCEPTOS_PUC();
                entity.CODIGO_CONCEPTO_PUC = await _repository.GetNextKey();
                entity.CODIGO_CONCEPTO = dto.CodigoConcepto;
                entity.CODIGO_PUC = dto.CodigoPUC;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.ESTATUS = dto.Status;
              
              
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRhConceptosPUCDto(created.Data);
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
 
        public async Task<ResultDto<RhConceptosPUCDeleteDto>> Delete(RhConceptosPUCDeleteDto dto)
        {

            ResultDto<RhConceptosPUCDeleteDto> result = new ResultDto<RhConceptosPUCDeleteDto>(null);
            try
            {

                var conceptoPUC = await _repository.GetByCodigo(dto.CodigoConceptoPUC);
                if (conceptoPUC == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto PUC No existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoConceptoPUC);

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

        public async Task<ResultDto<RhConceptosPUCResponseDto>> GetByCodigo(RhConceptosPUCFilterDto dto)
        { 
            ResultDto<RhConceptosPUCResponseDto> result = new ResultDto<RhConceptosPUCResponseDto>(null);
            try
            {

                var conceptoPUC = await _repository.GetByCodigo(dto.CodigoConceptoPUC);
                if (conceptoPUC == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto PUC no existe";
                    return result;
                }
                
                var resultDto =  await MapRhConceptosPUCDto(conceptoPUC);
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

        public async Task<ResultDto<List<RhConceptosPUCResponseDto>>> GetAllByConcepto(RhConceptosPUCFilterDto dto)
        {
            ResultDto<List<RhConceptosPUCResponseDto>> result = new ResultDto<List<RhConceptosPUCResponseDto>>(null);
            try
            {

                var conceptoPUC = await _repository.GetByCodigoConcepto(dto.CodigoConcepto);
                if (conceptoPUC == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListConceptosPUCDto(conceptoPUC);
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
        
        public async Task<ResultDto<List<RhConceptosPUCResponseDto>>> GetAll()
        {
            ResultDto<List<RhConceptosPUCResponseDto>> result = new ResultDto<List<RhConceptosPUCResponseDto>>(null);
            try
            {

                var conceptoPUC = await _repository.GetByAll();
                if (conceptoPUC == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListConceptosPUCDto(conceptoPUC);
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

