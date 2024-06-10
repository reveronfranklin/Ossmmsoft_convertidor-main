using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmSolCompromisoService : IAdmSolCompromisoService
    {
        private readonly IAdmSolCompromisoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;

        public AdmSolCompromisoService(IAdmSolCompromisoRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                     IAdmProveedoresRepository admProveedoresRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _admProveedoresRepository = admProveedoresRepository;
            
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
        public async Task<AdmSolCompromisoResponseDto> MapSolCompromisoDto(ADM_SOL_COMPROMISO dtos)
        {
            AdmSolCompromisoResponseDto itemResult = new AdmSolCompromisoResponseDto();
            itemResult.CodigoSolCompromiso = dtos.CODIGO_SOL_COMPROMISO;
            itemResult.TipoSolCompromisoId = dtos.TIPO_SOL_COMPROMISO_ID;
            itemResult.FechaSolicitud = dtos.FECHA_SOLICITUD;
            itemResult.FechaSolicitudString = dtos.FECHA_SOLICITUD.ToString("u");
            FechaDto fechaSolicitudObj = GetFechaDto(dtos.FECHA_SOLICITUD);
            itemResult.FechaSolicitudObj = (FechaDto)fechaSolicitudObj;
            itemResult.NumeroSolicitud = dtos.NUMERO_SOLICITUD;
            itemResult.CodigoSolicitante = dtos.CODIGO_SOLICITANTE;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.Status = dtos.STATUS;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.Ano = dtos.ANO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            
            return itemResult;
        }

        public async Task<List<AdmSolCompromisoResponseDto>> MapListSolCompromisoDto(List<ADM_SOL_COMPROMISO> dtos)
        {
            List<AdmSolCompromisoResponseDto> result = new List<AdmSolCompromisoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapSolCompromisoDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<AdmSolCompromisoResponseDto> GetByCodigo(int codigoSolCompromiso)
        {
            AdmSolCompromisoResponseDto result = new AdmSolCompromisoResponseDto();
            try
            {
                var solCompromiso = await _repository.GetByCodigo(codigoSolCompromiso);
                if(solCompromiso != null) 
                {
                  var dto = await MapSolCompromisoDto(solCompromiso);
                  result = dto;
                }
                else 
                {
                    result = null;
                }

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<List<AdmSolCompromisoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmSolCompromisoResponseDto>> result = new ResultDto<List<AdmSolCompromisoResponseDto>>(null);
            try
            {
                var solicitudCompromiso = await _repository.GetAll();
                var cant = solicitudCompromiso.Count();
                if (solicitudCompromiso != null && solicitudCompromiso.Count() > 0)
                {
                    var listDto = await MapListSolCompromisoDto(solicitudCompromiso);

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

        public async Task<ResultDto<AdmSolCompromisoResponseDto>> Update(AdmSolCompromisoUpdateDto dto)
        {
            ResultDto<AdmSolCompromisoResponseDto> result = new ResultDto<AdmSolCompromisoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var solicitudCompromiso = await _repository.GetByCodigo(dto.CodigoSolCompromiso);
                if (solicitudCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Solicitud Compromiso no existe";
                    return result;
                }

                if (dto.TipoSolCompromisoId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Solicitud Compromiso Id no existe";
                    return result;

                }
                var tipoSolicitudCompromisoId = await _admDescriptivaRepository.GetByIdAndTitulo(22, dto.TipoSolCompromisoId);
                if(tipoSolicitudCompromisoId == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Solicitud Compromiso Id no existe";
                    return result;

                }
                if (dto.FechaSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Solicitud Invalida";
                    return result;
                }
               

                if (dto.NumeroSolicitud is not null && dto.NumeroSolicitud.Length>20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
                    return result;

                }

                
                if (dto.CodigoSolicitante <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }
                var proveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.CodigoProveedor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.Motivo is not null && dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }

                if (dto.Status is not null && dto.Status.Length < 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;

                }

                if (dto.Status is not null && dto.Status.Length> 4)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
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
                if (dto.Ano <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año invalido";
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
            

                solicitudCompromiso.CODIGO_SOL_COMPROMISO = dto.CodigoSolCompromiso;
                solicitudCompromiso.ANO = dto.Ano;
                solicitudCompromiso.NUMERO_SOLICITUD = dto.NumeroSolicitud;
                solicitudCompromiso.FECHA_SOLICITUD = dto.FechaSolicitud;
                solicitudCompromiso.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                solicitudCompromiso.TIPO_SOL_COMPROMISO_ID = dto.TipoSolCompromisoId;
                solicitudCompromiso.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                solicitudCompromiso.MOTIVO = dto.Motivo;
                solicitudCompromiso.STATUS = dto.Status;
                solicitudCompromiso.EXTRA1 = dto.Extra1;
                solicitudCompromiso.EXTRA2 = dto.Extra2;
                solicitudCompromiso.EXTRA3 = dto.Extra3;
                solicitudCompromiso.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

               
                solicitudCompromiso.CODIGO_EMPRESA = conectado.Empresa;
                solicitudCompromiso.USUARIO_UPD = conectado.Usuario;
                solicitudCompromiso.FECHA_UPD = DateTime.Now;

                await _repository.Update(solicitudCompromiso);

                var resultDto = await MapSolCompromisoDto(solicitudCompromiso);
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

        public async Task<ResultDto<AdmSolCompromisoResponseDto>> Create(AdmSolCompromisoUpdateDto dto)
        {
            ResultDto<AdmSolCompromisoResponseDto> result = new ResultDto<AdmSolCompromisoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.TipoSolCompromisoId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Solicitud Compromiso Id no existe";
                    return result;

                }
                var tipoSolicitudCompromisoId = await _admDescriptivaRepository.GetByIdAndTitulo(22, dto.TipoSolCompromisoId);
                if (tipoSolicitudCompromisoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Solicitud Compromiso Id no existe";
                    return result;

                }
                if (dto.FechaSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Solicitud Invalida";
                    return result;
                }


                if (dto.NumeroSolicitud is not null && dto.NumeroSolicitud.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
                    return result;

                }


                if (dto.CodigoSolicitante <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }

                var proveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.CodigoProveedor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.Motivo is not null && dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }

                if(dto.Status is not null && dto.Status.Length < 2) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;

                }

                if (dto.Status is not null && dto.Status.Length > 4  )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
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
                if (dto.Ano <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año invalido";
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


                ADM_SOL_COMPROMISO entity = new ADM_SOL_COMPROMISO();
                entity.CODIGO_SOL_COMPROMISO = await _repository.GetNextKey();
                entity.ANO = dto.Ano;
                entity.NUMERO_SOLICITUD = dto.NumeroSolicitud;
                entity.FECHA_SOLICITUD = dto.FechaSolicitud;
                entity.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                entity.TIPO_SOL_COMPROMISO_ID = dto.TipoSolCompromisoId;
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
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
                    var resultDto = await MapSolCompromisoDto(created.Data);
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

        public async Task<ResultDto<AdmSolCompromisoDeleteDto>> Delete(AdmSolCompromisoDeleteDto dto)
        {
            ResultDto<AdmSolCompromisoDeleteDto> result = new ResultDto<AdmSolCompromisoDeleteDto>(null);
            try
            {

                var codigoSolCompromiso = await _repository.GetByCodigo(dto.CodigoSolCompromiso);
                if (codigoSolCompromiso == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Sol compromiso no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoSolCompromiso);

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

