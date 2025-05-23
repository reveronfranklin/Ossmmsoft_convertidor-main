﻿using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmOrdenPagoService : IAdmOrdenPagoService
    {
        public IPRE_V_SALDOSRepository PreSaldosRepository { get; }
        private readonly IAdmOrdenPagoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmCompromisoOpService _admCompromisoOpService;
        private readonly IPreCompromisosService _preCompromisosService;
        private readonly IPreDetalleCompromisosRepository _preDetalleCompromisosRepository;
        private readonly IPrePucCompromisosRepository _prePucCompromisosRepository;
        private readonly IAdmPucOrdenPagoService _admPucOrdenPagoService;
        private readonly IPRE_V_SALDOSRepository _preSaldosRepository;
        private readonly IAdmBeneficariosOpService _admBeneficariosOpService;
        private readonly IAdmPucOrdenPagoRepository _admPucOrdenPagoRepository;


        public AdmOrdenPagoService(IAdmOrdenPagoRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmProveedoresRepository admProveedoresRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IAdmCompromisoOpService admCompromisoOpService,
                                     IPreCompromisosService preCompromisosService,
                                     IPreDetalleCompromisosRepository preDetalleCompromisosRepository,
                                     IPrePucCompromisosRepository prePucCompromisosRepository,
                                     IAdmPucOrdenPagoService admPucOrdenPagoService,
                                     IPRE_V_SALDOSRepository   preSaldosRepository,
                                     IAdmBeneficariosOpService admBeneficariosOpService,
                                     IAdmPucOrdenPagoRepository admPucOrdenPagoRepository)
        {
      
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admProveedoresRepository = admProveedoresRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _admCompromisoOpService = admCompromisoOpService;
            _preCompromisosService = preCompromisosService;
            _preDetalleCompromisosRepository = preDetalleCompromisosRepository;
            _prePucCompromisosRepository = prePucCompromisosRepository;
            _admPucOrdenPagoService = admPucOrdenPagoService;
            _preSaldosRepository = preSaldosRepository;
            _admBeneficariosOpService = admBeneficariosOpService;
            _admPucOrdenPagoRepository = admPucOrdenPagoRepository;
        }


        public string GetDenominacionDescriptiva(List<ADM_DESCRIPTIVAS> descriptivas , int id)
        {
            string result = "";
            
            var descriptivaTipoOrdenPago =
                descriptivas.Where(x => x.DESCRIPCION_ID == id).FirstOrDefault();
            if (descriptivaTipoOrdenPago != null)
            {
                result = descriptivaTipoOrdenPago.DESCRIPCION;
            }

            return result;
        }
        
        public async Task<AdmOrdenPagoResponseDto> MapOrdenPagoDto(ADM_ORDEN_PAGO dtos,List<ADM_DESCRIPTIVAS> descriptivas,ADM_PROVEEDORES proveedor)
        {

       
            
            
            AdmOrdenPagoResponseDto itemResult = new AdmOrdenPagoResponseDto();
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.ANO = dtos.ANO;

            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.NombreProveedor = "";
            itemResult.NombreProveedor = proveedor.NOMBRE_PROVEEDOR;
          
            itemResult.NumeroOrdenPago = dtos.NUMERO_ORDEN_PAGO;
            itemResult.FechaOrdenPago = dtos.FECHA_ORDEN_PAGO;
            itemResult.FechaOrdenPagoString =Fecha.GetFechaString(dtos.FECHA_ORDEN_PAGO);
            ;            FechaDto fechaOrdenPagoObj = Fecha.GetFechaDto(dtos.FECHA_ORDEN_PAGO);
            itemResult.FechaOrdenPagoObj = (FechaDto)fechaOrdenPagoObj;
            
            itemResult.CantidadPago = dtos.CANTIDAD_PAGO;
            itemResult.TipoOrdenPagoId = dtos.TIPO_ORDEN_PAGO_ID;
            itemResult.DescripcionTipoOrdenPago = GetDenominacionDescriptiva(descriptivas , dtos.TIPO_ORDEN_PAGO_ID);
            itemResult.FrecuenciaPagoId = dtos.FRECUENCIA_PAGO_ID;
            itemResult.DescripcionFrecuencia = GetDenominacionDescriptiva(descriptivas ,(int)dtos.FRECUENCIA_PAGO_ID);
            itemResult.TipoPagoId = dtos.TIPO_PAGO_ID;
            itemResult.DescripcionTipoPago = GetDenominacionDescriptiva(descriptivas ,(int)dtos.TIPO_PAGO_ID);
            itemResult.Status = dtos.STATUS;
            itemResult.DescripcionStatus= Estatus.GetStatus( itemResult.Status );
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.NumeroComprobante = dtos.NUMERO_COMPROBANTE;
            if (dtos.FECHA_COMPROBANTE != null)
            {
                itemResult.FechaComprobante = (DateTime)dtos.FECHA_COMPROBANTE;
                itemResult.FechaComprobanteString = Fecha.GetFechaString((DateTime)dtos.FECHA_COMPROBANTE);
                FechaDto fechaComprobanteObj = Fecha.GetFechaDto((DateTime)dtos.FECHA_COMPROBANTE);
                itemResult.FechaComprobanteObj = (FechaDto)fechaComprobanteObj;
            }
          
        
            itemResult.NumeroComprobante2 = dtos.NUMERO_COMPROBANTE2;
            itemResult.numeroComprobante3 = dtos.NUMERO_COMPROBANTE3;
            itemResult.NumeroComprobante4 = dtos.NUMERO_COMPROBANTE4;
            itemResult.ConFactura = false;
            if (dtos.CON_FACTURA == 1)
            {
                itemResult.ConFactura = true;
            }
            return itemResult;
        }

        public async Task<ResultDto<List<AdmOrdenPagoResponseDto>>> GetByPresupuesto(AdmOrdenPagoFilterDto filter)
        {

            ResultDto<List<AdmOrdenPagoResponseDto>> result = new ResultDto<List<AdmOrdenPagoResponseDto>>(null);
            try
            {
                var ordenPago = await _repository.GetByPresupuesto(filter);
             
                if (ordenPago.Data != null )
                {
                    var listDto = await MapListOrdenPagoDto(ordenPago.Data);

                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";
                    result.CantidadRegistros = ordenPago.CantidadRegistros;
                    result.TotalPage = ordenPago.TotalPage;
                    result.Page = ordenPago.Page;


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

        public async Task<List<AdmOrdenPagoResponseDto>> MapListOrdenPagoDto(List<ADM_ORDEN_PAGO> dtos)
        {
            
            var descriptivas = await _admDescriptivaRepository.GetAll();
           
            List<AdmOrdenPagoResponseDto> result = new List<AdmOrdenPagoResponseDto>();
            {
                foreach (var item in dtos)
                {
                    var proveedores = await _admProveedoresRepository.GetByCodigo(item.CODIGO_PROVEEDOR);
                    var itemResult = await MapOrdenPagoDto(item,descriptivas,proveedores);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<AdmOrdenPagoResponseDto>> Update(AdmOrdenPagoUpdateDto dto)
        {
            ResultDto<AdmOrdenPagoResponseDto> result = new ResultDto<AdmOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago((int)dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }

                var modificable= await OrdenDePagoEsModificable((int)dto.CodigoOrdenPago);
                if (!modificable)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Orden de pago no es Modificable(Status o Tiene Pagos)";
                    return result;
                }
                var compromiso = await _preCompromisosService.GetByCompromiso(dto.CodigoCompromiso);
                if (compromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Compromiso Invalido";
                    return result;
                }

                if (compromiso.FechaCompromiso.Date > dto.FechaOrdenPago.Date)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"La fecha de la Orden de Pago({dto.FechaOrdenPago.Date}) no puede ser menor a la fecha del compromiso({compromiso.FechaCompromiso.Date})";
                    return result;
                }

                if (dto.FechaOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Orden Pago Invalida";
                    return result;
                }
                var tipoOrdenPago = await _admDescriptivaRepository.GetByCodigo(dto.TipoOrdenPagoId);
                if (tipoOrdenPago==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Orden Pago Invalido";
                    return result;
                }

             

                if (dto.CantidadPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Pago Invalida";
                    return result;
                }

            
                var frecuenciaPagoId = await _admDescriptivaRepository.GetByIdAndTitulo(15, dto.FrecuenciaPagoId);
                if (frecuenciaPagoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia Pago Invalido";
                    return result;
                }

                var tipoPagoId = await _admDescriptivaRepository.GetByIdAndTitulo(16, dto.TipoPagoId);
                if (tipoPagoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Pago Id Invalido";
                    return result;
                }

            
                

                if (dto.Motivo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
              
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto==null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
              

          

                if (dto.NumeroComprobante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante Invalido";
                    return result;
                }
                if (dto.FechaComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha comprobante Invalida";
                    return result;
                }

                if (dto.NumeroComprobante2 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante2 Invalido";
                    return result;
                }
                
                codigoOrdenPago.ANO = presupuesto.ANO;
                codigoOrdenPago.CODIGO_PROVEEDOR = compromiso.CodigoProveedor;
                codigoOrdenPago.FECHA_ORDEN_PAGO = dto.FechaOrdenPago;
                codigoOrdenPago.TIPO_ORDEN_PAGO_ID = dto.TipoOrdenPagoId;
                codigoOrdenPago.CANTIDAD_PAGO = dto.CantidadPago;
                codigoOrdenPago.FRECUENCIA_PAGO_ID = dto.FrecuenciaPagoId;
                codigoOrdenPago.TIPO_PAGO_ID = dto.TipoPagoId;
                codigoOrdenPago.MOTIVO = dto.Motivo;
                codigoOrdenPago.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoOrdenPago.NUMERO_COMPROBANTE = dto.NumeroComprobante;
                if (dto.FechaComprobante != null)
                {
                    codigoOrdenPago.FECHA_COMPROBANTE = (DateTime)dto.FechaComprobante;
                }

             
                codigoOrdenPago.NUMERO_COMPROBANTE2=dto.NumeroComprobante2;
                codigoOrdenPago.NUMERO_COMPROBANTE3 = dto.NumeroComprobante3;
                codigoOrdenPago.NUMERO_COMPROBANTE4 = dto.NumeroComprobante4;

                codigoOrdenPago.CON_FACTURA = 0;
                if (dto.ConFactura)
                {
                    codigoOrdenPago.CON_FACTURA = 1;
                }
                
                codigoOrdenPago.CODIGO_EMPRESA = conectado.Empresa;
                codigoOrdenPago.USUARIO_UPD = conectado.Usuario;
                codigoOrdenPago.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoOrdenPago);
                var descriptivas = await _admDescriptivaRepository.GetAll();
                var proveedores = await _admProveedoresRepository.GetByCodigo(codigoOrdenPago.CODIGO_PROVEEDOR);
                var resultDto = await MapOrdenPagoDto(codigoOrdenPago,descriptivas,proveedores);
                
                await _prePresupuestosRepository.RecalcularSaldo(codigoOrdenPago.CODIGO_PRESUPUESTO);
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

        public async Task<bool> OrdenDePagoEsModificable(int codigoOrdenPago)
        {
            bool result = false;
            
            var ordenPago = await _repository.GetCodigoOrdenPago(codigoOrdenPago);
            if (ordenPago != null)
            {
                if (ordenPago.STATUS == "PE")
                {
                    result = true;
                }
                if (ordenPago.STATUS == "AP" || ordenPago.STATUS == "AN")
                {
                    result = false;
                }
                var admPucOrdenPago = await _admPucOrdenPagoRepository.GetByOrdenPago(codigoOrdenPago);
                if(admPucOrdenPago != null && admPucOrdenPago.Count>0)
                {
                    var montoPagado = admPucOrdenPago.Sum(x => x.MONTO_PAGADO);
                    if (ordenPago.STATUS == "AP" && montoPagado == 0)
                    {
                        result = true;
                    }
                }
            }
        
            return result;

        }
      
        
        public async Task<ResultDto<bool>> CrearPucOrdenPagoDesdeCompromiso(int codigoCompromiso,int codigoOrdenPago)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);

            try
            {
                var detalleCompromiso = await _preDetalleCompromisosRepository.GetByCodigoCompromiso(codigoCompromiso);
                if (detalleCompromiso.Count > 0)
                {
                    foreach (var itemDetalle in detalleCompromiso)
                    {
                        var pucCompromiso =
                            await _prePucCompromisosRepository.GetListByCodigoDetalleCompromiso(itemDetalle
                                .CODIGO_DETALLE_COMPROMISO);
                        if (pucCompromiso.Count > 0)
                        {
                            foreach (var itemPucCompromiso in pucCompromiso)
                            {

                                if (itemPucCompromiso.MONTO - itemPucCompromiso.MONTO_CAUSADO > 0)
                                {
                                    AdmPucOrdenPagoUpdateDto newPuc = new AdmPucOrdenPagoUpdateDto();
                                    newPuc.CodigoOrdenPago = codigoOrdenPago;
                                    newPuc.CodigoPucOrdenPago = 0;
                                    newPuc.CodigoPucCompromiso = itemPucCompromiso.CODIGO_PUC_COMPROMISO;  
                                    newPuc.CodigoIcp = itemPucCompromiso.CODIGO_ICP;  
                                    newPuc.CodigoPuc = itemPucCompromiso.CODIGO_PUC;  
                                    newPuc.FinanciadoId = itemPucCompromiso.FINANCIADO_ID;  
                                    newPuc.CodigoFinanciado = itemPucCompromiso.CODIGO_FINANCIADO;  
                                    newPuc.CodigoSaldo = itemPucCompromiso.CODIGO_SALDO;  
                                    newPuc.CodigoSaldo = itemPucCompromiso.CODIGO_SALDO;  
                                    newPuc.Monto = itemPucCompromiso.MONTO-itemPucCompromiso.MONTO_CAUSADO;  
                                    newPuc.MontoCompromiso= itemPucCompromiso.MONTO; 
                                    newPuc.MontoPagado = 0;  
                                    newPuc.MontoAnulado =0;  
                                    newPuc.Extra1 ="";  
                                    newPuc.Extra2 ="";  
                                    newPuc.Extra3 ="";  
                                    newPuc.CodigoCompromisoOp =codigoCompromiso;  
                                    newPuc.CodigoPresupuesto =itemPucCompromiso.CODIGO_PRESUPUESTO;
                                    await _admPucOrdenPagoService.Create(newPuc);
                                
                              
                                    var total = await _admPucOrdenPagoRepository.GetTotalByCodigoCompromiso(itemPucCompromiso.CODIGO_PUC_COMPROMISO);

                                    await _prePucCompromisosRepository.UpdateMontoCausadoById(
                                        itemPucCompromiso.CODIGO_PUC_COMPROMISO, total);
                                }
 
                            }
                        }
                        
                    }
                    
                }

                result.Message = "";
                result.IsValid = true;
                result.Data = true;
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.IsValid = false;
                result.Data = false;
                return result;
            }
            
         
        }
        
        public async Task<ResultDto<AdmOrdenPagoResponseDto>> Create(AdmOrdenPagoUpdateDto dto)
        {
            ResultDto<AdmOrdenPagoResponseDto> result = new ResultDto<AdmOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago((int)dto.CodigoOrdenPago);
                if (codigoOrdenPago != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo  existe";
                    return result;
                }
              
                var compromiso = await _preCompromisosService.GetByCompromiso(dto.CodigoCompromiso);
                if (compromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Compromiso Invalido";
                    return result;
                }

                
                if (dto.FechaOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Orden Pago Invalida";
                    return result;
                }
                var tipoOrdenPagoId = await _admDescriptivaRepository.GetByCodigo(dto.TipoOrdenPagoId);
                if (tipoOrdenPagoId==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Orden Pago Invalido";
                    return result;
                }
                

                if (dto.CantidadPago <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Pago Invalida";
                    return result;
                }

             
                var frecuenciaPagoId = await _admDescriptivaRepository.GetByCodigo(dto.FrecuenciaPagoId);
                if (frecuenciaPagoId==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia Pago Invalido";
                    return result;
                }

                var tipoPagoId = await _admDescriptivaRepository.GetByCodigo(dto.TipoPagoId);
                if (tipoPagoId==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Pago Id Invalido";
                    return result;
                }

             

                if (dto.Motivo == null || dto.Motivo.Length<2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
             

                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto==null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
                
               
           

                if (dto.NumeroComprobante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante Invalido";
                    return result;
                }
                if (dto.NumeroComprobante > 0 && dto.FechaComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha comprobante Invalida";
                    return result;
                }

                if (dto.NumeroComprobante2 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante2 Invalido";
                    return result;
                }
               

            ADM_ORDEN_PAGO entity = new ADM_ORDEN_PAGO();
            entity.CODIGO_ORDEN_PAGO = await _repository.GetNextKey();
            entity.ANO = presupuesto.ANO;
            entity.CODIGO_PROVEEDOR = compromiso.CodigoProveedor;
            entity.NUMERO_ORDEN_PAGO = await _repository.GetNextOrdenPago(dto.CodigoPresupuesto);
            entity.FECHA_ORDEN_PAGO = DateTime.Now;
            entity.TIPO_ORDEN_PAGO_ID = dto.TipoOrdenPagoId;
            entity.CANTIDAD_PAGO = dto.CantidadPago;
            entity.FRECUENCIA_PAGO_ID = dto.FrecuenciaPagoId;
            entity.TIPO_PAGO_ID = dto.TipoPagoId;
            entity.STATUS = "PE";
            entity.MOTIVO = dto.Motivo;
            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            entity.NUMERO_COMPROBANTE = dto.NumeroComprobante;
            if (dto.FechaComprobante != null)
            {
                entity.FECHA_COMPROBANTE = (DateTime)dto.FechaComprobante;
            }
          
            entity.NUMERO_COMPROBANTE2 = dto.NumeroComprobante2;
            entity.NUMERO_COMPROBANTE3 = dto.NumeroComprobante3;
            entity.NUMERO_COMPROBANTE4 = dto.NumeroComprobante4;
            entity.FECHA_PLAZO_DESDE = dto.FechaPlazoDesde;
            entity.FECHA_PLAZO_HASTA = dto.FechaPlazoHasta;
            entity.CON_FACTURA = 0;
            if (dto.ConFactura)
            {
                entity.CON_FACTURA = 1;
            }
            
            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                
                //CREAR LOS PUC a parir del compromiso
                await CrearPucOrdenPagoDesdeCompromiso(dto.CodigoCompromiso,created.Data.CODIGO_ORDEN_PAGO);
                
                //CREAMOS EL COMPROMISO DE LA ORDEN DE PAGO
                AdmCompromisoOpUpdateDto compromisoOp = new AdmCompromisoOpUpdateDto();
                compromisoOp.CodigoCompromisoOp = 0;
                compromisoOp.CodigoProveedor = compromiso.CodigoProveedor;
                compromisoOp.CodigoPresupuesto = compromiso.CodigoPresupuesto;
                compromisoOp.CodigoOrdenPago = created.Data.CODIGO_ORDEN_PAGO;
                compromisoOp.CodigoValContrato = 0;
                compromisoOp.OrigenCompromisoId = dto.TipoOrdenPagoId; 
                compromisoOp.CodigoIdentificador = compromiso.CodigoCompromiso; 
                var compromisOpCreated = await _admCompromisoOpService.Create(compromisoOp);
                
               
                await _prePresupuestosRepository.RecalcularSaldo(created.Data.CODIGO_PRESUPUESTO);
             
                
                var descriptivas = await _admDescriptivaRepository.GetAll();
                var proveedores = await _admProveedoresRepository.GetByCodigo(compromisoOp.CodigoProveedor );
                var resultDto = await MapOrdenPagoDto(created.Data,descriptivas,proveedores);

                result.Data = resultDto; 
                //resultDto;
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

        public async Task<ResultDto<AdmOrdenPagoDeleteDto>> Delete(AdmOrdenPagoDeleteDto dto) 
        {
            ResultDto<AdmOrdenPagoDeleteDto> result = new ResultDto<AdmOrdenPagoDeleteDto>(null);
            try
            {

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }
                var modificable= await OrdenDePagoEsModificable((int)dto.CodigoOrdenPago);
                if (!modificable)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Orden de pago no es Modificable(Status o Tiene Pagos)";
                    return result;
                }

                if (codigoOrdenPago.STATUS != "PE")
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no Puede ser Eliminada, necesita estar en Status PENDIENTE";
                    return result;
                }

                var pucOrdenPago = await _admPucOrdenPagoRepository.GetByOrdenPago(dto.CodigoOrdenPago);
                if (pucOrdenPago != null && pucOrdenPago.Count > 0)
                {

                    foreach (var item in pucOrdenPago)
                    {
                        await _prePucCompromisosRepository.UpdateMontoCausadoById(
                            item.CODIGO_PUC_COMPROMISO, 0);
                    }
                   
                }
             
              

                var deleted = await _repository.Delete(dto.CodigoOrdenPago);

                await _prePresupuestosRepository.RecalcularSaldo(codigoOrdenPago.CODIGO_PRESUPUESTO);

                
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


        public async Task<string> ValidaAprobarOrdenPago(ADM_ORDEN_PAGO ordenPago)
        {
            
            string result = "";
            decimal totalMontoBeneficiariosOp=0;
            decimal totalMontoPucOrdenPago = 0;
            if (ordenPago.STATUS != "PE")
            {
               
                result = "Orden de pago no esta pendiente";
               
            }
            AdmOrdenPagoBeneficiarioFlterDto filter = new AdmOrdenPagoBeneficiarioFlterDto();
            filter.CodigoPresupuesto = ordenPago.CODIGO_PRESUPUESTO;
            filter.CodigoOrdenPago = ordenPago.CODIGO_ORDEN_PAGO;
            var admBeneficiarios = await _admBeneficariosOpService.GetByOrdenPago(filter);
            if (admBeneficiarios.Data.Count > 0)
            {
                totalMontoBeneficiariosOp = admBeneficiarios.Data.Sum(t => t.Monto);
            }
            
            var admPucOrdenPago =await  _admPucOrdenPagoService.GetByOrdenPago(ordenPago.CODIGO_ORDEN_PAGO);
            if (admPucOrdenPago.Data.Count > 0)
            {
                 
                totalMontoPucOrdenPago = admPucOrdenPago.Data.Sum(t => t.Monto);
            }

            if (totalMontoBeneficiariosOp != totalMontoPucOrdenPago)
            {
                result = $"Monto en Beneficiarios es diferente a sus PUC. Total Beneficiarios OP: {totalMontoBeneficiariosOp} - Total PUC OP: {totalMontoPucOrdenPago}";
            }
            
            
            return result;

        }
        
        
        public async Task<string> ValidaAnularOrdenPago(ADM_ORDEN_PAGO ordenPago)
        {
            
            string result = "";
            decimal totalPagado=0;
       
            if (ordenPago.STATUS != "AP")
            {
               
                result = "Orden de pago no esta pendiente";
               
            }
            AdmOrdenPagoBeneficiarioFlterDto filter = new AdmOrdenPagoBeneficiarioFlterDto();
           
            
            var admPucOrdenPago =await  _admPucOrdenPagoService.GetByOrdenPago(ordenPago.CODIGO_ORDEN_PAGO);
            if (admPucOrdenPago.Data.Count > 0)
            {
                 
                totalPagado = admPucOrdenPago.Data.Sum(t => t.MontoPagado);
            }

            if (totalPagado!=0)
            {
                result = $"Orden de Pago ya tiene Pagos: {totalPagado} ";
            }
            
            
            return result;

        }


        
        public async Task<ResultDto<AdmOrdenPagoResponseDto>> Aprobar(AdmOrdenPagoAprobarAnular dto)
        {
            ResultDto<AdmOrdenPagoResponseDto> result = new ResultDto<AdmOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }

                var validaAprobarOrdenPago =await ValidaAprobarOrdenPago(codigoOrdenPago);
                
                if (validaAprobarOrdenPago!="")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message =validaAprobarOrdenPago;
                    return result;
                }

                codigoOrdenPago.STATUS = "AP";

                
                codigoOrdenPago.CODIGO_EMPRESA = conectado.Empresa;
                codigoOrdenPago.USUARIO_UPD = conectado.Usuario;
                codigoOrdenPago.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoOrdenPago);
                
                
                await _preSaldosRepository.RecalcularSaldo(codigoOrdenPago.CODIGO_PRESUPUESTO);
                var descriptivas = await _admDescriptivaRepository.GetAll();
                var proveedores = await _admProveedoresRepository.GetByCodigo(codigoOrdenPago.CODIGO_PROVEEDOR);
                var resultDto = await MapOrdenPagoDto(codigoOrdenPago,descriptivas,proveedores);
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

        public async Task<ResultDto<AdmOrdenPagoResponseDto>> Anular(AdmOrdenPagoAprobarAnular dto)
        {
            ResultDto<AdmOrdenPagoResponseDto> result = new ResultDto<AdmOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }

                var validaAnularOrdenPago =await ValidaAnularOrdenPago(codigoOrdenPago);
                
                if (validaAnularOrdenPago!="")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message =validaAnularOrdenPago;
                    return result;
                }

                codigoOrdenPago.STATUS = "AN";

                
                codigoOrdenPago.CODIGO_EMPRESA = conectado.Empresa;
                codigoOrdenPago.USUARIO_UPD = conectado.Usuario;
                codigoOrdenPago.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoOrdenPago);
                
                await _preSaldosRepository.RecalcularSaldo(codigoOrdenPago.CODIGO_PRESUPUESTO);
                var descriptivas = await _admDescriptivaRepository.GetAll();
                var proveedores = await _admProveedoresRepository.GetByCodigo(codigoOrdenPago.CODIGO_PROVEEDOR);
                var resultDto = await MapOrdenPagoDto(codigoOrdenPago,descriptivas,proveedores);
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

