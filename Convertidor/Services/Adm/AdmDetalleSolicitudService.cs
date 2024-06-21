using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmDetalleSolicitudService : IAdmDetalleSolicitudService
    {
        private readonly IAdmDetalleSolicitudRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;

        public AdmDetalleSolicitudService(IAdmDetalleSolicitudRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IAdmSolicitudesRepository admSolicitudesRepository
                                
                                     )
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _admSolicitudesRepository = admSolicitudesRepository;
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
            itemResult.DescripcionUnidad = "";
            var unidadDescriptiva = await _admDescriptivaRepository.GetByCodigo(dtos.UDM_ID);
            if (unidadDescriptiva != null)
            {
                itemResult.DescripcionUnidad =unidadDescriptiva.DESCRIPCION ;
            }
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.PrecioUnitario = dtos.PRECIO_UNITARIO;
            itemResult.PrecioTotal = itemResult.PrecioUnitario * itemResult.Cantidad;
            itemResult.PorDescuento = dtos.POR_DESCUENTO;
            itemResult.MontoDescuento = dtos.MONTO_DESCUENTO;
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.PorImpuesto = dtos.POR_IMPUESTO;
            itemResult.MontoImpuesto = dtos.MONTO_IMPUESTO;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.CodigoProducto = dtos.CODIGO_PRODUCTO == null ? 0 : dtos.CODIGO_PRODUCTO ;
            return itemResult;
        }

        public  async Task<List<AdmDetalleSolicitudResponseDto>> MapListDetalleSolicitudDto(List<ADM_DETALLE_SOLICITUD> dtos)
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

        public ResultDto<List<AdmDetalleSolicitudResponseDto>> GetByCodigoSolicitud(int codigoSolicitud)
        {

            ResultDto<List<AdmDetalleSolicitudResponseDto>> result = new ResultDto<List<AdmDetalleSolicitudResponseDto>>(null);
            try
            {
                var detalleSolicitud = _repository.GetByCodigoSolicitud(codigoSolicitud);
          
                if (detalleSolicitud != null && detalleSolicitud.Count() > 0)
                {
                   

                    result.Data = detalleSolicitud;
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
                    result.Message = TraduccionErrores.AdmDetalleSolicitudNoexiste; 
                    return result;
                }
                if (dto.CodigoSolicitud<0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo solicitud invalido";
                    return result;
                }
                var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (solicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo solicitud invalido";
                    return result;
                }

                if (dto.Cantidad <=0)
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
                    result.Message = "Unidad de medida  no existe";
                    return result;

                }

                if (string.IsNullOrEmpty(dto.Descripcion))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

                if (dto.PrecioUnitario <=0)
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
                var tipoImpuesto = await _admDescriptivaRepository.GetByIdAndTitulo(18,dto.TipoImpuestoId);
                if(tipoImpuesto == false) 
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

                if (dto.CodigoProducto > 0)
                {
                    //TODO Validar Producto 
                }
                
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
              
                codigoDetallesolicitud.CODIGO_PRESUPUESTO =(int)solicitud.CODIGO_PRESUPUESTO;
                codigoDetallesolicitud.CODIGO_PRODUCTO =dto.CodigoProducto;

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoDetallesolicitud.CODIGO_EMPRESA = conectado.Empresa;
                codigoDetallesolicitud.USUARIO_UPD = conectado.Usuario;
                codigoDetallesolicitud.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoDetallesolicitud);

                var resultDto =  await MapDetalleSolicitudDto(codigoDetallesolicitud);
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
               
                if (dto.CodigoSolicitud <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo solicitud invalido";
                    return result;
                }

                var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (solicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo solicitud invalido";
                    return result;
                }

                if (dto.Cantidad <= 0)
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

                if (string.IsNullOrEmpty(dto.Descripcion))
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
                var tipoImpuesto = await _admDescriptivaRepository.GetByIdAndTitulo(18, dto.TipoImpuestoId);
                if (tipoImpuesto == false)
                {
                 
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Tipo Impuesto Id Invalido";
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

                if (dto.CodigoProducto > 0)
                {
                    //TODO Validar Producto 
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
            entity.CODIGO_PRODUCTO = dto.CodigoProducto;
            entity.CODIGO_PRESUPUESTO = (int)solicitud.CODIGO_PRESUPUESTO;
            entity.CODIGO_PRODUCTO = dto.CodigoProducto;

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

