namespace Convertidor.Data.Repository.Rh
{
	public class RhProcesosDetalleService:IRhProcesosDetalleService
    {
		
  



   
        private readonly IRhProcesoDetalleRepository _repository;
        private readonly IRhProcesoRepository _rhProcesoRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhTipoNominaRepository _rhTipoNominaRepository;



        public RhProcesosDetalleService(IRhProcesoDetalleRepository repository,
                                 IRhProcesoRepository rhProcesoRepository,
                                 IRhConceptosRepository rhConceptosRepository,
                                 ISisUsuarioRepository sisUsuarioRepository,
                                 IRhTipoNominaRepository rhTipoNominaRepository)
        {
            _repository = repository;
            _rhProcesoRepository = rhProcesoRepository;
            _rhConceptosRepository = rhConceptosRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhTipoNominaRepository = rhTipoNominaRepository;
        }
       
        public async Task<RH_PROCESOS_DETALLES> GetByCodigo(int codigoProcessoDetalle)
        {
            try
            {
                var proceso = await _repository.GetByCodigo(codigoProcessoDetalle);
              

                return proceso;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<List<RhProcesosDetalleResponseDtoDto>>> GetAll()
        {

            ResultDto<List<RhProcesosDetalleResponseDtoDto>> result = new ResultDto<List<RhProcesosDetalleResponseDtoDto>>(null);
            try
            {
                var procesos = await _repository.GetAll();
                var listDto = await MapListRhProcesoDetalleResponsesDto(procesos);

                result.Data = listDto;
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
        public async Task<ResultDto<List<RhProcesosDetalleResponseDtoDto>>> GetByProceso(RhProcesosDetalleFilterDtoDto filter)
        {

            ResultDto<List<RhProcesosDetalleResponseDtoDto>> result = new ResultDto<List<RhProcesosDetalleResponseDtoDto>>(null);
            try
            {
                List<RH_PROCESOS_DETALLES> procesos = new List<RH_PROCESOS_DETALLES>();
              
                if (filter.CodigoTipoNomina <= 0)
                {
                    var tipoNomina = await _rhTipoNominaRepository.GetAll();
                    if (tipoNomina != null)
                    {

                        filter.CodigoTipoNomina = tipoNomina.FirstOrDefault().CODIGO_TIPO_NOMINA;
                    }
                }
               
                procesos = await _repository.GetByCodigoProcesoTipoNomina(filter.CodigoProceso,filter.CodigoTipoNomina);
                
                var listDto = await MapListRhProcesoDetalleResponsesDto(procesos);

                result.Data = listDto;
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
        public async Task<ResultDto<List<RhProcesosDetalleResponseDtoDto>>> GetByCodigoProcesoTipoNomina(RhProcesosDetalleFilterDtoDto filter)
        {

            ResultDto<List<RhProcesosDetalleResponseDtoDto>> result = new ResultDto<List<RhProcesosDetalleResponseDtoDto>>(null);
            try
            {
                var procesos = await _repository.GetByCodigoProcesoTipoNomina(filter.CodigoProceso,filter.CodigoTipoNomina);
                var listDto = await MapListRhProcesoDetalleResponsesDto(procesos);

                result.Data = listDto;
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

        
        public async  Task<RhProcesosDetalleResponseDtoDto> MapRhProcesoDetalleResponseDto(RH_PROCESOS_DETALLES dtos)
        {
 

            RhProcesosDetalleResponseDtoDto itemResult = new RhProcesosDetalleResponseDtoDto();
                itemResult.CodigoDetalleProceso = dtos.CODIGO_DETALLE_PROCESO;
                itemResult.CodigoProceso = dtos.CODIGO_PROCESO;
                itemResult.CodigoConcepto = dtos.CODIGO_CONCEPTO;
                itemResult.DescripcionConcepto = "";
                var concepto = await _rhConceptosRepository.GetByCodigo(itemResult.CodigoConcepto);
                if (concepto != null)
                {
                    itemResult.DescripcionConcepto = concepto.DENOMINACION;
                }
                itemResult.CodigoTipoNomina = dtos.CODIGO_TIPO_NOMINA;
                itemResult.DescripcionTipoNomina = "";
                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(itemResult.CodigoTipoNomina);
                if (tipoNomina != null)
                {
                    itemResult.DescripcionTipoNomina = tipoNomina.DESCRIPCION;
                }
               
                
              
             
          
            return itemResult;



        }

        public async  Task<List<RhProcesosDetalleResponseDtoDto>> MapListRhProcesoDetalleResponsesDto(List<RH_PROCESOS_DETALLES> dtos)
        {
            List<RhProcesosDetalleResponseDtoDto> result = new List<RhProcesosDetalleResponseDtoDto>();
           
            
            foreach (var item in dtos)
            {

                RhProcesosDetalleResponseDtoDto itemResult = new RhProcesosDetalleResponseDtoDto();

                itemResult = await MapRhProcesoDetalleResponseDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
        public async Task<ResultDto<RhProcesosDetalleResponseDtoDto>> Update(RhProcesosDetalleUpdateDtoDto dto)
        {

            ResultDto<RhProcesosDetalleResponseDtoDto> result = new ResultDto<RhProcesosDetalleResponseDtoDto>(null);
            try
            {

                var procesoDetalle = await _repository.GetByCodigo(dto.CodigoProceso);
                if (procesoDetalle == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proceso Detalle no existe";
                    return result;
                }
                
                var proceso = await _rhProcesoRepository.GetByCodigo(dto.CodigoProceso);
                if (proceso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proceso no existe";
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

       
                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.CodigoTipoNomina);
                if (tipoNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Nomina no existe";
                    return result;
                }


                procesoDetalle.CODIGO_PROCESO= dto.CodigoProceso;
                procesoDetalle.CODIGO_CONCEPTO= dto.CodigoConcepto;
                procesoDetalle.CODIGO_TIPO_NOMINA= concepto.CODIGO_TIPO_NOMINA;
              
                var conectado = await _sisUsuarioRepository.GetConectado();
                procesoDetalle.CODIGO_EMPRESA = conectado.Empresa;
                procesoDetalle.USUARIO_UPD = conectado.Usuario;
                procesoDetalle.FECHA_UPD = DateTime.Now;

                await _repository.Update(procesoDetalle);

                var resultDto = await MapRhProcesoDetalleResponseDto(procesoDetalle);
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

        public async Task<ResultDto<RhProcesosDetalleResponseDtoDto>> Create(RhProcesosDetalleUpdateDtoDto dto)
        {

            ResultDto<RhProcesosDetalleResponseDtoDto> result = new ResultDto<RhProcesosDetalleResponseDtoDto>(null);
            try
            {
                
                var proceso = await _rhProcesoRepository.GetByCodigo(dto.CodigoProceso);
                if (proceso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proceso no existe";
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

       
                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.CodigoTipoNomina);
                if (tipoNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Nomina no existe";
                    return result;
                }


            
                RH_PROCESOS_DETALLES entity = new RH_PROCESOS_DETALLES();
                entity.CODIGO_DETALLE_PROCESO = await _repository.GetNextKey();
                entity.CODIGO_PROCESO= dto.CodigoProceso;
                entity.CODIGO_CONCEPTO= dto.CodigoConcepto;
                entity.CODIGO_TIPO_NOMINA= concepto.CODIGO_TIPO_NOMINA;
                entity.FECHA_INS = DateTime.Now;
             
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;


                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRhProcesoDetalleResponseDto(created.Data);
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
 
        public async Task<ResultDto<RhProcesosDetalleDeleteDtoDto>> Delete(RhProcesosDetalleDeleteDtoDto dto)
        {

            ResultDto<RhProcesosDetalleDeleteDtoDto> result = new ResultDto<RhProcesosDetalleDeleteDtoDto>(null);
            try
            {

                var proceso = await _repository.GetByCodigo(dto.CodigoDetalleProceso);
                if (proceso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proceso Detalle no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDetalleProceso);

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

        
        
    }
}

