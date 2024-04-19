using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Adm;

namespace Convertidor.Services.Presupuesto
{
	public class PreSolModificacionService: IPreSolModificacionService
    {

      
        private readonly IPreSolModificacionRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        public PreSolModificacionService(IPreSolModificacionRepository repository,
                                      IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                      IPreDescriptivaRepository repositoryPreDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository
        )
		{
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;
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

        public async Task<PreSolModificacionResponseDto> MapPreSolModificacion(PRE_SOL_MODIFICACION dto)
        {
            PreSolModificacionResponseDto itemResult = new PreSolModificacionResponseDto();
            itemResult.CodigoSolModificacion = dto.CODIGO_SOL_MODIFICACION;
            itemResult.TipoModificacionId = dto.TIPO_MODIFICACION_ID;
            itemResult.FechaSolicitud = dto.FECHA_SOLICITUD;
            itemResult.FechaSolicitudString = dto.FECHA_SOLICITUD.ToString("u");
            FechaDto FechaSolicitudObj = GetFechaDto(dto.FECHA_SOLICITUD);
            itemResult.FechaSolicitudObj = (FechaDto)FechaSolicitudObj;
            itemResult.Ano = dto.ANO;
            itemResult.NumeroSolModificacion = dto.NUMERO_SOL_MODIFICACION;
            itemResult.CodigoOficio = dto.CODIGO_OFICIO;
            itemResult.CodigoSolicitante = dto.CODIGO_SOLICITANTE;
            itemResult.Motivo = dto.MOTIVO;
            itemResult.Status = dto.STATUS;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.NumeroCorrelativo = dto.NUMERO_CORRELATIVO;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
       

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

                var tipoModificacionId = await _repositoryPreDescriptiva.GetByIdAndTitulo(8, dto.TipoModificacionId);
                if (dto.TipoModificacionId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Modificaion Id Invalido";
                    return result;
                }

                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año Invalido";
                    return result;
                }

              
                if (dto.NumeroSolModificacion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso Invalido";
                    return result;
                }
                if (dto.CodigoOficio.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Oficio Invalido";
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

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }
                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

               
                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                codigoSolModificacion.CODIGO_SOL_MODIFICACION = dto.CodigoSolModificacion;
                codigoSolModificacion.TIPO_MODIFICACION_ID = dto.TipoModificacionId;
                codigoSolModificacion.FECHA_SOLICITUD = dto.FechaSolicitud;
                codigoSolModificacion.ANO = dto.Ano;
                codigoSolModificacion.NUMERO_SOL_MODIFICACION = dto.NumeroSolModificacion;
                codigoSolModificacion.CODIGO_OFICIO = dto.CodigoOficio;
                codigoSolModificacion.CODIGO_SOLICITANTE =dto.CodigoSolicitante;
                codigoSolModificacion.MOTIVO =dto.Motivo;
                codigoSolModificacion.STATUS = dto.Status;
                codigoSolModificacion.EXTRA1 = dto.Extra1;
                codigoSolModificacion.EXTRA2 = dto.Extra2;
                codigoSolModificacion.EXTRA3 = dto.Extra3;
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
                if (dto.TipoModificacionId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Modificaion Id Invalido";
                    return result;
                }

                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año Invalido";
                    return result;
                }


                if (dto.NumeroSolModificacion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso Invalido";
                    return result;
                }
                if (dto.CodigoOficio.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Oficio Invalido";
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

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }
                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }


                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
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
                entity.ANO = dto.Ano;
                entity.NUMERO_SOL_MODIFICACION = dto.NumeroSolModificacion;
                entity.CODIGO_OFICIO = dto.CodigoOficio;
                entity.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                entity.MOTIVO = dto.Motivo;
                entity.STATUS = dto.Status;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
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

