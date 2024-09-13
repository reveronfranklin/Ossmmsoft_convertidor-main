using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmDetalleSolicitudService : IAdmDetalleSolicitudService
    {
        private readonly IAdmDetalleSolicitudRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;
        private readonly IAdmPucSolicitudRepository _admPucSolicitudRepository;
        private readonly IAdmProductosRepository _admProductosRepository;
        private readonly IPRE_V_SALDOSRepository _preVSaldosRepository;
        public AdmDetalleSolicitudService(IAdmDetalleSolicitudRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IAdmSolicitudesRepository admSolicitudesRepository,
                                     IAdmPucSolicitudRepository admPucSolicitudRepository,
                                     IAdmProductosRepository admProductosRepository
                                    
                                
                                     )
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _admSolicitudesRepository = admSolicitudesRepository;
            _admPucSolicitudRepository = admPucSolicitudRepository;
            _admProductosRepository = admProductosRepository;
        
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
            itemResult.PrecioTotal = (decimal)dtos.TOTAL;
            itemResult.PorDescuento = dtos.POR_DESCUENTO;
            itemResult.MontoDescuento = dtos.MONTO_DESCUENTO;
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.PorImpuesto = dtos.POR_IMPUESTO;
            itemResult.MontoImpuesto = dtos.MONTO_IMPUESTO;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.Total = dtos.TOTAL;
            itemResult.TotalMasImpuesto = dtos.TOTAL_MAS_IMPUESTO;
            itemResult.CodigoProducto = dtos.CODIGO_PRODUCTO == null ? 0 : dtos.CODIGO_PRODUCTO ;

            itemResult.DescripcionProducto = "";
            var producto = await _admProductosRepository.GetByCodigo((int)dtos.CODIGO_PRODUCTO);
            if (producto != null)
            {
                itemResult.DescripcionProducto =producto.DESCRIPCION;
            }
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

        public async  Task<ResultDto<List<AdmDetalleSolicitudResponseDto>>> GetByCodigoSolicitud(int codigoSolicitud)
        {

            ResultDto<List<AdmDetalleSolicitudResponseDto>> result = new ResultDto<List<AdmDetalleSolicitudResponseDto>>(null);
            try
            {
                var detalleSolicitud = await  _repository.GetByCodigoSolicitud(codigoSolicitud);
          
                if (detalleSolicitud != null && detalleSolicitud.Count() > 0)
                {

                    var totalMasImpuesto = await GetTotalMonto(detalleSolicitud);
                    var totalImpuesto = await GetTotalImpuesto(detalleSolicitud);
                    var total = await GetTotal(detalleSolicitud);
                    var totalPuc = await GetTotalMontoPuc(codigoSolicitud);
                    
                    result.Total1 = totalMasImpuesto;
                    result.Total2 = totalPuc;
                    result.Total3 = total;
                    result.Total4 = totalImpuesto;
                    
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
       
        
        
        public async Task<decimal> TotalPuc(int codigoDetalleSolicitud)
        {

            decimal result = 0;
            try
            {
              
                var pucSolicitud = await _admPucSolicitudRepository.GetByDetalleSolicitud(codigoDetalleSolicitud);
                if (pucSolicitud != null && pucSolicitud.Count > 0)
                {
                    decimal total = 0;
                    foreach (var item in pucSolicitud)
                    {
                        total = total + item.MONTO;
                    }

                    result = total;


                }
                else
                {
                    result = 0;
                }
              


                return result;
              
            }
            catch (Exception ex)
            {
                return result;
            }

        }

        
        public async Task<decimal> GetTotalMonto(List<AdmDetalleSolicitudResponseDto> detalleSolicitud)
        {

            decimal result = 0;
            try
            {
             
                if (detalleSolicitud != null && detalleSolicitud.Count > 0)
                {
                  
                    
                    var total  = detalleSolicitud.Sum(p => p.TotalMasImpuesto);
                    result = (decimal)total; 
                   
                    
                }
                else
                {
                    result = 0;
                }
              


                return result;
              
            }
            catch (Exception ex)
            {
                return result;
            }

        }
        
        public async Task<decimal> GetTotalImpuesto(List<AdmDetalleSolicitudResponseDto> detalleSolicitud)
        {

            decimal result = 0;
            try
            {
             
                if (detalleSolicitud != null && detalleSolicitud.Count > 0)
                {
                  
                    
                    var total  = detalleSolicitud.Sum(p => p.MontoImpuesto);
                    result = (decimal)total; 
                   
                    
                }
                else
                {
                    result = 0;
                }
              


                return result;
              
            }
            catch (Exception ex)
            {
                return result;
            }

        }
        
        public async Task<decimal> GetTotal(List<AdmDetalleSolicitudResponseDto> detalleSolicitud)
        {

            decimal result = 0;
            try
            {
             
                if (detalleSolicitud != null && detalleSolicitud.Count > 0)
                {
                  
                    
                    var total  = detalleSolicitud.Sum(p => p.Total);
                    result = (decimal)total; 
                   
                    
                }
                else
                {
                    result = 0;
                }
              


                return result;
              
            }
            catch (Exception ex)
            {
                return result;
            }

        }
        
        public async Task<decimal> GetTotalMontoPuc(int codigoSolicitud)
        {

            decimal result = 0;
            try
            {

                var puc = await _admPucSolicitudRepository.GetBySolicitud(codigoSolicitud);
             
                if (puc != null && puc.Count > 0)
                {
                  
                    
                    var total  = puc.Sum(p => p.MONTO);
                    result = (decimal)total; 
                   
                    
                }
                else
                {
                    result = 0;
                }
              


                return result;
              
            }
            catch (Exception ex)
            {
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
                var status = Estatus.GetStatusObj(solicitud.STATUS);
                if (status.Modificable == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Solicitud no puede ser modificada, se encuentra en status: {status.Descripcion}";
                    return result;
                }

                if (dto.Cantidad <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
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
             
                var tipoImpuesto = await _admDescriptivaRepository.GetByIdAndTitulo(18,dto.TipoImpuestoId);
                if(tipoImpuesto == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Impuesto Id Invalido";
                    return result;
                }

              
                var descriptivaImpuesto = await _admDescriptivaRepository.GetByCodigo(dto.TipoImpuestoId);

                /*var producto = await _admProductosRepository.GetByCodigo(dto.CodigoProducto);
                if (producto==null)
                {
                      result.Data = null;
                       result.IsValid = false;
                       result.Message = "Seleccione un Producto Valido";
                       return result;
                }*/
                
                codigoDetallesolicitud.CODIGO_SOLICITUD = dto.CodigoSolicitud;
                codigoDetallesolicitud.CANTIDAD = dto.Cantidad;
                codigoDetallesolicitud.UDM_ID = dto.UdmId;
                codigoDetallesolicitud.DESCRIPCION = dto.Descripcion;
                codigoDetallesolicitud.PRECIO_UNITARIO = dto.PrecioUnitario;
                codigoDetallesolicitud.POR_DESCUENTO = 0;
                codigoDetallesolicitud.MONTO_DESCUENTO =0;
                codigoDetallesolicitud.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;

                codigoDetallesolicitud.POR_IMPUESTO = ConvertStringToDecimal(descriptivaImpuesto.EXTRA1);
                codigoDetallesolicitud.MONTO_IMPUESTO =  ((codigoDetallesolicitud.PRECIO_UNITARIO * codigoDetallesolicitud.CANTIDAD) * codigoDetallesolicitud.POR_IMPUESTO )/100 ;
              
                codigoDetallesolicitud.CODIGO_PRESUPUESTO =(int)solicitud.CODIGO_PRESUPUESTO;
                //codigoDetallesolicitud.CODIGO_PRODUCTO =dto.CodigoProducto;
                codigoDetallesolicitud.TOTAL = codigoDetallesolicitud.PRECIO_UNITARIO * codigoDetallesolicitud.CANTIDAD;
                codigoDetallesolicitud.TOTAL_MAS_IMPUESTO =
                    codigoDetallesolicitud.TOTAL + (decimal)codigoDetallesolicitud.MONTO_IMPUESTO;

                var totalPuc = await TotalPuc(dto.CodigoDetalleSolicitud);
                if (codigoDetallesolicitud.TOTAL_MAS_IMPUESTO < totalPuc)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Esta intentando Modificar Precio o Cantidad y supera lo cargado en PUC";
                    return result;
                }
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


        public decimal ConvertStringToDecimal(string numberString)
        {
            
        
            try
            {
                decimal numeroDecimal = Convert.ToDecimal(numberString);
                return numeroDecimal;
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (OverflowException)
            {
                return 0;
            }
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
                var status = Estatus.GetStatusObj(solicitud.STATUS);
                if (status.Modificable == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Solicitud no puede ser modificada, se encuentra en status: {status.Descripcion}";
                    return result;
                }


                /*var solicitudProducto =
                    await _repository.GetByCodigoSolicitudProducto(dto.CodigoSolicitud, dto.CodigoProducto);
                if (solicitudProducto != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe este producto en la solicitud";
                    return result;
                }*/
                
                if (dto.Cantidad <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
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
              
                var tipoImpuesto = await _admDescriptivaRepository.GetByIdAndTitulo(18, dto.TipoImpuestoId);
                if (tipoImpuesto == false)
                {
                 
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Tipo Impuesto Id Invalido";
                    return result;
                }

                var descriptivaImpuesto = await _admDescriptivaRepository.GetByCodigo(dto.TipoImpuestoId);
                /*if (dto.CodigoProducto > 0)
                {
                    var producto = await _admProductosRepository.GetByCodigo(dto.CodigoProducto);
               
                
                    if (producto==null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Seleccione un Producto Valido";
                        return result;
                    }
                }*/
                
             
              

            ADM_DETALLE_SOLICITUD entity = new ADM_DETALLE_SOLICITUD();
            entity.CODIGO_DETALLE_SOLICITUD = await _repository.GetNextKey();
            entity.CODIGO_SOLICITUD = dto.CodigoSolicitud;
            entity.CANTIDAD = dto.Cantidad;
            entity.CANTIDAD_COMPRADA = 0;
            entity.CANTIDAD_ANULADA = 0;
            entity.UDM_ID = dto.UdmId;
            entity.DESCRIPCION = dto.Descripcion;
            entity.PRECIO_UNITARIO = dto.PrecioUnitario;
            entity.POR_DESCUENTO=0;
            entity.MONTO_DESCUENTO = 0;
            entity.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;

          
            entity.CODIGO_PRODUCTO = dto.CodigoProducto;
            entity.CODIGO_PRESUPUESTO = (int)solicitud.CODIGO_PRESUPUESTO;
            entity.CODIGO_PRODUCTO = dto.CodigoProducto;
            
            
            entity.POR_IMPUESTO = ConvertStringToDecimal(descriptivaImpuesto.EXTRA1);
            entity.MONTO_IMPUESTO =  ((entity.PRECIO_UNITARIO * entity.CANTIDAD) * entity.POR_IMPUESTO )/100 ;
            entity.TOTAL = entity.PRECIO_UNITARIO * entity.CANTIDAD;
            entity.TOTAL_MAS_IMPUESTO =
                entity.TOTAL + (decimal)entity.MONTO_IMPUESTO;

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

                var pucSolicitud = await _admPucSolicitudRepository.ExisteByDetalleSolicitud(dto.CodigoDetalleSolicitud);
                if (pucSolicitud ==true)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Solicitud Contiene Informacion de PUC";
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

