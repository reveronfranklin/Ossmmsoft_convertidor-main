using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmDetalleSolicitudService : IAdmDetalleSolicitudService
    {
        private readonly IAdmDetalleSolicitudRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmDetalleSolicitudService(IAdmDetalleSolicitudRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

        public async Task<AdmDetalleSolicitudResponseDto> MapDetalleSolicitudDto(ADM_DETALLE_SOLICITUD dtos)
        {
            AdmDetalleSolicitudResponseDto itemResult = new AdmDetalleSolicitudResponseDto();
            itemResult.CodigoDetalleSolicitud = dtos.CODIGO_DETALLE_SOLICITUD;
            itemResult.CodigoSolicitud = dtos.CODIGO_SOLICITUD;
            itemResult.Cantidad = dtos.CANTIDAD;
            itemResult.CantidadComprada = dtos.CANTIDAD_COMPRADA;
            itemResult.CantidadAnulada = dtos.CANTIDAD_ANULADA;
            itemResult.UdmId = dtos.UDM_ID;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.PrecioUnitario = dtos.PRECIO_UNITARIO;
            itemResult.PorDescuento = dtos.POR_DESCUENTO;
            itemResult.MontoDescuento = dtos.MONTO_DESCUENTO;
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.PorImpuesto = dtos.POR_IMPUESTO;
            itemResult.MontoImpuesto = dtos.MONTO_IMPUESTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            return itemResult;
        }

        public async Task<List<AdmDetalleSolicitudResponseDto>> MapListDetalleSolicitudDto(List<ADM_DETALLE_SOLICITUD> dtos)
        {
            List<AdmDetalleSolicitudResponseDto> result = new List<AdmDetalleSolicitudResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapDetalleSolicitudDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmDetalleSolicitudResponseDto>>> GetAll()
        {

            ResultDto<List<AdmDetalleSolicitudResponseDto>> result = new ResultDto<List<AdmDetalleSolicitudResponseDto>>(null);
            try
            {
                var detalleSolicitud = await _repository.GetAll();
                var cant = detalleSolicitud.Count();
                if (detalleSolicitud != null && detalleSolicitud.Count() > 0)
                {
                    var listDto = await MapListDetalleSolicitudDto(detalleSolicitud);

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

        public async Task<ResultDto<AdmDetalleSolicitudResponseDto>> Update(AdmDetalleSolicitudUpdateDto dto)
        {
            ResultDto<AdmDetalleSolicitudResponseDto> result = new ResultDto<AdmDetalleSolicitudResponseDto>(null);
            try
            {
                var codigoDetallesolicitud = await _repository.GetCodigoDetalleSolicitud(dto.CodigoDetalleSolicitud);
                if (codigoDetallesolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Solicitud no existe";
                    return result;
                }
                if (dto.CodigoSolicitud<0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo solicitud invalido";
                    return result;
                }

                if (dto.Cantidad <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
                    return result;

                }

                if (dto.CantidadComprada <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad comprada Invalida";
                    return result;
                }
                if (dto.CantidadAnulada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad anulada Invalida";
                    return result;
                }

                var udmId = await _admDescriptivaRepository.GetByIdAndTitulo(21,dto.UdmId);
                if (udmId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Udm Id no existe";
                    return result;

                }

                if (dto.Descripcion==string.Empty && dto.Descripcion.Length>2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

                if (dto.PrecioUnitario <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Precio unitario Invalido";
                    return result;
                }
                if (dto.PorDescuento<0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por descuento invalido";
                    return result;
                }

                if (dto.MontoDescuento <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Descuento Invalido";
                    return result;
                }
                var tipoImpuestoID = await _admDescriptivaRepository.GetByIdAndTitulo(18,dto.TipoImpuestoId);
                if(tipoImpuestoID == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Impuesto Id Invalido";
                    return result;
                }

                if (dto.PorImpuesto <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por Impuesto Invalido";
                    return result;
                }

                if (dto.MontoImpuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Impuesto Invalido";
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

                codigoDetallesolicitud.CODIGO_DETALLE_SOLICITUD = dto.CodigoDetalleSolicitud;
                codigoDetallesolicitud.CODIGO_SOLICITUD = dto.CodigoSolicitud;
                codigoDetallesolicitud.CANTIDAD = dto.Cantidad;
                codigoDetallesolicitud.CANTIDAD_COMPRADA = dto.CantidadComprada;
                codigoDetallesolicitud.CANTIDAD_ANULADA = dto.CantidadAnulada;
                codigoDetallesolicitud.UDM_ID = dto.UdmId;
                codigoDetallesolicitud.DESCRIPCION = dto.Descripcion;
                codigoDetallesolicitud.PRECIO_UNITARIO = dto.PrecioUnitario;
                codigoDetallesolicitud.POR_DESCUENTO = dto.PorDescuento;
                codigoDetallesolicitud.MONTO_DESCUENTO = dto.MontoDescuento;
                codigoDetallesolicitud.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                codigoDetallesolicitud.POR_IMPUESTO = dto.PorImpuesto;
                codigoDetallesolicitud.MONTO_IMPUESTO = dto.MontoImpuesto;
                codigoDetallesolicitud.EXTRA1 = dto.Extra1;
                codigoDetallesolicitud.EXTRA2 = dto.Extra2;
                codigoDetallesolicitud.EXTRA3 = dto.Extra3;
                codigoDetallesolicitud.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoDetallesolicitud.CODIGO_EMPRESA = conectado.Empresa;
                codigoDetallesolicitud.USUARIO_UPD = conectado.Usuario;
                codigoDetallesolicitud.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoDetallesolicitud);

                var resultDto = await MapDetalleSolicitudDto(codigoDetallesolicitud);
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

        public async Task<ResultDto<AdmDetalleSolicitudResponseDto>> Create(AdmDetalleSolicitudUpdateDto dto)
        {
            ResultDto<AdmDetalleSolicitudResponseDto> result = new ResultDto<AdmDetalleSolicitudResponseDto>(null);
            try
            {
                var codigoDetallesolicitud = await _repository.GetCodigoDetalleSolicitud(dto.CodigoDetalleSolicitud);
                if (codigoDetallesolicitud != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Solicitud ya existe";
                    return result;
                }
                if (dto.CodigoSolicitud < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo solicitud invalido";
                    return result;
                }

                if (dto.Cantidad < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
                    return result;

                }

                if (dto.CantidadComprada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad comprada Invalida";
                    return result;
                }
                if (dto.CantidadAnulada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad anulada Invalida";
                    return result;
                }

                var udmId = await _admDescriptivaRepository.GetByIdAndTitulo(21, dto.UdmId);
                if (udmId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Udm Id no existe";
                    return result;

                }

                if (dto.Descripcion == string.Empty && dto.Descripcion.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

                if (dto.PrecioUnitario < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Precio unitario Invalido";
                    return result;
                }
                if (dto.PorDescuento < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por descuento invalido";
                    return result;
                }

                if (dto.MontoDescuento < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Descuento Invalido";
                    return result;
                }
                var tipoImpuestoID = await _admDescriptivaRepository.GetByIdAndTitulo(18, dto.TipoImpuestoId);
                if (tipoImpuestoID == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Impuesto Id Invalido";
                    return result;
                }

                if (dto.PorImpuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por Impuesto Invalido";
                    return result;
                }

                if (dto.MontoImpuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Impuesto Invalido";
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


            ADM_DETALLE_SOLICITUD entity = new ADM_DETALLE_SOLICITUD();
            entity.CODIGO_DETALLE_SOLICITUD = await _repository.GetNextKey();
            entity.CODIGO_SOLICITUD = dto.CodigoSolicitud;
            entity.CANTIDAD = dto.Cantidad;
            entity.CANTIDAD_COMPRADA = dto.CantidadComprada;
            entity.CANTIDAD_ANULADA = dto.CantidadAnulada;
            entity.UDM_ID = dto.UdmId;
            entity.DESCRIPCION = dto.Descripcion;
            entity.PRECIO_UNITARIO = dto.PrecioUnitario;
            entity.POR_DESCUENTO=dto.PorDescuento;
            entity.MONTO_DESCUENTO = dto.MontoDescuento;
            entity.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
            entity.POR_IMPUESTO = dto.PorImpuesto;
            entity.MONTO_IMPUESTO = dto.MontoImpuesto;
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
                var resultDto = await MapDetalleSolicitudDto(created.Data);
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

        public async Task<ResultDto<AdmDetalleSolicitudDeleteDto>> Delete(AdmDetalleSolicitudDeleteDto dto) 
        {
            ResultDto<AdmDetalleSolicitudDeleteDto> result = new ResultDto<AdmDetalleSolicitudDeleteDto>(null);
            try
            {

                var codigoDetalleSolicitud = await _repository.GetCodigoDetalleSolicitud(dto.CodigoDetalleSolicitud);
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

