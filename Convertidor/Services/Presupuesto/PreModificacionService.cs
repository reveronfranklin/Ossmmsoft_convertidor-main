using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreModificacionService : IPreModificacionService
    {
        private readonly IPreModificacionRepository _repository;
        private readonly IPreSolModificacionRepository _preSolModificacionRepository;
        private readonly IPRE_PRESUPUESTOSRepository _preSUPUESTOSRepository;
        private readonly IPreDescriptivaRepository _preDescriptivaRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreModificacionService(IPreModificacionRepository repository,
                                      IPreSolModificacionRepository preSolModificacionRepository,
                                      IPRE_PRESUPUESTOSRepository preSUPUESTOSRepository,
                                      IPreDescriptivaRepository preDescriptivaRepository,
                                      ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _preSolModificacionRepository = preSolModificacionRepository;
            _preSUPUESTOSRepository = preSUPUESTOSRepository;
            _preDescriptivaRepository = preDescriptivaRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PreModificacionResponseDto>>> GetAll()
        {

            ResultDto<List<PreModificacionResponseDto>> result = new ResultDto<List<PreModificacionResponseDto>>(null);
            try
            {

                var Modificacion = await _repository.GetAll();



                if (Modificacion.Count() > 0)
                {
                    List<PreModificacionResponseDto> listDto = new List<PreModificacionResponseDto>();

                    foreach (var item in Modificacion)
                    {
                        var dto = await MapPreModificacion(item);
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

        public async Task<PreModificacionResponseDto> MapPreModificacion(PRE_MODIFICACION dto)
        {
            PreModificacionResponseDto itemResult = new PreModificacionResponseDto();
            itemResult.CodigoModificacion = dto.CODIGO_MODIFICACION;
            itemResult.CodigoSolModificacion = dto.CODIGO_SOL_MODIFICACION;
            itemResult.TipoModificacionId = dto.TIPO_MODIFICACION_ID;
            
            itemResult.FechaModificacion = dto.FECHA_MODIFICACION;
            itemResult.FechaModificacionString = dto.FECHA_MODIFICACION.ToString("u");
            FechaDto fechaModificacionObj = GetFechaDto(dto.FECHA_MODIFICACION);
            itemResult.FechaModificacionObj = (FechaDto)fechaModificacionObj;
            itemResult.Ano = dto.ANO;
            itemResult.NumeroModificacion = dto.NUMERO_MODIFICACION;
            itemResult.NoResAct = dto.NO_RES_ACT;
            itemResult.CodigoOficio = dto.CODIGO_OFICIO;
            itemResult.CodigoSolicitante = dto.CODIGO_SOLICITANTE;
            itemResult.Motivo = dto.MOTIVO;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
           
            return itemResult;

        }

        public async Task<List<PreModificacionResponseDto>> MapListPreModificacionDto(List<PRE_MODIFICACION> dtos)
        {
            List<PreModificacionResponseDto> result = new List<PreModificacionResponseDto>();


            foreach (var item in dtos)
            {

                PreModificacionResponseDto itemResult = new PreModificacionResponseDto();

                itemResult = await MapPreModificacion(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PreModificacionResponseDto>> Update(PreModificacionUpdateDto dto)
        {

            ResultDto<PreModificacionResponseDto> result = new ResultDto<PreModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoModificacion = await _repository.GetByCodigo(dto.CodigoModificacion);
                if (codigoModificacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Modificacion no existe";
                    return result;
                }

                if(dto.CodigoSolModificacion < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Modificacion Invalido";
                    return result;

                }
                var codigoSolModificacion = await _preSolModificacionRepository.GetByCodigo(dto.CodigoSolModificacion);
                if (codigoSolModificacion.CODIGO_SOL_MODIFICACION < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Modificacion Invalido";
                    return result;
                }

                if (dto.TipoModificacionId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Modificacion Id Invalido";
                    return result;

                }

                var tipoModificacionId = await _preDescriptivaRepository.GetByIdAndTitulo(8,dto.TipoModificacionId);
                if (tipoModificacionId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Modificacion Id Invalido";
                    return result;
                }
                if (dto.FechaModificacion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "fecha Modificacion Invalida";
                    return result;
                }
                if (dto.Ano == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año Invalido";
                    return result;
                }

              
                if (dto.NumeroModificacion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Modificacion Invalido";
                    return result;
                }

                if (dto.NoResAct.Length > 30)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Res Act Invalido";
                    return result;
                }
                if(dto.CodigoOficio.Length > 100) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Oficio Invalido";
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

                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;

                }
                var codigoPresupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                codigoModificacion.CODIGO_MODIFICACION = dto.CodigoModificacion;
                codigoModificacion.CODIGO_SOL_MODIFICACION = dto.CodigoSolModificacion;
                codigoModificacion.TIPO_MODIFICACION_ID = dto.TipoModificacionId;
                codigoModificacion.FECHA_MODIFICACION = dto.FechaModificacion;
                codigoModificacion.ANO = dto.Ano;
                codigoModificacion.NUMERO_MODIFICACION = dto.NumeroModificacion;
                codigoModificacion.NO_RES_ACT = dto.NoResAct;
                codigoModificacion.CODIGO_OFICIO = dto.CodigoOficio;
                codigoModificacion.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                codigoModificacion.MOTIVO = dto.Motivo;
                codigoModificacion.STATUS = dto.Status;
                codigoModificacion.EXTRA1 = dto.Extra1;
                codigoModificacion.EXTRA2 = dto.Extra2;
                codigoModificacion.EXTRA3 = dto.Extra3;
                codigoModificacion.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;



                codigoModificacion.CODIGO_EMPRESA = conectado.Empresa;
                codigoModificacion.USUARIO_UPD = conectado.Usuario;
                codigoModificacion.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoModificacion);

                var resultDto = await MapPreModificacion(codigoModificacion);
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

        public async Task<ResultDto<PreModificacionResponseDto>> Create(PreModificacionUpdateDto dto)
        {

            ResultDto<PreModificacionResponseDto> result = new ResultDto<PreModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                
                if (dto.CodigoSolModificacion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Modificacion Invalido";
                    return result;

                }
                var codigoSolModificacion = await _preSolModificacionRepository.GetByCodigo(dto.CodigoSolModificacion);
                if (codigoSolModificacion.CODIGO_SOL_MODIFICACION < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Modificacion Invalido";
                    return result;
                }

                if (dto.TipoModificacionId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Modificacion Id Invalido";
                    return result;

                }

                var tipoModificacionId = await _preDescriptivaRepository.GetByIdAndTitulo(8, dto.TipoModificacionId);
                if (tipoModificacionId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Modificacion Id Invalido";
                    return result;
                }
                if (dto.FechaModificacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "fecha Modificacion Invalida";
                    return result;
                }
                if (dto.Ano == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año Invalido";
                    return result;
                }


                if (dto.NumeroModificacion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Modificacion Invalido";
                    return result;
                }

                if (dto.NoResAct.Length > 30)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Res Act Invalido";
                    return result;
                }
                if (dto.CodigoOficio.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Oficio Invalido";
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

                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;

                }
                var codigoPresupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }




                PRE_MODIFICACION entity = new PRE_MODIFICACION();
                entity.CODIGO_MODIFICACION = await _repository.GetNextKey();
                entity.CODIGO_SOL_MODIFICACION = dto.CodigoSolModificacion;
                entity.TIPO_MODIFICACION_ID = dto.TipoModificacionId;
                entity.FECHA_MODIFICACION = dto.FechaModificacion;
                entity.ANO = dto.Ano;
                entity.NUMERO_MODIFICACION = dto.NumeroModificacion;
                entity.NO_RES_ACT = dto.NoResAct;
                entity.CODIGO_OFICIO = dto.CodigoOficio;
                entity.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                entity.MOTIVO = dto.Motivo;
                entity.STATUS = dto.Status;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreModificacion(created.Data);
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

        public async Task<ResultDto<PreModificacionDeleteDto>> Delete(PreModificacionDeleteDto dto)
        {

            ResultDto<PreModificacionDeleteDto> result = new ResultDto<PreModificacionDeleteDto>(null);
            try
            {

                var codigoModificacion = await _repository.GetByCodigo(dto.CodigoModificacion);
                if (codigoModificacion == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Modificacion no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoModificacion);

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
