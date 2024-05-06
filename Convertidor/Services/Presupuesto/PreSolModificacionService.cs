using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using NPOI.OpenXmlFormats.Vml.Office;

namespace Convertidor.Services.Presupuesto
{
	public class PreSolModificacionService: IPreSolModificacionService
    {

      
        private readonly IPreSolModificacionRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _personasRepository;
        private readonly IPreModificacionRepository _preModificacionRepository;

        public PreSolModificacionService(IPreSolModificacionRepository repository,
                                      IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                      IPreDescriptivaRepository repositoryPreDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IRhPersonasRepository personasRepository,
                                      IPreModificacionRepository preModificacionRepository
        )
		{
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;
            _personasRepository = personasRepository;
            _preModificacionRepository = preModificacionRepository;
        }


        public async Task<ResultDto<List<PreSolModificacionResponseDto>>> GetAll()
        {

            ResultDto<List<PreSolModificacionResponseDto>> result = new ResultDto<List<PreSolModificacionResponseDto>>(null);
            try
            {

                var solModificacion = await _repository.GetAll();

               

                if (solModificacion.Count() > 0)
                {
                    List<PreSolModificacionResponseDto> listDto = new List<PreSolModificacionResponseDto>();

                    foreach (var item in solModificacion)
                    {
                        var dto = await MapPreSolModificacion(item);
                        listDto.Add(dto);
                    }


                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<List<PreSolModificacionResponseDto>>>  GetByPresupuesto(FilterPresupuestoDto filter)
        {
            ResultDto<List<PreSolModificacionResponseDto>> result = new ResultDto<List<PreSolModificacionResponseDto>>(null);
            try
            {

                var solModificacion = await _repository.GetByPresupuesto(filter.CodigoPresupuesto);

               

                if (solModificacion.Count() > 0)
                {
                    List<PreSolModificacionResponseDto> listDto = new List<PreSolModificacionResponseDto>();

                    foreach (var item in solModificacion)
                    {
                        var dto = await MapPreSolModificacion(item);
                        listDto.Add(dto);
                    }


                    result.Data = listDto.OrderByDescending(x=>x.FechaSolicitud).ToList();

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }
        
        public List<StatusSolicitudModificacion> GetListStatus()
        {
            List<StatusSolicitudModificacion> result = new List<StatusSolicitudModificacion>();
            StatusSolicitudModificacion anulada = new StatusSolicitudModificacion();
            anulada.Codigo = "AN";
            anulada.Descripcion = "ANULADA";
            StatusSolicitudModificacion pendiente = new StatusSolicitudModificacion();
            pendiente.Codigo = "PE";
            pendiente.Descripcion = "PENDIENTE";
            
            StatusSolicitudModificacion aprobada = new StatusSolicitudModificacion();
            aprobada.Codigo = "AP";
            aprobada.Descripcion = "APROBADA";
            result.Add(anulada);
            result.Add(pendiente);
            result.Add(aprobada);
          
            return result;
        }
        public string GetStatus(string status)
        {
            string result = "";
            var tipoNominaObj = GetListStatus().Where(x => x.Codigo == status).First();
            result = tipoNominaObj.Descripcion;
          
            return result;
        }
        
        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);

            return FechaDesdeObj;
        }
        public string GetFechaString(DateTime? fecha)
        {
            var result = "";
            try
            {
              
                if (fecha != null)
                {
                    result = $"{fecha:MM/dd/yyyy}";
                }
         

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return result;
              
            }
           
          
        }

        
        public async Task<bool> SolicitudPuedeModificarseoEliminarse(int codigoSolicitudModificacion)
        {
            bool result = true;
            
            var preModificacion = await _preModificacionRepository.GetByCodigoSolicitud(codigoSolicitudModificacion);
            if (preModificacion != null)
            {
                result = false;
            }
            return result;
        }
        
        
        public async Task<string> GetStatusProceso(PRE_SOL_MODIFICACION dto)
        {
            var detente = 1;
            if (dto.CODIGO_SOL_MODIFICACION == 1597)
            {
                detente = 1;
            }
            var preModificacionAprobada = await _preModificacionRepository.GetByCodigoSolicitud(dto.CODIGO_SOL_MODIFICACION);
            string result = "";
            if (dto.STATUS == "AN" || ( preModificacionAprobada!=null && preModificacionAprobada.STATUS=="AN"))
            {
                var persona = await _personasRepository.GetCodigoPersona(dto.USUARIO_UPD);
                var nombrePersona = "";
                if (persona != null)
                {
                    nombrePersona = $"{persona.NOMBRE} {persona.APELLIDO}";
                }

                var fechaAnulado = GetFechaString(dto.FECHA_UPD);
                if (preModificacionAprobada != null)
                {
                    fechaAnulado = GetFechaString(preModificacionAprobada.FECHA_INS);
                }
                result=$"ANULADO POR: {nombrePersona} { fechaAnulado}";
                return result;
            }

        
            if (preModificacionAprobada != null && preModificacionAprobada.STATUS=="AP")
            {
                result = $"APROBADO MODIFICACION: #{preModificacionAprobada.CODIGO_MODIFICACION} {GetFechaString(preModificacionAprobada.FECHA_INS)}";
                return result;
            }
            
            var preModificacion = await _preModificacionRepository.GetByCodigoSolicitud(dto.CODIGO_SOL_MODIFICACION);
            if (preModificacion == null)
            {
                result = "PENDIENTE MODIFICACION";
                return result;
            }
            


            return result;
        }
        public async Task<PreSolModificacionResponseDto> MapPreSolModificacion(PRE_SOL_MODIFICACION dto)
        {
            PreSolModificacionResponseDto itemResult = new PreSolModificacionResponseDto();
            itemResult.CodigoSolModificacion = dto.CODIGO_SOL_MODIFICACION;
            itemResult.TipoModificacionId = dto.TIPO_MODIFICACION_ID;
            itemResult.DescripcionTipoModificacion = "";
            var tipoModificacionId = await _repositoryPreDescriptiva.GetByCodigo(dto.TIPO_MODIFICACION_ID);
            if (tipoModificacionId != null)
            {
                itemResult.DescripcionTipoModificacion = tipoModificacionId.DESCRIPCION;
                string[] accionList = tipoModificacionId.EXTRA3.Split(",");
                itemResult.Aportar = false;
                itemResult.Descontar = false;
                itemResult.OrigenPreSaldo = false;
                foreach (var item in accionList)
                {
                    if (item == "APORTAR")
                    {
                        itemResult.Aportar = true;
                    }
                    if (item == "DESCONTAR")
                    {
                        itemResult.Descontar = true;
                    }
                    if (item == "ORIGEN_PRESALDO")
                    {
                        itemResult.OrigenPreSaldo = true;
                    }
                    
                    
                }
            }
            itemResult.FechaSolicitud = dto.FECHA_SOLICITUD;
            itemResult.FechaSolicitudString =GetFechaString(dto.FECHA_SOLICITUD);
            FechaDto FechaSolicitudObj = GetFechaDto(dto.FECHA_SOLICITUD);
            itemResult.FechaSolicitudObj = (FechaDto)FechaSolicitudObj;
            itemResult.Ano = dto.ANO;
            itemResult.NumeroSolModificacion = dto.NUMERO_SOL_MODIFICACION;
            itemResult.CodigoOficio = dto.CODIGO_OFICIO;
            itemResult.CodigoSolicitante = dto.CODIGO_SOLICITANTE;
            itemResult.Motivo = dto.MOTIVO;
            itemResult.Status = dto.STATUS;
            itemResult.DescripcionEstatus =GetStatus(dto.STATUS);
            itemResult.NumeroCorrelativo = dto.NUMERO_CORRELATIVO;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
            itemResult.StatusProceso = await GetStatusProceso(dto);
            return itemResult;

        }


        public async Task<List<PreSolModificacionResponseDto>> MapListPreSolModificacionDto(List<PRE_SOL_MODIFICACION> dtos)
        {
            List<PreSolModificacionResponseDto> result = new List<PreSolModificacionResponseDto>();


            foreach (var item in dtos)
            {

                PreSolModificacionResponseDto itemResult = new PreSolModificacionResponseDto();

                itemResult = await MapPreSolModificacion(item);

                result.Add(itemResult);
            }
            return result;



        }
        
        
           public async Task<ResultDto<PreSolModificacionResponseDto>> UpdateStatus(int codigoSolModificacion,string status)
        {

            ResultDto<PreSolModificacionResponseDto> result = new ResultDto<PreSolModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var solModificacion = await _repository.GetByCodigo(codigoSolModificacion);
                if (solModificacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol ModificaCion no existe";
                    return result;
                }
                var findStatus=GetStatus(status);
                if (findStatus == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status no existe";
                    return result;
                }
                


                solModificacion.STATUS = status;


                solModificacion.CODIGO_EMPRESA = conectado.Empresa;
                solModificacion.USUARIO_UPD = conectado.Usuario;
                solModificacion.FECHA_UPD = DateTime.Now;
                await _repository.Update(solModificacion);

                var resultDto =await  MapPreSolModificacion(solModificacion);
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
        

        public async Task<ResultDto<PreSolModificacionResponseDto>> Update(PreSolModificacionUpdateDto dto)
        {

            ResultDto<PreSolModificacionResponseDto> result = new ResultDto<PreSolModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoSolModificacion = await _repository.GetByCodigo(dto.CodigoSolModificacion);
                if (codigoSolModificacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol ModificaCion no existe";
                    return result;
                }

               var  puedeModificarse=await SolicitudPuedeModificarseoEliminarse(dto.CodigoSolModificacion);
               if (!puedeModificarse)
               {
                   result.Data = null;
                   result.IsValid = false;
                   result.Message = "Solicitud no puede se alterada, ya existe en historico de modificacion";
                   return result;
               }
                var tipoModificacionId = await _repositoryPreDescriptiva.GetByIdAndTitulo(8, dto.TipoModificacionId);
                if (tipoModificacionId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Modificaion Id Invalido";
                    return result;
                }

             


                if (dto.NumeroSolModificacion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso Invalido";
                    return result;
                }
              


                if (dto.CodigoSolicitante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }

                if (dto.Motivo.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
                
             

                if (dto.CodigoPresupuesto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                codigoSolModificacion.CODIGO_SOL_MODIFICACION = dto.CodigoSolModificacion;
                codigoSolModificacion.TIPO_MODIFICACION_ID = dto.TipoModificacionId;
                codigoSolModificacion.FECHA_SOLICITUD = dto.FechaSolicitud;
                codigoSolModificacion.ANO =codigoPresupuesto.ANO;
                codigoSolModificacion.NUMERO_SOL_MODIFICACION = dto.NumeroSolModificacion;
                codigoSolModificacion.CODIGO_OFICIO = $"FORMA 01-{codigoPresupuesto.ANO.ToString()}";
                codigoSolModificacion.CODIGO_SOLICITANTE =dto.CodigoSolicitante;
                codigoSolModificacion.MOTIVO =dto.Motivo;
                codigoSolModificacion.NUMERO_CORRELATIVO = dto.NumeroCorrelativo;
                codigoSolModificacion.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
              


                codigoSolModificacion.CODIGO_EMPRESA = conectado.Empresa;
                codigoSolModificacion.USUARIO_UPD = conectado.Usuario;
                codigoSolModificacion.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoSolModificacion);

                var resultDto =await  MapPreSolModificacion(codigoSolModificacion);
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

        public async Task<ResultDto<PreSolModificacionResponseDto>> Create(PreSolModificacionUpdateDto dto)
        {

            ResultDto<PreSolModificacionResponseDto> result = new ResultDto<PreSolModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoSolModificacion = await _repository.GetByCodigo(dto.CodigoSolModificacion);
                if (codigoSolModificacion != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol ModificaCion no existe";
                    return result;
                }

                var tipoModificacionId = await _repositoryPreDescriptiva.GetByIdAndTitulo(8, dto.TipoModificacionId);
                if (tipoModificacionId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Modificaion Id Invalido";
                    return result;
                }
                


                if (dto.NumeroSolModificacion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso Invalido";
                    return result;
                }
              


                if (dto.CodigoSolicitante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }

                if (dto.Motivo.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }

           


                if(dto.CodigoPresupuesto <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                PRE_SOL_MODIFICACION entity = new PRE_SOL_MODIFICACION();
                entity.CODIGO_SOL_MODIFICACION = await _repository.GetNextKey();
                entity.TIPO_MODIFICACION_ID = dto.TipoModificacionId;
                entity.FECHA_SOLICITUD = dto.FechaSolicitud;
                entity.ANO = codigoPresupuesto.ANO;
                entity.NUMERO_SOL_MODIFICACION = dto.NumeroSolModificacion;
                entity.CODIGO_OFICIO = $"FORMA 01-{codigoPresupuesto.ANO.ToString()}";
                entity.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                entity.MOTIVO = dto.Motivo;
                entity.STATUS = "PE";
                entity.NUMERO_CORRELATIVO = dto.NumeroCorrelativo;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreSolModificacion(created.Data);
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
  


        public async Task<ResultDto<PreSolModificacionDeleteDto>> Delete(PreSolModificacionDeleteDto dto)
        {

            ResultDto<PreSolModificacionDeleteDto> result = new ResultDto<PreSolModificacionDeleteDto>(null);
            try
            {

                var codigoSolModificacion= await _repository.GetByCodigo(dto.CodigoSolModificacion);
                if (codigoSolModificacion == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Modificacion no existe";
                    return result;
                }
                var  puedeModificarse=await SolicitudPuedeModificarseoEliminarse(dto.CodigoSolModificacion);
                if (!puedeModificarse)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Solicitud no puede se Eliminada, ya existe en historico de modificacion";
                    return result;
                }

                var deleted = await _repository.Delete(dto.CodigoSolModificacion);

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

