using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmSolicitudesService : IAdmSolicitudesService
    {
        private readonly IAdmSolicitudesRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmSolicitudesService(IAdmSolicitudesRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
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
        public async Task<AdmSolicitudesResponseDto> MapSolicitudesDto(ADM_SOLICITUDES dtos)
        {
            AdmSolicitudesResponseDto itemResult = new AdmSolicitudesResponseDto();
            itemResult.CodigoSolicitud = dtos.CODIGO_SOLICITUD;
            itemResult.Ano = dtos.ANO;
            itemResult.NumeroSolicitud = dtos.NUMERO_SOLICITUD;
            itemResult.FechaSolicitud = dtos.FECHA_SOLICITUD;
            itemResult.FechaSolicitudString = dtos.FECHA_SOLICITUD.ToString("u");
            FechaDto fechaSolicitudObj = GetFechaDto(dtos.FECHA_SOLICITUD);
            itemResult.FechaSolicitudObj = (FechaDto)fechaSolicitudObj;
            itemResult.CodigoSolicitante = dtos.CODIGO_SOLICITANTE;
            itemResult.TipoSolicitudId = dtos.TIPO_SOLICITUD_ID;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.Nota = dtos.NOTA;
            itemResult.Status = dtos.STATUS;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            return itemResult;
        }

        public async Task<List<AdmSolicitudesResponseDto>> MapListSolicitudesDto(List<ADM_SOLICITUDES> dtos)
        {
            List<AdmSolicitudesResponseDto> result = new List<AdmSolicitudesResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapSolicitudesDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmSolicitudesResponseDto>>> GetAll()
        {

            ResultDto<List<AdmSolicitudesResponseDto>> result = new ResultDto<List<AdmSolicitudesResponseDto>>(null);
            try
            {
                var solicitudes = await _repository.GetAll();
                var cant = solicitudes.Count();
                if (solicitudes != null && solicitudes.Count() > 0)
                {
                    var listDto = await MapListSolicitudesDto(solicitudes);

                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No data";

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }

        public async Task<ResultDto<AdmSolicitudesResponseDto>> Update(AdmSolicitudesUpdateDto dto)
        {
            ResultDto<AdmSolicitudesResponseDto> result = new ResultDto<AdmSolicitudesResponseDto>(null);
            try
            {
                var solicitud = await _repository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (solicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Solicitud no existe";
                    return result;
                }
                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año invalido";
                    return result;
                }

                if (dto.NumeroSolicitud is not null && dto.NumeroSolicitud.Length>20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
                    return result;

                }

                if (dto.FechaSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Solicitud Invalida";
                    return result;
                }
                if (dto.CodigoSolicitante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }

                var tipoSolicitud = await _admDescriptivaRepository.GetByTitulo(35);
                if (dto.TipoSolicitudId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Solicitud Id no existe";
                    return result;

                }

                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.Motivo is not null && dto.Motivo.Length>1150)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
                if (dto.Nota is not null && dto.Nota.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nota invalida";
                    return result;
                }

                if (dto.Status is not null && dto.Status.Length>2)
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

                solicitud.CODIGO_SOLICITUD = dto.CodigoSolicitud;
                solicitud.ANO = dto.Ano;
                solicitud.NUMERO_SOLICITUD = dto.NumeroSolicitud;
                solicitud.FECHA_SOLICITUD = dto.FechaSolicitud;
                solicitud.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                solicitud.TIPO_SOLICITUD_ID = dto.TipoSolicitudId;
                solicitud.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                solicitud.MOTIVO = dto.Motivo;
                solicitud.NOTA = dto.Nota;
                solicitud.STATUS = dto.Status;
                solicitud.EXTRA1 = dto.Extra1;
                solicitud.EXTRA2 = dto.Extra2;
                solicitud.EXTRA3 = dto.Extra3;
                solicitud.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

                var conectado = await _sisUsuarioRepository.GetConectado();
                solicitud.CODIGO_EMPRESA = conectado.Empresa;
                solicitud.USUARIO_UPD = conectado.Usuario;
                solicitud.FECHA_UPD = DateTime.Now;

                await _repository.Update(solicitud);

                var resultDto = await MapSolicitudesDto(solicitud);
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

        public async Task<ResultDto<AdmSolicitudesResponseDto>> Create(AdmSolicitudesUpdateDto dto)
        {
            ResultDto<AdmSolicitudesResponseDto> result = new ResultDto<AdmSolicitudesResponseDto>(null);
            try
            {
                var codigoSolicitud = await _repository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (codigoSolicitud != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Solicitud ya existe";
                    return result;
                }
                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año invalido";
                    return result;
                }

                if (dto.NumeroSolicitud is not null && dto.NumeroSolicitud.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
                    return result;

                }

                if (dto.FechaSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Solicitud Invalida";
                    return result;
                }
                if (dto.CodigoSolicitante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }

                var tipoSolicitud = await _admDescriptivaRepository.GetByTitulo(35);
                if (dto.TipoSolicitudId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Solicitud Id no existe";
                    return result;

                }

                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.Motivo is not null && dto.Motivo.Length > 1150)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
                if (dto.Nota is not null && dto.Nota.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nota invalida";
                    return result;
                }

                if (dto.Status is not null && dto.Status.Length > 2)
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


                ADM_SOLICITUDES entity = new ADM_SOLICITUDES();
            entity.CODIGO_SOLICITUD = await _repository.GetNextKey();
            entity.ANO = dto.Ano;
            entity.NUMERO_SOLICITUD=dto.NumeroSolicitud;
            entity.FECHA_SOLICITUD = dto.FechaSolicitud;
            entity.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
            entity.TIPO_SOLICITUD_ID = dto.TipoSolicitudId;
            entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
            entity.MOTIVO = dto.Motivo;
            entity.NOTA=dto.Nota;
            entity.STATUS = dto.Status;
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


            var conectado = await _sisUsuarioRepository.GetConectado();
            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapSolicitudesDto(created.Data);
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

        public async Task<ResultDto<AdmSolicitudesDeleteDto>> Delete(AdmSolicitudesDeleteDto dto) 
        {
            ResultDto<AdmSolicitudesDeleteDto> result = new ResultDto<AdmSolicitudesDeleteDto>(null);
            try
            {

                var codigoSolicitud = await _repository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (codigoSolicitud == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoSolicitud);

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

