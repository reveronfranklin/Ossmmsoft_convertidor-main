﻿using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Presupuesto
{
	public class PreCompromisosService: IPreCompromisosService
    {

      
        private readonly IPreCompromisosRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;
        private readonly IAdmDetalleSolicitudRepository _admDetalleSolicitudRepository;
        private readonly IAdmPucSolicitudRepository _admPucSolicitudRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly ISisSerieDocumentosRepository _serieDocumentosRepository;
        private readonly ISisDescriptivaRepository _sisDescriptivaRepository;
        private readonly IPRE_V_SALDOSRepository _preVSaldosRepository;
        private readonly IAdmSolicitudesService _admSolicitudesService;
        private readonly IPreDetalleCompromisosRepository _preDetalleCompromisosRepository;
        private readonly IPrePucCompromisosRepository _prePucCompromisosRepository;

        private readonly IConfiguration _configuration;
        public PreCompromisosService(IPreCompromisosRepository repository,
                                      IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                      IPreDescriptivaRepository repositoryPreDescriptiva,
                                      IAdmProveedoresRepository admProveedoresRepository,
                                      IAdmSolicitudesRepository admSolicitudesRepository,
                                      IAdmDetalleSolicitudRepository admDetalleSolicitudRepository,
                                      IAdmPucSolicitudRepository admPucSolicitudRepository,
                                      IConfiguration configuration,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IAdmDescriptivaRepository admDescriptivaRepository,
                                      ISisSerieDocumentosRepository serieDocumentosRepository,
                                      ISisDescriptivaRepository sisDescriptivaRepository,
                                      IPRE_V_SALDOSRepository preVSaldosRepository,
                                      IAdmSolicitudesService admSolicitudesService,
                                      IPreDetalleCompromisosRepository preDetalleCompromisosRepository,
                                      IPrePucCompromisosRepository prePucCompromisosRepository
        )
		{
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _configuration = configuration;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _admProveedoresRepository = admProveedoresRepository;
            _admSolicitudesRepository = admSolicitudesRepository;
            _admDetalleSolicitudRepository = admDetalleSolicitudRepository;
            _admPucSolicitudRepository = admPucSolicitudRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _serieDocumentosRepository = serieDocumentosRepository;
            _sisDescriptivaRepository = sisDescriptivaRepository;
            _preVSaldosRepository = preVSaldosRepository;
            _admSolicitudesService = admSolicitudesService;
            _preDetalleCompromisosRepository = preDetalleCompromisosRepository;
            _prePucCompromisosRepository = prePucCompromisosRepository;
        }


        public async Task<ResultDto<List<PreCompromisosResponseDto>>> GetAll()
        {

            ResultDto<List<PreCompromisosResponseDto>> result = new ResultDto<List<PreCompromisosResponseDto>>(null);
            try
            {

                var compromisos = await _repository.GetAll();

               

                if (compromisos.Count() > 0)
                {
                    List<PreCompromisosResponseDto> listDto = new List<PreCompromisosResponseDto>();

                    foreach (var item in compromisos)
                    {
                        var dto = await MapPreCompromisos(item);
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


        public async Task<PreCompromisosResponseDto> MapPreCompromisos(PRE_COMPROMISOS dto)
        {
            PreCompromisosResponseDto itemResult = new PreCompromisosResponseDto();
            itemResult.CodigoCompromiso = dto.CODIGO_COMPROMISO;
            itemResult.Ano = dto.ANO;
            itemResult.CodigoSolicitud = dto.CODIGO_SOLICITUD;
            itemResult.NumeroCompromiso = dto.NUMERO_COMPROMISO;
            itemResult.FechaCompromiso = dto.FECHA_COMPROMISO;
            itemResult.FechaCompromisoString = Fecha.GetFechaString( dto.FECHA_COMPROMISO);
            FechaDto fechaCompromisoObj = Fecha.GetFechaDto(dto.FECHA_COMPROMISO);
            itemResult.FechaCompromisoObj = (FechaDto)fechaCompromisoObj;
            itemResult.CodigoProveedor = dto.CODIGO_PROVEEDOR;
            var proveedor = await _admProveedoresRepository.GetByCodigo(dto.CODIGO_PROVEEDOR);
            if (proveedor != null)
            {
                itemResult.NombreProveedor = proveedor.NOMBRE_PROVEEDOR;
            }
        
            itemResult.CodigoDirEntrega = dto.CODIGO_DIR_ENTREGA;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
            return itemResult;

        }


        public async Task<ResultDto<bool>> CrearCompromisoDesdeSolicitud(int codigoSolicitud)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            var conectado = await _sisUsuarioRepository.GetConectado();
            try
            {

                var admSolicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(codigoSolicitud);
                if (admSolicitud == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Codigo de Solicitud no existe";
                    return result;
                }
                var preCompromisoPorSolicitud = await _repository.GetByCodigoSolicitud(codigoSolicitud);
                if (preCompromisoPorSolicitud != null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = $"Codigo de Solicitud ya tiene el compromiso: {admSolicitud.NUMERO_SOLICITUD}";
                    return result;
                }

                var solicitudPuedeSerAprobada = await _admSolicitudesService.SolicitudPuedeSerAprobada(codigoSolicitud);
                if (solicitudPuedeSerAprobada.IsValid==false)
                {
                    result.Data = solicitudPuedeSerAprobada.Data;
                    result.IsValid = solicitudPuedeSerAprobada.IsValid;
                    result.Message = solicitudPuedeSerAprobada.Message;
                    return result;
                }
                
                
                
                PRE_COMPROMISOS entity = new PRE_COMPROMISOS();
                entity.CODIGO_COMPROMISO = await _repository.GetNextKey();
                entity.ANO = (int)admSolicitud.ANO;
                entity.CODIGO_SOLICITUD = admSolicitud.CODIGO_SOLICITUD;
                
                var tipoSolicitudTitulo = await _admDescriptivaRepository.GetByTitulo(35);
                var descriptivaSolicitud =
                    tipoSolicitudTitulo.Where(x => x.DESCRIPCION_ID == admSolicitud.TIPO_SOLICITUD_ID).FirstOrDefault();
                //SE GENERA EL PROXIMO NUMERO DE COMPROMISO
                var sisDescriptiva = await _sisDescriptivaRepository.GetByCodigoDescripcion(descriptivaSolicitud.CODIGO);
                var numeroCompromiso = await _serieDocumentosRepository.GenerateNextSerie((int)admSolicitud.CODIGO_PRESUPUESTO,sisDescriptiva.DESCRIPCION_ID,sisDescriptiva.CODIGO_DESCRIPCION);
                entity.NUMERO_COMPROMISO = numeroCompromiso;
                entity.FECHA_COMPROMISO = DateTime.Now;
                entity.CODIGO_PROVEEDOR = (int)admSolicitud.CODIGO_PROVEEDOR;
                entity.FECHA_ENTREGA = null;
                //TODO
                //entity.CODIGO_DIR_ENTREGA = admSolicitud.co;
                entity.TIPO_PAGO_ID = null;
                entity.MOTIVO = admSolicitud.MOTIVO;
                entity.STATUS = "PE";
                entity.EXTRA1 = "";
                entity.EXTRA2 = "";
                entity.EXTRA3 = "";
                entity.CODIGO_PRESUPUESTO = (int)admSolicitud.CODIGO_PRESUPUESTO;
                entity.TIPO_RENGLON_ID = null;
                entity.NUMERO_ORDEN =null;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var admDetalleSolicitud =
                        await _admDetalleSolicitudRepository.GetByCodigoSolicitud(codigoSolicitud);

                    if (admDetalleSolicitud != null && admDetalleSolicitud.Count > 0)
                    {
                        foreach (var item in admDetalleSolicitud)
                        {
                            PRE_DETALLE_COMPROMISOS detailEntity = new PRE_DETALLE_COMPROMISOS();
                            detailEntity.CODIGO_COMPROMISO = created.Data.CODIGO_COMPROMISO;
                            detailEntity.CODIGO_DETALLE_COMPROMISO = await _preDetalleCompromisosRepository.GetNextKey();
                            detailEntity.CODIGO_DETALLE_SOLICITUD = item.CodigoDetalleSolicitud;
                            
                            detailEntity.CANTIDAD = item.Cantidad;
                            detailEntity.CANTIDAD_ANULADA = 0;
                            detailEntity.UDM_ID = item.UdmId;
                            detailEntity.DESCRIPCION = item.Descripcion;
                            detailEntity.PRECIO_UNITARIO = item.PrecioUnitario;
                            detailEntity.POR_DESCUENTO=(decimal)item.PorDescuento;
                            detailEntity.MONTO_DESCUENTO = (decimal)item.MontoDescuento;
                            detailEntity.TIPO_IMPUESTO_ID = item.TipoImpuestoId;
                            detailEntity.POR_IMPUESTO = item.PorImpuesto;
                            detailEntity.MONTO_IMPUESTO = (decimal)item.MontoImpuesto ;
                            //TODO VERIFICAR SI SE AGREGA CODIGO PRODUCTO
                            //detailEntity.CODIGO_PRODUCTO = item.CodigoProducto;
                            detailEntity.CODIGO_PRESUPUESTO = (int)item.CodigoPresupuesto;
                            detailEntity.CODIGO_EMPRESA = conectado.Empresa;
                            detailEntity.USUARIO_INS = conectado.Usuario;
                            detailEntity.FECHA_INS = DateTime.Now;

                            var createdDetail = await _preDetalleCompromisosRepository.Add(detailEntity);
                            if (createdDetail.IsValid && createdDetail.Data != null)
                            {
                                var admPucSolicitud = await _admPucSolicitudRepository.GetByDetalleSolicitud(
                                    item.CodigoDetalleSolicitud);
                                if (admPucSolicitud != null && admPucSolicitud.Count > 0)
                                {
                                    foreach (var itemPuc in admPucSolicitud)
                                    {
                                        
                                        PRE_PUC_COMPROMISOS entityPuc = new PRE_PUC_COMPROMISOS();
                                        entityPuc.CODIGO_PUC_COMPROMISO = await _prePucCompromisosRepository.GetNextKey();
                                        entityPuc.CODIGO_DETALLE_COMPROMISO = createdDetail.Data.CODIGO_DETALLE_COMPROMISO;
                                        entityPuc.CODIGO_PUC_SOLICITUD = itemPuc.CODIGO_PUC_SOLICITUD;
                                        entityPuc.CODIGO_SALDO = itemPuc.CODIGO_SALDO;
                                        entityPuc.CODIGO_ICP = itemPuc.CODIGO_ICP;
                                        entityPuc.CODIGO_PUC = itemPuc.CODIGO_PUC;
                                        entityPuc.FINANCIADO_ID = itemPuc.FINANCIADO_ID;
                                        entityPuc.CODIGO_FINANCIADO = (int)itemPuc.CODIGO_FINANCIADO;
                                        entityPuc.MONTO = itemPuc.MONTO;
                                        entityPuc.MONTO_CAUSADO = itemPuc.MONTO;
                                        entityPuc.MONTO_ANULADO = itemPuc.MONTO_ANULADO;
                                        entityPuc.EXTRA1 = "";
                                        entityPuc.EXTRA2 = "";
                                        entityPuc.EXTRA3 = "";
                                        entityPuc.CODIGO_PRESUPUESTO = itemPuc.CODIGO_PRESUPUESTO;

                                        entityPuc.CODIGO_EMPRESA = conectado.Empresa;
                                        entityPuc.USUARIO_INS = conectado.Usuario;
                                        entityPuc.FECHA_INS = DateTime.Now;

                                        var createdPuc = await _prePucCompromisosRepository.Add(entityPuc);
                                        
                                        
                                    }
                                }
                            }
                        }
                    }

                }

                admSolicitud.STATUS = "AP";
                admSolicitud.USUARIO_UPD = conectado.Usuario;
                admSolicitud.FECHA_UPD=DateTime.Now;
                await _admSolicitudesRepository.Update(admSolicitud);
                
                //ACTUALIZAR PRE_V_SALDO
                await _preVSaldosRepository.RecalcularSaldo((int)admSolicitud.CODIGO_PRESUPUESTO);
                
                result.Data = true;
                result.IsValid = true;
                result.Message = "";
                
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = ex.Message;
            }
    
            return result;
        }

         public async Task<ResultDto<bool>> AnularDesdeSolicitud(int codigoSolicitud)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            var conectado = await _sisUsuarioRepository.GetConectado();
            try
            {

                var admSolicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(codigoSolicitud);
                if (admSolicitud == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Codigo de Solicitud no existe";
                    return result;
                }
              ;
                if (admSolicitud.STATUS != "AP")
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = $"La solicitud {admSolicitud.NUMERO_SOLICITUD} no esta Aprobada";
                    return result;
                }

                var aprobar = await _repository.AnularDesdeSolicitud(codigoSolicitud);
                
                //ACTUALIZAR PRE_V_SALDO
                await _preVSaldosRepository.RecalcularSaldo((int)admSolicitud.CODIGO_PRESUPUESTO);
                
                result.Data = true;
                result.IsValid = true;
                result.Message = "";
                
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = ex.Message;
            }
    
            return result;
        }

        
        public async Task<ResultDto<List<PreCompromisosResponseDto>>> GetByPresupuesto(PreCompromisosFilterDto filter)
        {
            return await _repository.GetByPresupuesto(filter);
        }

        public async Task<List<PreCompromisosResponseDto>> MapListPreCompromisosDto(List<PRE_COMPROMISOS> dtos)
        {
            List<PreCompromisosResponseDto> result = new List<PreCompromisosResponseDto>();


            foreach (var item in dtos)
            {

                PreCompromisosResponseDto itemResult = new PreCompromisosResponseDto();

                itemResult = await MapPreCompromisos(item);

                result.Add(itemResult);
            }
            return result;



        }

        
        public async Task<ResultDto<PreCompromisosResponseDto>> Update(PreCompromisosUpdateDto dto)
        {

            ResultDto<PreCompromisosResponseDto> result = new ResultDto<PreCompromisosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if(dto.CodigoCompromiso < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Compromiso no existe";
                    return result;

                }
                var codigoCompromiso = await _repository.GetByCodigo(dto.CodigoCompromiso);
                if (codigoCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Compromiso no existe";
                    return result;
                }
                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año Invalido";
                    return result;
                }

                if(dto.CodigoSolicitud < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud Invalido";
                    return result;
                }
                var codigoSolicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (codigoSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud Invalido";
                    return result;
                }

                if (dto.NumeroCompromiso == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso no puede estar vacio";
                    return result;

                }

                if (dto.NumeroCompromiso.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso Invalido";
                    return result;

                }

                if (dto.FechaCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha compromiso Invalida";
                    return result;
                }
                
                
                if(dto.CodigoProveedor < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;

                }

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (codigoProveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.CodigoDirEntrega < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Dir entrega Invalido";
                    return result;
                }

                if (dto.Motivo.Length > 1150)
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

               

                codigoCompromiso.CODIGO_COMPROMISO = dto.CodigoCompromiso;
                codigoCompromiso.ANO = dto.Ano;
                codigoCompromiso.CODIGO_SOLICITUD = dto.CodigoSolicitud;
                codigoCompromiso.FECHA_COMPROMISO = dto.FechaCompromiso;
                codigoCompromiso.CODIGO_PROVEEDOR =dto.CodigoProveedor;
                codigoCompromiso.FECHA_ENTREGA =dto.FechaEntrega;
                codigoCompromiso.CODIGO_DIR_ENTREGA = dto.CodigoDirEntrega;
                codigoCompromiso.TIPO_PAGO_ID = dto.TipoPagoId;
                codigoCompromiso.MOTIVO = dto.Motivo;
                codigoCompromiso.STATUS = dto.Status;
                codigoCompromiso.EXTRA1 = dto.Extra1;
                codigoCompromiso.EXTRA2 = dto.Extra2;
                codigoCompromiso.EXTRA3 = dto.Extra3;
                codigoCompromiso.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoCompromiso.TIPO_RENGLON_ID = dto.TipoRenglonId;
                codigoCompromiso.NUMERO_ORDEN= dto.NumeroOrden;


                codigoCompromiso.CODIGO_EMPRESA = conectado.Empresa;
                codigoCompromiso.USUARIO_UPD = conectado.Usuario;
                codigoCompromiso.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoCompromiso);

                var resultDto =await  MapPreCompromisos(codigoCompromiso);
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

        public async Task<ResultDto<PreCompromisosResponseDto>> Create(PreCompromisosUpdateDto dto)
        {

            ResultDto<PreCompromisosResponseDto> result = new ResultDto<PreCompromisosResponseDto>(null);
            try
            {

                var conectado = await _sisUsuarioRepository.GetConectado();



                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año Invalido";
                    return result;
                }

                if (dto.CodigoSolicitud < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud Invalido";
                    return result;
                }
                var codigoSolicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (codigoSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud Invalido";
                    return result;
                }

                if(dto.NumeroCompromiso == string.Empty) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso no puede estar vacio";
                    return result;

                }

                if(dto.NumeroCompromiso.Length > 20) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso Invalido";
                    return result;

                }
                if (dto.FechaCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha compromiso Invalida";
                    return result;
                }


                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;

                }

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (codigoProveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.CodigoDirEntrega < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Dir entrega Invalido";
                    return result;
                }

                if (dto.Motivo.Length > 1150)
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

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                PRE_COMPROMISOS entity = new PRE_COMPROMISOS();
                entity.CODIGO_COMPROMISO = await _repository.GetNextKey();
                entity.ANO = dto.Ano;
                entity.CODIGO_SOLICITUD = dto.CodigoSolicitud;
                
                entity.NUMERO_COMPROMISO = dto.NumeroCompromiso;
                entity.FECHA_COMPROMISO = dto.FechaCompromiso;
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.FECHA_ENTREGA = dto.FechaEntrega;
                entity.CODIGO_DIR_ENTREGA = dto.CodigoDirEntrega;
                entity.TIPO_PAGO_ID = dto.TipoPagoId;
                entity.MOTIVO = dto.Motivo;
                entity.STATUS = dto.Status;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.TIPO_RENGLON_ID = dto.TipoRenglonId;
                entity.NUMERO_ORDEN = dto.NumeroOrden;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreCompromisos(created.Data);
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
        

        public async Task<ResultDto<PreCompromisosDeleteDto>> Delete(PreCompromisosDeleteDto dto)
        {

            ResultDto<PreCompromisosDeleteDto> result = new ResultDto<PreCompromisosDeleteDto>(null);
            try
            {

                var codigoCompromiso = await _repository.GetByCodigo(dto.CodigoCompromiso);
                if (codigoCompromiso == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Compromiso no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoCompromiso);

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


        public async Task<PreCompromisosResponseDto> GetByNumeroYFecha(string numeroCompromiso, DateTime fechaCompromiso)
        {
            PreCompromisosResponseDto result = new PreCompromisosResponseDto();
            try
            {

                var compromisos = await _repository.GetByNumeroYFecha(numeroCompromiso,fechaCompromiso);
                if (compromisos == null)
                {
                    return null;
                }


               result = await MapPreCompromisos(compromisos);
                

            }
            catch (Exception ex)
            {
             
               var message = ex.Message;
            }

            return result;
        }

    }
}

