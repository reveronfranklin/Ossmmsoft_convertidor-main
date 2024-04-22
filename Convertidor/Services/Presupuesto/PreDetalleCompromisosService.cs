using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public class PreDetalleCompromisosService: IPreDetalleCompromisosService
    {
        
        private readonly IPreDetalleCompromisosRepository _repository;
        private readonly IPreCompromisosRepository _preCompromisosRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IAdmDescriptivaRepository _admDescriptiva;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public PreDetalleCompromisosService(IPreDetalleCompromisosRepository repository,
                                      IPreCompromisosRepository preCompromisosRepository,
                                      IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                      IPreDescriptivaRepository repositoryPreDescriptiva,
                                      IAdmDescriptivaRepository admDescriptiva,
                                      IAdmSolicitudesRepository admSolicitudesRepository,
                                      IConfiguration configuration,
                                      ISisUsuarioRepository sisUsuarioRepository
                                     )
		{
            _repository = repository;
            _preCompromisosRepository = preCompromisosRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _configuration = configuration;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _admDescriptiva = admDescriptiva;
            _admSolicitudesRepository = admSolicitudesRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }


        public async Task<ResultDto<List<PreDetalleCompromisosResponseDto>>> GetAll()
        {

            ResultDto<List<PreDetalleCompromisosResponseDto>> result = new ResultDto<List<PreDetalleCompromisosResponseDto>>(null);
            try
            {

                var Detallecompromisos = await _repository.GetAll();

               

                if (Detallecompromisos.Count() > 0)
                {
                    List<PreDetalleCompromisosResponseDto> listDto = new List<PreDetalleCompromisosResponseDto>();

                    foreach (var item in Detallecompromisos)
                    {
                        var dto = await MapPreDetalleCompromisos(item);
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

        public async Task<PreDetalleCompromisosResponseDto> MapPreDetalleCompromisos(PRE_DETALLE_COMPROMISOS dto)
        {
            PreDetalleCompromisosResponseDto itemResult = new PreDetalleCompromisosResponseDto();
            itemResult.CodigoDetalleCompromiso = dto.CODIGO_DETALLE_COMPROMISO;
            itemResult.CodigoCompromiso = dto.CODIGO_COMPROMISO;
            itemResult.CodigoDetalleSolicitud = dto.CODIGO_DETALLE_SOLICITUD;
            itemResult.Cantidad = dto.CANTIDAD;
            itemResult.CantidadAnulada = dto.CANTIDAD_ANULADA;
            itemResult.UdmId = dto.UDM_ID;
            itemResult.Descripcion = dto.DESCRIPCION;
            itemResult.PrecioUnitario = dto.PRECIO_UNITARIO;
            itemResult.PorDescuento = dto.POR_DESCUENTO;
            itemResult.MontoDescuento = dto.MONTO_DESCUENTO;
            itemResult.TipoImpuestoId = dto.TIPO_IMPUESTO_ID;
            itemResult.PorImpuesto = dto.POR_IMPUESTO;
            itemResult.MontoImpuesto = dto.MONTO_IMPUESTO;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
           

            return itemResult;

        }


        public async Task<List<PreDetalleCompromisosResponseDto>> MapListPreCompromisosDto(List<PRE_DETALLE_COMPROMISOS> dtos)
        {
            List<PreDetalleCompromisosResponseDto> result = new List<PreDetalleCompromisosResponseDto>();


            foreach (var item in dtos)
            {

                PreDetalleCompromisosResponseDto itemResult = new PreDetalleCompromisosResponseDto();

                itemResult = await MapPreDetalleCompromisos(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PreDetalleCompromisosResponseDto>> Update(PreDetalleCompromisosUpdateDto dto)
        {

            ResultDto<PreDetalleCompromisosResponseDto> result = new ResultDto<PreDetalleCompromisosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoDetalleCompromiso = await _repository.GetByCodigo(dto.CodigoDetalleCompromiso);
                if (codigoDetalleCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Compromiso no existe";
                    return result;
                }

                var codigoCompromiso = await _preCompromisosRepository.GetByCodigo(dto.CodigoCompromiso);
                if (codigoCompromiso.CODIGO_COMPROMISO < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Compromiso Invalido";
                    return result;
                }

                if (dto.CodigoDetalleSolicitud < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Solicitud Invalido";
                    return result;
                }

                
                if (dto.Cantidad < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Invalida";
                    return result;
                }
                if (dto.CantidadAnulada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Anulada Invalida";
                    return result;
                }
                var udmId = await _repositoryPreDescriptiva.GetByIdAndTitulo(5, dto.UdmId);
                if (udmId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "UdmID Invalido";
                    return result;
                }

                if (dto.Descripcion.Length > 2000)
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
                    result.Message = "Precio Unitario Invalido";
                    return result;
                }

                if (dto.PorDescuento > 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por descuento Invalido";
                    return result;
                }

                var tipoImpuestoId = await _admDescriptiva.GetByIdAndTitulo(18,dto.TipoImpuestoId);
                if (tipoImpuestoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Impuesto ID Invalido";
                    return result;
                }

                if (dto.PorImpuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por Impuesto Invalido";
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
                if (codigoPresupuesto.CODIGO_PRESUPUESTO < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }


                codigoDetalleCompromiso.CODIGO_DETALLE_COMPROMISO = dto.CodigoDetalleCompromiso;
                codigoDetalleCompromiso.CODIGO_COMPROMISO = dto.CodigoCompromiso;
                codigoDetalleCompromiso.CODIGO_DETALLE_SOLICITUD = dto.CodigoDetalleSolicitud;
                codigoDetalleCompromiso.CANTIDAD = dto.Cantidad;
                codigoDetalleCompromiso.CANTIDAD_ANULADA = dto.CantidadAnulada;
                codigoDetalleCompromiso.UDM_ID = dto.UdmId;
                codigoDetalleCompromiso.DESCRIPCION =dto.Descripcion;
                codigoDetalleCompromiso.PRECIO_UNITARIO =dto.PrecioUnitario;
                codigoDetalleCompromiso.POR_DESCUENTO = dto.PorDescuento;
                codigoDetalleCompromiso.MONTO_DESCUENTO = dto.MontoDescuento;
                codigoDetalleCompromiso.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                codigoDetalleCompromiso.POR_IMPUESTO = dto.PorImpuesto;
                codigoDetalleCompromiso.MONTO_IMPUESTO = dto.MontoImpuesto;
                codigoDetalleCompromiso.EXTRA1 = dto.Extra1;
                codigoDetalleCompromiso.EXTRA2 = dto.Extra2;
                codigoDetalleCompromiso.EXTRA3 = dto.Extra3;
                codigoDetalleCompromiso.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
       

                codigoDetalleCompromiso.CODIGO_EMPRESA = conectado.Empresa;
                codigoDetalleCompromiso.USUARIO_UPD = conectado.Usuario;
                codigoDetalleCompromiso.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoDetalleCompromiso);

                var resultDto =await  MapPreDetalleCompromisos(codigoDetalleCompromiso);
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

        public async Task<ResultDto<PreDetalleCompromisosResponseDto>> Create(PreDetalleCompromisosUpdateDto dto)
        {

            ResultDto<PreDetalleCompromisosResponseDto> result = new ResultDto<PreDetalleCompromisosResponseDto>(null);
            try
            {

                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoDetalleCompromiso = await _repository.GetByCodigo(dto.CodigoDetalleCompromiso);
                if (codigoDetalleCompromiso != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Compromiso ya existe";
                    return result;
                }

                var codigoCompromiso = await _preCompromisosRepository.GetByCodigo(dto.CodigoCompromiso);
                if (codigoCompromiso.CODIGO_COMPROMISO < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Compromiso Invalido";
                    return result;
                }

                if (dto.CodigoDetalleSolicitud < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Solicitud Invalido";
                    return result;
                }


                if (dto.Cantidad < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Invalida";
                    return result;
                }
                if (dto.CantidadAnulada < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Anulada Invalida";
                    return result;
                }
                var udmId = await _repositoryPreDescriptiva.GetByIdAndTitulo(5, dto.UdmId);
                if (udmId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "UdmID Invalido";
                    return result;
                }

                if (dto.Descripcion.Length > 2000)
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
                    result.Message = "Precio Unitario Invalido";
                    return result;
                }

                if (dto.PorDescuento > 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por descuento Invalido";
                    return result;
                }

                var tipoImpuestoId = await _admDescriptiva.GetByIdAndTitulo(18, dto.TipoImpuestoId);
                if (tipoImpuestoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Impuesto ID Invalido";
                    return result;
                }

                if (dto.PorImpuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por Impuesto Invalido";
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
                if (codigoPresupuesto.CODIGO_PRESUPUESTO < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                PRE_DETALLE_COMPROMISOS entity = new PRE_DETALLE_COMPROMISOS();
                entity.CODIGO_DETALLE_COMPROMISO = await _repository.GetNextKey();
                entity.CODIGO_COMPROMISO = dto.CodigoCompromiso;
                entity.CODIGO_DETALLE_SOLICITUD = dto.CodigoDetalleSolicitud;
                entity.CANTIDAD = dto.Cantidad;
                entity.CANTIDAD_ANULADA = dto.CantidadAnulada;
                entity.UDM_ID = dto.UdmId;
                entity.DESCRIPCION = dto.Descripcion;
                entity.PRECIO_UNITARIO = dto.PrecioUnitario;
                entity.POR_DESCUENTO = dto.PorDescuento;
                entity.MONTO_DESCUENTO = dto.MontoDescuento;
                entity.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                entity.POR_IMPUESTO = dto.PorImpuesto;
                entity.MONTO_IMPUESTO = dto.MontoImpuesto;
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
                    var resultDto = await MapPreDetalleCompromisos(created.Data);
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
  


        public async Task<ResultDto<PreDetalleCompromisosDeleteDto>> Delete(PreDetalleCompromisosDeleteDto dto)
        {

            ResultDto<PreDetalleCompromisosDeleteDto> result = new ResultDto<PreDetalleCompromisosDeleteDto>(null);
            try
            {

                var CodigoDetalleCompromiso = await _repository.GetByCodigo(dto.CodigoDetalleCompromiso);
                if (CodigoDetalleCompromiso == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Compromiso no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDetalleCompromiso);

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

