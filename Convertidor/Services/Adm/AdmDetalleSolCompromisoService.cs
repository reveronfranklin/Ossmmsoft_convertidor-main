using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmDetalleSolCompromisoService : IAdmDetalleSolCompromisoService
    {
        private readonly IAdmDetalleSolCompromisoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmPucSolicitudService _admPucSolicitudService;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;

        public AdmDetalleSolCompromisoService(IAdmDetalleSolCompromisoRepository repository,
                                              ISisUsuarioRepository sisUsuarioRepository,
                                              IAdmPucSolicitudService admPucSolicitudService,
                                              IAdmDescriptivaRepository admDescriptivaRepository,
                                              IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admPucSolicitudService = admPucSolicitudService;
            _admDescriptivaRepository = admDescriptivaRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
        }


        public async Task<AdmDetalleSolCompromisoResponseDto> MapDetalleSolCompromisodDto(ADM_DETALLE_SOL_COMPROMISO dtos)
        {
            AdmDetalleSolCompromisoResponseDto itemResult = new AdmDetalleSolCompromisoResponseDto();
            itemResult.CodigoDetalleSolicitud = dtos.CODIGO_DETALLE_SOLICITUD;
            itemResult.CodigoPucSolicitud = dtos.CODIGO_PUC_SOLICITUD;
            itemResult.Cantidad = dtos.CANTIDAD;
            itemResult.UdmId = dtos.UDM_ID;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.PrecioUnitario = dtos.PRECIO_UNITARIO;
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.PorImpuesto = dtos.POR_IMPUESTO;
            itemResult.CantidadAprobada = dtos.CANTIDAD_APROBADA;
            itemResult.CantidadAnulada = dtos.CANTIDAD_ANULADA;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            
            return itemResult;
        }

        public async Task<List<AdmDetalleSolCompromisoResponseDto>> MapListDetalleSolicitudDto(List<ADM_DETALLE_SOL_COMPROMISO> dtos)
        {
            List<AdmDetalleSolCompromisoResponseDto> result = new List<AdmDetalleSolCompromisoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapDetalleSolCompromisodDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }


        public async Task<ResultDto<List<AdmDetalleSolCompromisoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmDetalleSolCompromisoResponseDto>> result = new ResultDto<List<AdmDetalleSolCompromisoResponseDto>>(null);
            try
            {
                var detalleSolCompromiso = await _repository.GetAll();

                if (detalleSolCompromiso != null && detalleSolCompromiso.Count() > 0)
                {
                    var listDto = await MapListDetalleSolicitudDto(detalleSolCompromiso);

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

        public async Task<ResultDto<AdmDetalleSolCompromisoResponseDto>> Create(AdmDetalleSolCompromisoUpdateDto dto)
        {
            ResultDto<AdmDetalleSolCompromisoResponseDto> result = new ResultDto<AdmDetalleSolCompromisoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoPucSolicitud <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Puc Solicitud no existe";
                    return result;

                }

                var codigoPucSolicitud = await _admPucSolicitudService.GetByCodigo(dto.CodigoPucSolicitud);
                if (codigoPucSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud Invalido";
                    return result;
                }


                if (dto.Cantidad <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "cantidad invalida";
                    return result;
                }

                if(dto.UdmId <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "UdmId invalida";
                    return result;


                }

                var UdmId = await _admDescriptivaRepository.GetByIdAndTitulo(21,dto.UdmId);
                if(UdmId == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "UdmId invalida";
                    return result;

                }
                if (dto.Denominacion.Length > 1000 )
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion invalida";
                    return result;
                }


                if (dto.PrecioUnitario <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Precio Unitario invalido";
                    return result;
                }

                if(dto.TipoImpuestoId <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo Impuesto Id invalido";
                    return result;


                }

                var tipoImpuestoId = await _admDescriptivaRepository.GetByIdAndTitulo(18, dto.TipoImpuestoId);
                if (tipoImpuestoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo Impuesto Id invalido";
                    return result;
                }

                if (dto.PorImpuesto <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por impuesto invalido";
                    return result;
                }


                if (dto.CantidadAprobada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad aprobada invalida";
                    return result;


                }

                if (dto.CantidadAnulada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Anulada invalida";
                    return result;


                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto invalido";
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




                ADM_DETALLE_SOL_COMPROMISO entity = new ADM_DETALLE_SOL_COMPROMISO();
                entity.CODIGO_DETALLE_SOLICITUD = await _repository.GetNextKey();
                entity.CODIGO_PUC_SOLICITUD = dto.CodigoPucSolicitud;
                entity.CANTIDAD = dto.Cantidad;
                entity.UDM_ID = dto.UdmId;
                entity.DENOMINACION = dto.Denominacion;
                entity.PRECIO_UNITARIO = dto.PrecioUnitario;
                entity.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                entity.POR_IMPUESTO = dto.PorImpuesto;
                entity.CANTIDAD_APROBADA = dto.CantidadAprobada;
                entity.CANTIDAD_ANULADA = dto.CantidadAnulada;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;




                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDetalleSolCompromisodDto(created.Data);
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

        public async Task<ResultDto<AdmDetalleSolCompromisoResponseDto>> Update(AdmDetalleSolCompromisoUpdateDto dto)
        {
            ResultDto<AdmDetalleSolCompromisoResponseDto> result = new ResultDto<AdmDetalleSolCompromisoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoDetalleSolicitud = await _repository.GetByCodigo(dto.CodigoDetalleSolicitud);
                if (codigoDetalleSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Solicitud no existe";
                    return result;
                }

                if(dto.CodigoPucSolicitud <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud Invalido";
                    return result;


                }

                var codigoPucSolicitud = await _admPucSolicitudService.GetByCodigo( dto.CodigoPucSolicitud);
                if (codigoPucSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud Invalido";
                    return result;
                }

                if (dto.Cantidad <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "cantidad invalida";
                    return result;
                }

                if (dto.UdmId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "UdmId invalida";
                    return result;


                }

                var UdmId = await _admDescriptivaRepository.GetByIdAndTitulo(21, dto.UdmId);
                if (UdmId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "UdmId invalida";
                    return result;

                }
                if (dto.Denominacion.Length > 1000)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion invalida";
                    return result;
                }


                if (dto.PrecioUnitario <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Precio Unitario invalido";
                    return result;
                }

                if (dto.TipoImpuestoId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo Impuesto Id invalido";
                    return result;


                }

                var tipoImpuestoId = await _admDescriptivaRepository.GetByIdAndTitulo(18, dto.TipoImpuestoId);
                if (tipoImpuestoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo Impuesto Id invalido";
                    return result;
                }

                if (dto.PorImpuesto <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por impuesto invalido";
                    return result;
                }


                if (dto.CantidadAprobada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad aprobada invalida";
                    return result;


                }

                if (dto.CantidadAnulada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Anulada invalida";
                    return result;


                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto invalido";
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




                codigoDetalleSolicitud.CODIGO_DETALLE_SOLICITUD = dto.CodigoDetalleSolicitud;
                codigoDetalleSolicitud.CODIGO_PUC_SOLICITUD = dto.CodigoPucSolicitud;
                codigoDetalleSolicitud.CANTIDAD = dto.Cantidad;
                codigoDetalleSolicitud.UDM_ID = dto.UdmId;
                codigoDetalleSolicitud.DENOMINACION = dto.Denominacion;
                codigoDetalleSolicitud.PRECIO_UNITARIO = dto.PrecioUnitario;
                codigoDetalleSolicitud.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                codigoDetalleSolicitud.POR_IMPUESTO = dto.PorImpuesto;
                codigoDetalleSolicitud.CANTIDAD_APROBADA = dto.CantidadAprobada;
                codigoDetalleSolicitud.CANTIDAD_ANULADA = dto.CantidadAnulada;
                codigoDetalleSolicitud.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoDetalleSolicitud.EXTRA1 = dto.Extra1;
                codigoDetalleSolicitud.EXTRA2 = dto.Extra2;
                codigoDetalleSolicitud.EXTRA3 = dto.Extra3;

                codigoDetalleSolicitud.CODIGO_EMPRESA = conectado.Empresa;
                codigoDetalleSolicitud.USUARIO_UPD = conectado.Usuario;
                codigoDetalleSolicitud.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoDetalleSolicitud);

                var resultDto = await MapDetalleSolCompromisodDto(codigoDetalleSolicitud);
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



    }
}
