namespace Convertidor.Data.Repository.Rh
{
	public class RhProcesosService: IRhProcesosService
    {
		
        
        private readonly IRhProcesoRepository _repository;
        private readonly IRhProcesoDetalleRepository _rhProcesoDetalleRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

 

        public RhProcesosService(IRhProcesoRepository repository,
                                 IRhProcesoDetalleRepository rhProcesoDetalleRepository,
                                 IRhConceptosRepository rhConceptosRepository,
                                 ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _rhProcesoDetalleRepository = rhProcesoDetalleRepository;
            _rhConceptosRepository = rhConceptosRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        public async Task<RH_PROCESOS> GetByCodigo(int codigoProcesso)
        {
            try
            {
                var proceso = await _repository.GetByCodigo(codigoProcesso);
              

                return proceso;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<List<RhProcesosDto>>> GetAll()
        {

            ResultDto<List<RhProcesosDto>> result = new ResultDto<List<RhProcesosDto>>(null);
            try
            {
                var procesos = await _repository.GetAll();
                var listDto = await MapListProceso(procesos);

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
        
        public async Task<ResultDto<List<RhProcesosDto>>> GetByProceso(RhProcesosFilterDtoDto filter)
        {

            ResultDto<List<RhProcesosDto>> result = new ResultDto<List<RhProcesosDto>>(null);
            try
            {
                var proceso = await _repository.GetByCodigo(filter.CodigoProceso);
                List<RH_PROCESOS> procesos = new List<RH_PROCESOS>();
                procesos.Add(proceso);
                var listDto = await MapListProceso(procesos);

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

        public async Task<ResultDto<List<RhProcesosResponseDtoDto>>> GetAllRhProcesoResponseDto()
        {

            ResultDto<List<RhProcesosResponseDtoDto>> result = new ResultDto<List<RhProcesosResponseDtoDto>>(null);
            try
            {
                var procesos = await _repository.GetAll();
                var listDto = await MapListRhProcesoResponsesDto(procesos);

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
        private async Task<List<RhProcesosDto>> MapListProceso(List<RH_PROCESOS> dto)
        {

            List<RhProcesosDto> result = new List<RhProcesosDto>();
         
            try
            {
                var resultNew = dto

                 .Select(e => new RhProcesosDto
                 {
                     CodigoProceso = e.CODIGO_PROCESO,
                     Descripcion = e.DESCRIPCION,
               

                 });
                result = resultNew.ToList();

                foreach (var item in result)
                {
                    var detalleConceptos = await _rhProcesoDetalleRepository.GetByCodigoProceso(item.CodigoProceso);
                    if (detalleConceptos.Count > 0)
                    {
                        List<ListConceptosDto> listConceptosDto = new List<ListConceptosDto>();
                        foreach (var itemDetalle in detalleConceptos)
                        {
                            var concepto = await _rhConceptosRepository.GetByCodigoTipoNomina(itemDetalle.CODIGO_CONCEPTO, itemDetalle.CODIGO_TIPO_NOMINA);
                            ListConceptosDto itemlistConceptosDto = new ListConceptosDto();
                            itemlistConceptosDto.Codigo = concepto.CODIGO;
                            itemlistConceptosDto.CodigoConcepto = concepto.CODIGO_CONCEPTO;
                            itemlistConceptosDto.CodigoTipoNomina = concepto.CODIGO_TIPO_NOMINA;
                            itemlistConceptosDto.Denominacion = concepto.DENOMINACION;

                            listConceptosDto.Add(itemlistConceptosDto);

                        }
                        item.Conceptos = listConceptosDto;
                    }
                }


               

                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }







        }

        
        
          public async  Task<RhProcesosResponseDtoDto> MapRhProcesoResponseDto(RH_PROCESOS dtos)
        {
 

                RhProcesosResponseDtoDto itemResult = new RhProcesosResponseDtoDto();
                itemResult.CodigoProceso = dtos.CODIGO_PROCESO;
                itemResult.Descripcion = dtos.DESCRIPCION;
              
             
          
            return itemResult;



        }

        public async  Task<List<RhProcesosResponseDtoDto>> MapListRhProcesoResponsesDto(List<RH_PROCESOS> dtos)
        {
            List<RhProcesosResponseDtoDto> result = new List<RhProcesosResponseDtoDto>();
           
            
            foreach (var item in dtos)
            {

                RhProcesosResponseDtoDto itemResult = new RhProcesosResponseDtoDto();

                itemResult = await MapRhProcesoResponseDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
        public async Task<ResultDto<RhProcesosResponseDtoDto>> Update(RhProcesosUpdateDtoDto dto)
        {

            ResultDto<RhProcesosResponseDtoDto> result = new ResultDto<RhProcesosResponseDtoDto>(null);
            try
            {

                var proceso = await _repository.GetByCodigo(dto.CodigoProceso);
                if (proceso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proceso no existe";
                    return result;
                }
                if (dto.Descripcion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

                
              
              
              

                proceso.DESCRIPCION= dto.Descripcion;
              
                var conectado = await _sisUsuarioRepository.GetConectado();
                proceso.CODIGO_EMPRESA = conectado.Empresa;
                proceso.USUARIO_UPD = conectado.Usuario;
                proceso.FECHA_UPD = DateTime.Now;

                await _repository.Update(proceso);

        
                
                var resultDto = await MapRhProcesoResponseDto(proceso);
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

        public async Task<ResultDto<RhProcesosResponseDtoDto>> Create(RhProcesosUpdateDtoDto dto)
        {

            ResultDto<RhProcesosResponseDtoDto> result = new ResultDto<RhProcesosResponseDtoDto>(null);
            try
            {
                if (dto.Descripcion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

            
                RH_PROCESOS entity = new RH_PROCESOS();
                entity.CODIGO_PROCESO = await _repository.GetNextKey();
                entity.DESCRIPCION = dto.Descripcion;
                entity.FECHA_INS = DateTime.Now;
             
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS=DateTime.Now;


                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRhProcesoResponseDto(created.Data);
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
 
        public async Task<ResultDto<RhProcesosDeleteDtoDto>> Delete(RhProcesosDeleteDtoDto dto)
        {

            ResultDto<RhProcesosDeleteDtoDto> result = new ResultDto<RhProcesosDeleteDtoDto>(null);
            try
            {

                var proceso = await _repository.GetByCodigo(dto.CodigoProceso);
                if (proceso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proceso no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoProceso);

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

