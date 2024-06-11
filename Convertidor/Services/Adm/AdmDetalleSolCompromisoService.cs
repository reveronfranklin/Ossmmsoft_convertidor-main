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
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;


        public AdmDetalleSolCompromisoService(IAdmDetalleSolCompromisoRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;

            
        }

      
        public async Task<AdmDetalleSolCompromisoResponseDto> MapDetalleSolCompromisoDto(ADM_DETALLE_SOL_COMPROMISO dtos)
        {
            AdmDetalleSolCompromisoResponseDto itemResult = new AdmDetalleSolCompromisoResponseDto();
            itemResult.CodigoDetalleSolicitud = dtos.CODIGO_DETALLE_SOLICITUD;
            itemResult.CodigoPucSolicitud = dtos.CODIGO_PUC_SOLICITUD;
            itemResult.Cantidad = dtos.CANTIDAD;
            itemResult.UdmId = dtos.UDM_ID;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.PrecioUnitario = dtos.PRECIO_UNITARIO;
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.PORIMPUESTO = dtos.POR_IMPUESTO;
            itemResult.CantidadAprobada = dtos.CANTIDAD_APROBADA;
            itemResult.CantidadAnulada = dtos.CANTIDAD_ANULADA;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;


            return itemResult;
        }

        public async Task<List<AdmDetalleSolCompromisoResponseDto>> MapListDetalleSolCompromisoDto(List<ADM_DETALLE_SOL_COMPROMISO> dtos)
        {
            List<AdmDetalleSolCompromisoResponseDto> result = new List<AdmDetalleSolCompromisoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapDetalleSolCompromisoDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<AdmDetalleSolCompromisoResponseDto> GetByCodigo(int codigoDetalleSolicitud)
        {
            AdmDetalleSolCompromisoResponseDto result = new AdmDetalleSolCompromisoResponseDto();
            try
            {
                var detalleSolCompromiso = await _repository.GetByCodigo(codigoDetalleSolicitud);
                if(detalleSolCompromiso != null) 
                {
                  var dto = await MapDetalleSolCompromisoDto(detalleSolCompromiso);
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

        public async Task<ResultDto<List<AdmDetalleSolCompromisoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmDetalleSolCompromisoResponseDto>> result = new ResultDto<List<AdmDetalleSolCompromisoResponseDto>>(null);
            try
            {
                var detalleSolicitudCompromiso = await _repository.GetAll();
                var cant = detalleSolicitudCompromiso.Count();
                if (detalleSolicitudCompromiso != null && detalleSolicitudCompromiso.Count() > 0)
                {
                    var listDto = await MapListDetalleSolCompromisoDto(detalleSolicitudCompromiso);

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

        public async Task<ResultDto<AdmDetalleSolCompromisoResponseDto>> Update(AdmDetalleSolCompromisoUpdateDto dto)
        {
            ResultDto<AdmDetalleSolCompromisoResponseDto> result = new ResultDto<AdmDetalleSolCompromisoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                var detalleSolicitudCompromiso = await _repository.GetByCodigo(dto.CodigoDetalleSolicitud);
                if (detalleSolicitudCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Detalle Solicitud Compromiso no existe";
                    return result;
                }

                if (dto.CodigoPucSolicitud <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Puc Solicitud Id no existe";
                    return result;

                }
               
                if (dto.Cantidad <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "cantidad Invalida";
                    return result;
                }

                if (dto.UdmId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad de medida invalida";
                    return result;

                }

                var udmId = await _admDescriptivaRepository.GetByIdAndTitulo(21, dto.UdmId);
                if(udmId== false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad de medida invalida";
                    return result;

                }
              
                if (dto.Denominacion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }
                

                if (dto.PrecioUnitario <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Precio unitario Invalido";
                    return result;
                }

                if (dto.TipoImpuestoId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Impuesto Invalido";
                    return result;
                }
                var tipoImpuestoId = await _admDescriptivaRepository.GetByIdAndTitulo(18,dto.TipoImpuestoId);
                if(tipoImpuestoId== false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Impuesto Invalido";
                    return result;

                }

                if (dto.PORIMPUESTO < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por Impuesto Invalido";
                    return result;

                }

                if (dto.CantidadAprobada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "cantidad Invalida";
                    return result;
                }

                if (dto.CantidadAnulada < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Anulada Invalida";
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

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }


                detalleSolicitudCompromiso.CODIGO_DETALLE_SOLICITUD = dto.CodigoDetalleSolicitud;
                detalleSolicitudCompromiso.CODIGO_PUC_SOLICITUD = dto.CodigoPucSolicitud;
                detalleSolicitudCompromiso.CANTIDAD = dto.Cantidad;
                detalleSolicitudCompromiso.UDM_ID = dto.UdmId;
                detalleSolicitudCompromiso.DENOMINACION = dto.Denominacion;
                detalleSolicitudCompromiso.PRECIO_UNITARIO = dto.PrecioUnitario;
                detalleSolicitudCompromiso.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                detalleSolicitudCompromiso.POR_IMPUESTO = dto.PORIMPUESTO;
                detalleSolicitudCompromiso.CANTIDAD_APROBADA = dto.CantidadAprobada;
                detalleSolicitudCompromiso.CANTIDAD_ANULADA = dto.CantidadAnulada;
                detalleSolicitudCompromiso.EXTRA1 = dto.Extra1;
                detalleSolicitudCompromiso.EXTRA2 = dto.Extra2;
                detalleSolicitudCompromiso.EXTRA3 = dto.Extra3;
                detalleSolicitudCompromiso.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


                detalleSolicitudCompromiso.CODIGO_EMPRESA = conectado.Empresa;
                detalleSolicitudCompromiso.USUARIO_UPD = conectado.Usuario;
                detalleSolicitudCompromiso.FECHA_UPD = DateTime.Now;

                await _repository.Update(detalleSolicitudCompromiso);

                var resultDto = await MapDetalleSolCompromisoDto(detalleSolicitudCompromiso);
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
                    result.Message = "codigo Puc Solicitud Id no existe";
                    return result;

                }

                if (dto.Cantidad <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "cantidad Invalida";
                    return result;
                }


                if (dto.UdmId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad de medida invalida";
                    return result;

                }
                var udmId = await _admDescriptivaRepository.GetByIdAndTitulo(21, dto.UdmId);
                if (udmId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad de medida invalida";
                    return result;

                }
               


                if (dto.Denominacion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }


                if (dto.PrecioUnitario <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Precio unitario Invalido";
                    return result;
                }

                if (dto.TipoImpuestoId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Impuesto Invalido";
                    return result;
                }

                var tipoImpuestoId = await _admDescriptivaRepository.GetByIdAndTitulo(18, dto.TipoImpuestoId);
                if (tipoImpuestoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Impuesto Invalido";
                    return result;

                }

                

                if (dto.PORIMPUESTO < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por Impuesto Invalido";
                    return result;

                }

                if (dto.CantidadAprobada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "cantidad Invalida";
                    return result;
                }

                if (dto.CantidadAnulada < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Anulada Invalida";
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

                if(dto.CodigoPresupuesto < 0) 
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


                ADM_DETALLE_SOL_COMPROMISO entity = new ADM_DETALLE_SOL_COMPROMISO();
                entity.CODIGO_DETALLE_SOLICITUD = await _repository.GetNextKey();
                entity.CODIGO_PUC_SOLICITUD = dto.CodigoPucSolicitud;
                entity.CANTIDAD = dto.Cantidad;
                entity.UDM_ID = dto.UdmId;
                entity.DENOMINACION = dto.Denominacion;
                entity.PRECIO_UNITARIO = dto.PrecioUnitario;
                entity.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                entity.POR_IMPUESTO = dto.PORIMPUESTO;
                entity.CANTIDAD_APROBADA = dto.CantidadAprobada;
                entity.CANTIDAD_ANULADA = dto.CantidadAnulada;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;



                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDetalleSolCompromisoDto(created.Data);
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

        public async Task<ResultDto<AdmDetalleSolCompromisoDeleteDto>> Delete(AdmDetalleSolCompromisoDeleteDto dto)
        {
            ResultDto<AdmDetalleSolCompromisoDeleteDto> result = new ResultDto<AdmDetalleSolCompromisoDeleteDto>(null);
            try
            {

                var codigoDetalleSolicitud = await _repository.GetByCodigo(dto.CodigoDetalleSolicitud);
                if (codigoDetalleSolicitud == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Solicitud no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDetalleSolicitud);

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

