
using Convertidor.Data.DestinoInterfaces.ADM;
using Convertidor.Data.DestinoInterfaces.PRE;
using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.EntitiesDestino.PRE;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Utility;


namespace Convertidor.Services.Destino.ADM
{
    public class AdmOrdenPagoDestinoService : IAdmOrdenPagoDestinoService
    {
        private readonly IAdmOrdenPagoRepository _repository;
        private readonly IAdmOrdenPagoDestinoRepository _destinoRepository;
        private readonly IAdmPucOrdenPagoRepository _pucOrdenPagoRepository;
        private readonly IAdmPucOrdenPagoDestinoRepository _pucOrdenPagoDestinoRepository;
        private readonly IAdmBeneficiariosOpRepository _admBeneficiariosOpRepository;
        private readonly IAdmBeneficiariosOpDestinoRepository _admBeneficiariosOpDestinoRepository;
        private readonly IAdmRetencionesOpRepository _admRetencionesOpRepository;
        private readonly IAdmRetencionesOpDestinoRepository _admRetencionesOpDestinoRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IAdmProveedoresDestinoRepository _admProveedoresDestinoRepository;
        private readonly IAdmContactosProveedorRepository _admContactosProveedorRepository;
        private readonly IAdmContactosProveedorDestinoRepository _admContactosProveedorDestinoRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmDescriptivaDestinoRepository _admDescriptivaDestinoRepository;
        private readonly IPreVSaldoDestinoRepository _preVSaldoDestinoRepository;
        private readonly IPRE_V_SALDOSRepository _preVSaldosRepository;


        public AdmOrdenPagoDestinoService(
                        IAdmOrdenPagoRepository repository,
                        IAdmOrdenPagoDestinoRepository destinoRepository ,
                        IAdmPucOrdenPagoRepository pucOrdenPagoRepository,
                        IAdmPucOrdenPagoDestinoRepository pucOrdenPagoDestinoRepository,
                        IAdmBeneficiariosOpRepository admBeneficiariosOpRepository,
                        IAdmBeneficiariosOpDestinoRepository admBeneficiariosOpDestinoRepository,
                        IAdmRetencionesOpRepository admRetencionesOpRepository,
                        IAdmRetencionesOpDestinoRepository admRetencionesOpDestinoRepository,
                        IAdmProveedoresRepository admProveedoresRepository,
                        IAdmProveedoresDestinoRepository admProveedoresDestinoRepository,
                        IAdmContactosProveedorRepository admContactosProveedorRepository,
                        IAdmContactosProveedorDestinoRepository admContactosProveedorDestinoRepository,
                        IAdmDescriptivaRepository admDescriptivaRepository,
                        IAdmDescriptivaDestinoRepository admDescriptivaDestinoRepository,
                        IPreVSaldoDestinoRepository preVSaldoDestinoRepository,
                        IPRE_V_SALDOSRepository preVSaldosRepository)
        {
            _repository = repository;
            _destinoRepository = destinoRepository;
            _pucOrdenPagoRepository = pucOrdenPagoRepository;
            _pucOrdenPagoDestinoRepository = pucOrdenPagoDestinoRepository;
            _admBeneficiariosOpRepository = admBeneficiariosOpRepository;
            _admBeneficiariosOpDestinoRepository = admBeneficiariosOpDestinoRepository;
            _admRetencionesOpRepository = admRetencionesOpRepository;
            _admRetencionesOpDestinoRepository = admRetencionesOpDestinoRepository;
            _admProveedoresRepository = admProveedoresRepository;
            _admProveedoresDestinoRepository = admProveedoresDestinoRepository;
            _admContactosProveedorRepository = admContactosProveedorRepository;
            _admContactosProveedorDestinoRepository = admContactosProveedorDestinoRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _admDescriptivaDestinoRepository = admDescriptivaDestinoRepository;
            _preVSaldoDestinoRepository = preVSaldoDestinoRepository;
            _preVSaldosRepository = preVSaldosRepository;
        }

        
        public PRE_V_SALDOS MapPreVSaldo(Data.Entities.Presupuesto.PRE_V_SALDOS prevSaldoOrigen)
        {
           
            PRE_V_SALDOS newSaldo = new PRE_V_SALDOS();
            newSaldo.CODIGO_SALDO = prevSaldoOrigen.CODIGO_SALDO;
            newSaldo.ANO  = prevSaldoOrigen.ANO;
            newSaldo.FINANCIADO_ID = prevSaldoOrigen.FINANCIADO_ID;
            newSaldo.CODIGO_FINANCIADO  = prevSaldoOrigen.CODIGO_FINANCIADO;
            newSaldo.DESCRIPCION_FINANCIADO = prevSaldoOrigen.DESCRIPCION_FINANCIADO;
            newSaldo.CODIGO_ICP  = prevSaldoOrigen.CODIGO_ICP;
            newSaldo.CODIGO_SECTOR  = prevSaldoOrigen.CODIGO_SECTOR;
            newSaldo.CODIGO_PROGRAMA  = prevSaldoOrigen.CODIGO_PROGRAMA;
            newSaldo.CODIGO_SUBPROGRAMA  = prevSaldoOrigen.CODIGO_SUBPROGRAMA;
            newSaldo.CODIGO_PROYECTO  = prevSaldoOrigen.CODIGO_PROYECTO;
            newSaldo.CODIGO_ACTIVIDAD = prevSaldoOrigen.CODIGO_ACTIVIDAD;
            newSaldo.CODIGO_OFICINA  = prevSaldoOrigen.CODIGO_OFICINA;
            newSaldo.CODIGO_ICP_CONCAT  = prevSaldoOrigen.CODIGO_ICP_CONCAT;
            newSaldo.DENOMINACION_ICP  = prevSaldoOrigen.DENOMINACION_ICP;
            newSaldo.UNIDAD_EJECUTORA  = prevSaldoOrigen.UNIDAD_EJECUTORA;
            newSaldo.CODIGO_PUC  = prevSaldoOrigen.CODIGO_PUC;
            newSaldo.CODIGO_GRUPO  = prevSaldoOrigen.CODIGO_GRUPO;
            newSaldo.CODIGO_PARTIDA  = prevSaldoOrigen.CODIGO_PARTIDA;
            newSaldo.CODIGO_GENERICA  = prevSaldoOrigen.CODIGO_GENERICA;
            newSaldo.CODIGO_ESPECIFICA  = prevSaldoOrigen.CODIGO_ESPECIFICA;
            newSaldo.CODIGO_SUBESPECIFICA  = prevSaldoOrigen.CODIGO_SUBESPECIFICA;
            newSaldo.CODIGO_NIVEL5 = prevSaldoOrigen.CODIGO_NIVEL5;
            newSaldo.CODIGO_PUC_CONCAT  = prevSaldoOrigen.CODIGO_PUC_CONCAT;
            newSaldo.DENOMINACION_PUC  = prevSaldoOrigen.DENOMINACION_PUC;
            newSaldo.PRESUPUESTADO  = prevSaldoOrigen.PRESUPUESTADO;
            newSaldo.ASIGNACION  = prevSaldoOrigen.ASIGNACION;
            newSaldo.BLOQUEADO  = prevSaldoOrigen.BLOQUEADO;
            newSaldo.MODIFICADO  = prevSaldoOrigen.BLOQUEADO;
            newSaldo.AJUSTADO  = prevSaldoOrigen.AJUSTADO;
            newSaldo.VIGENTE = prevSaldoOrigen.VIGENTE;
            newSaldo.COMPROMETIDO  = prevSaldoOrigen.COMPROMETIDO;
            newSaldo.POR_COMPROMETIDO  = prevSaldoOrigen.POR_COMPROMETIDO;
            newSaldo.DISPONIBLE  = prevSaldoOrigen.DISPONIBLE;
            newSaldo.CAUSADO  = prevSaldoOrigen.CAUSADO;
            newSaldo.POR_CAUSADO  = prevSaldoOrigen.POR_CAUSADO;
            newSaldo.PAGADO  = prevSaldoOrigen.PAGADO;
            newSaldo.POR_PAGADO  = prevSaldoOrigen.POR_PAGADO;
            newSaldo.CODIGO_EMPRESA  = prevSaldoOrigen.CODIGO_EMPRESA;
            newSaldo.CODIGO_PRESUPUESTO  = prevSaldoOrigen.CODIGO_PRESUPUESTO;
            newSaldo.FECHA_SOLICITUD  = prevSaldoOrigen.FECHA_SOLICITUD;
            newSaldo.DESCRIPTIVA_FINANCIADO = prevSaldoOrigen.DESCRIPTIVA_FINANCIADO;
            newSaldo.SEARCH_TEXT  = prevSaldoOrigen.SEARCH_TEXT;
            return newSaldo;
        }
        
        public ADM_DESCRIPTIVAS MapDescriptiva(Data.Entities.Adm.ADM_DESCRIPTIVAS descriptivaOrigen)
        {
           
            ADM_DESCRIPTIVAS newDescriptiva = new ADM_DESCRIPTIVAS();
            newDescriptiva.DESCRIPCION_ID = descriptivaOrigen.DESCRIPCION_ID;
            newDescriptiva.DESCRIPCION_FK_ID = descriptivaOrigen.DESCRIPCION_FK_ID;
            newDescriptiva.TITULO_ID = descriptivaOrigen.TITULO_ID;
            newDescriptiva.DESCRIPCION = descriptivaOrigen.DESCRIPCION;
            newDescriptiva.CODIGO = descriptivaOrigen.CODIGO;
            newDescriptiva.EXTRA1 = descriptivaOrigen.EXTRA1;
            newDescriptiva.EXTRA2 = descriptivaOrigen.EXTRA2;
            newDescriptiva.EXTRA3= descriptivaOrigen.EXTRA3;
            newDescriptiva.USUARIO_INS = descriptivaOrigen.USUARIO_INS;
            newDescriptiva.FECHA_INS = descriptivaOrigen.FECHA_INS;
            newDescriptiva.USUARIO_UPD = descriptivaOrigen.USUARIO_UPD;
            newDescriptiva.FECHA_UPD = descriptivaOrigen.FECHA_UPD;
            newDescriptiva.CODIGO_EMPRESA = descriptivaOrigen.CODIGO_EMPRESA;
            return newDescriptiva;
        }
        
        public ADM_PROVEEDORES MapProveedor(Data.Entities.Adm.ADM_PROVEEDORES proveedorOrigen)
        {
           
            ADM_PROVEEDORES newProveedor = new ADM_PROVEEDORES();
            newProveedor.CODIGO_PROVEEDOR = proveedorOrigen.CODIGO_PROVEEDOR;	
            newProveedor.NOMBRE_PROVEEDOR  = proveedorOrigen.NOMBRE_PROVEEDOR;
            newProveedor.TIPO_PROVEEDOR_ID  = proveedorOrigen.TIPO_PROVEEDOR_ID;
            newProveedor.NACIONALIDAD = proveedorOrigen.NACIONALIDAD;
            newProveedor.CEDULA = proveedorOrigen.CEDULA;
            newProveedor.RIF  = proveedorOrigen.RIF;
            newProveedor.FECHA_RIF  = proveedorOrigen.FECHA_RIF;
            newProveedor.NIT = proveedorOrigen.NIT;
            newProveedor.FECHA_NIT = proveedorOrigen.FECHA_NIT;
            newProveedor.NUMERO_REGISTRO_CONTRALORIA  = proveedorOrigen.NUMERO_REGISTRO_CONTRALORIA;
            newProveedor.FECHA_REGISTRO_CONTRALORIA = proveedorOrigen.FECHA_REGISTRO_CONTRALORIA;
            newProveedor.CAPITAL_PAGADO  = proveedorOrigen.CAPITAL_PAGADO;
            newProveedor.CAPITAL_SUSCRITO  = proveedorOrigen.CAPITAL_SUSCRITO;
            newProveedor.TIPO_IMPUESTO_ID = proveedorOrigen.TIPO_IMPUESTO_ID;
            newProveedor.STATUS  = proveedorOrigen.STATUS;
            newProveedor.CODIGO_PERSONA = proveedorOrigen.CODIGO_PERSONA;
            newProveedor.CODIGO_AUXILIAR_GASTO_X_PAGAR  = proveedorOrigen.CODIGO_AUXILIAR_GASTO_X_PAGAR;
            newProveedor.CODIGO_AUXILIAR_ORDEN_PAGO  = proveedorOrigen.CODIGO_AUXILIAR_ORDEN_PAGO;
            newProveedor.ESTATUS_FISCO_ID = proveedorOrigen.ESTATUS_FISCO_ID;
            newProveedor.NUMERO_CUENTA = proveedorOrigen.NUMERO_CUENTA;
            newProveedor.EXTRA1 = proveedorOrigen.EXTRA1;
            newProveedor.EXTRA2 = proveedorOrigen.EXTRA2;
            newProveedor.EXTRA3 = proveedorOrigen.EXTRA3;
            newProveedor.USUARIO_INS = proveedorOrigen.USUARIO_INS;
            newProveedor.FECHA_INS = proveedorOrigen.FECHA_INS;
            newProveedor.USUARIO_UPD = proveedorOrigen.USUARIO_UPD;
            newProveedor.FECHA_UPD = proveedorOrigen.FECHA_UPD;
            newProveedor.CODIGO_EMPRESA = proveedorOrigen.CODIGO_EMPRESA;
            return newProveedor;
        }
        
        public ADM_ORDEN_PAGO Map(Data.Entities.Adm.ADM_ORDEN_PAGO ordenPagoOrigen)
        {
           
            ADM_ORDEN_PAGO newOrden = new ADM_ORDEN_PAGO();
            newOrden.CODIGO_ORDEN_PAGO = ordenPagoOrigen.CODIGO_ORDEN_PAGO;
            newOrden.ANO  = ordenPagoOrigen.ANO;
            newOrden.CODIGO_COMPROMISO = ordenPagoOrigen.CODIGO_COMPROMISO;
            newOrden.CODIGO_ORDEN_COMPRA= ordenPagoOrigen.CODIGO_ORDEN_COMPRA;
            newOrden.CODIGO_CONTRATO = ordenPagoOrigen.CODIGO_CONTRATO;
            newOrden.CODIGO_PROVEEDOR= ordenPagoOrigen.CODIGO_PROVEEDOR;
            newOrden.NUMERO_ORDEN_PAGO = ordenPagoOrigen.NUMERO_ORDEN_PAGO;
            newOrden.REFERENCIA_ORDEN_PAGO = ordenPagoOrigen.REFERENCIA_ORDEN_PAGO;
            newOrden.FECHA_ORDEN_PAGO = ordenPagoOrigen.FECHA_ORDEN_PAGO;
            newOrden.TIPO_ORDEN_PAGO_ID = ordenPagoOrigen.TIPO_ORDEN_PAGO_ID;
            newOrden.FECHA_PLAZO_DESDE = ordenPagoOrigen.FECHA_PLAZO_DESDE;
            newOrden.FECHA_PLAZO_HASTA = ordenPagoOrigen.FECHA_PLAZO_HASTA;
            newOrden.CANTIDAD_PAGO = ordenPagoOrigen.CANTIDAD_PAGO;
            newOrden.NUMERO_PAGO= ordenPagoOrigen.NUMERO_PAGO;
            newOrden.FRECUENCIA_PAGO_ID= ordenPagoOrigen.FRECUENCIA_PAGO_ID;
            newOrden.TIPO_PAGO_ID = ordenPagoOrigen.TIPO_PAGO_ID;
            newOrden.NUMERO_VALUACION= ordenPagoOrigen.NUMERO_VALUACION;
            newOrden.STATUS = ordenPagoOrigen.STATUS;
            newOrden.MOTIVO = ordenPagoOrigen.MOTIVO;
            newOrden.EXTRA1 = ordenPagoOrigen.EXTRA1;
            newOrden.EXTRA2 = ordenPagoOrigen.EXTRA2;
            newOrden.EXTRA3 = ordenPagoOrigen.EXTRA3;
            newOrden.USUARIO_INS = ordenPagoOrigen.USUARIO_INS;
            newOrden.FECHA_INS = ordenPagoOrigen.FECHA_INS;
            newOrden.USUARIO_UPD = ordenPagoOrigen.USUARIO_UPD;
            newOrden.FECHA_UPD = ordenPagoOrigen.FECHA_UPD;
            newOrden.CODIGO_EMPRESA = ordenPagoOrigen.CODIGO_EMPRESA;
            newOrden.CODIGO_PRESUPUESTO = ordenPagoOrigen.CODIGO_PRESUPUESTO;
            newOrden.EXTRA4 = ordenPagoOrigen.EXTRA4;
            newOrden.EXTRA5 = ordenPagoOrigen.EXTRA5;
            newOrden.EXTRA6 = ordenPagoOrigen.EXTRA6;
            newOrden.EXTRA7 = ordenPagoOrigen.EXTRA7;
            newOrden.EXTRA8 = ordenPagoOrigen.EXTRA8;
            newOrden.EXTRA9 = ordenPagoOrigen.EXTRA9;
            newOrden.EXTRA10 = ordenPagoOrigen.EXTRA10;
            newOrden.EXTRA11  = ordenPagoOrigen.EXTRA11;
            newOrden.EXTRA12  = ordenPagoOrigen.EXTRA12;
            newOrden.EXTRA13  = ordenPagoOrigen.EXTRA13;
            newOrden.EXTRA14  = ordenPagoOrigen.EXTRA14;
            newOrden.EXTRA15  = ordenPagoOrigen.EXTRA15;
            newOrden.NUMERO_COMPROBANTE = ordenPagoOrigen.NUMERO_COMPROBANTE;
            newOrden.FECHA_COMPROBANTE = ordenPagoOrigen.FECHA_COMPROBANTE;
            newOrden.NUMERO_COMPROBANTE2 = ordenPagoOrigen.NUMERO_COMPROBANTE2;
            newOrden.NUMERO_COMPROBANTE3 = ordenPagoOrigen.NUMERO_COMPROBANTE3;
            newOrden.NUMERO_COMPROBANTE4 = ordenPagoOrigen.NUMERO_COMPROBANTE4;
            newOrden.SEARCH_TEXT = ordenPagoOrigen.SEARCH_TEXT;
            return newOrden;
        }

        public List<ADM_PUC_ORDEN_PAGO> MapPuc(List<Data.Entities.Adm.ADM_PUC_ORDEN_PAGO> pucOrdenPagoOrigen)
        {
            List<ADM_PUC_ORDEN_PAGO> newDestido = new List<ADM_PUC_ORDEN_PAGO>();
            foreach (var item in pucOrdenPagoOrigen)
            {
                ADM_PUC_ORDEN_PAGO itemNewDestido = new ADM_PUC_ORDEN_PAGO();
                itemNewDestido.CODIGO_PUC_ORDEN_PAGO = item.CODIGO_PUC_ORDEN_PAGO;
                itemNewDestido.CODIGO_ORDEN_PAGO =item.CODIGO_ORDEN_PAGO;
                itemNewDestido.CODIGO_PUC_COMPROMISO= item.CODIGO_PUC_COMPROMISO;
                itemNewDestido.CODIGO_ICP= item.CODIGO_ICP;
                itemNewDestido.CODIGO_PUC= item.CODIGO_PUC;
                itemNewDestido.FINANCIADO_ID =item.FINANCIADO_ID;
                itemNewDestido.CODIGO_FINANCIADO =item.CODIGO_FINANCIADO;
                itemNewDestido.CODIGO_SALDO =item.CODIGO_SALDO;
                itemNewDestido.MONTO =item.MONTO;
                itemNewDestido.MONTO_PAGADO =item.MONTO_PAGADO;
                itemNewDestido.MONTO_ANULADO =item.MONTO_ANULADO;
                itemNewDestido.EXTRA1=item.EXTRA1;
                itemNewDestido.EXTRA2 =item.EXTRA2;
                itemNewDestido.EXTRA3 =item.EXTRA3;
                itemNewDestido.USUARIO_INS=item.USUARIO_INS;
                itemNewDestido.FECHA_INS=item.FECHA_INS;
                itemNewDestido.USUARIO_UPD=item.USUARIO_UPD;
                itemNewDestido.FECHA_UPD=item.FECHA_UPD;
                itemNewDestido.FECHA_UPD=item.FECHA_UPD;
                itemNewDestido.CODIGO_COMPROMISO_OP=item.CODIGO_COMPROMISO_OP;
                itemNewDestido.CODIGO_PRESUPUESTO=item.CODIGO_PRESUPUESTO;
                newDestido.Add(itemNewDestido);
            }
           
            return newDestido;
        }
        public List<ADM_BENEFICIARIOS_OP> MapBeneficiario(List<Data.Entities.Adm.ADM_BENEFICIARIOS_OP> beneficiarioOrigen)
        {
            List<ADM_BENEFICIARIOS_OP> newDestido = new List<ADM_BENEFICIARIOS_OP>();
            foreach (var item in beneficiarioOrigen)
            {
                ADM_BENEFICIARIOS_OP itemNewDestido = new ADM_BENEFICIARIOS_OP();
                itemNewDestido.CODIGO_BENEFICIARIO_OP = item.CODIGO_BENEFICIARIO_OP;
                itemNewDestido.CODIGO_ORDEN_PAGO = item.CODIGO_ORDEN_PAGO;
                itemNewDestido.CODIGO_PROVEEDOR = item.CODIGO_PROVEEDOR;
                itemNewDestido.CODIGO_CONTACTO_PROVEEDOR = item.CODIGO_CONTACTO_PROVEEDOR;
                itemNewDestido.MONTO = item.MONTO;
                itemNewDestido.MONTO_PAGADO = item.MONTO_PAGADO;
                itemNewDestido.MONTO_ANULADO = item.MONTO_ANULADO;
                itemNewDestido.EXTRA1 = item.EXTRA1;
                itemNewDestido.EXTRA2 = item.EXTRA2;
                itemNewDestido.EXTRA3 = item.EXTRA3;
                itemNewDestido.USUARIO_INS = item.USUARIO_INS;
                itemNewDestido.FECHA_INS = item.FECHA_INS;
                itemNewDestido.USUARIO_UPD = item.USUARIO_UPD;
                itemNewDestido.FECHA_UPD = item.FECHA_UPD;
                itemNewDestido.CODIGO_EMPRESA = item.CODIGO_EMPRESA;
                itemNewDestido.CODIGO_PRESUPUESTO = item.CODIGO_PRESUPUESTO;
                newDestido.Add(itemNewDestido);
            }
           
            return newDestido;
        }

        public List<ADM_RETENCIONES_OP> MapRetenciones(List<Data.Entities.Adm.ADM_RETENCIONES_OP> retencionesOrigen)
        {
            List<ADM_RETENCIONES_OP> newDestido = new List<ADM_RETENCIONES_OP>();
            foreach (var item in retencionesOrigen)
            {
                ADM_RETENCIONES_OP itemNewDestido = new ADM_RETENCIONES_OP();
                itemNewDestido.CODIGO_RETENCION_OP = item.CODIGO_RETENCION_OP;
                itemNewDestido.CODIGO_ORDEN_PAGO = item.CODIGO_ORDEN_PAGO;
                itemNewDestido.TIPO_RETENCION_ID = item.TIPO_RETENCION_ID;
                itemNewDestido.CODIGO_RETENCION = item.CODIGO_RETENCION;
                itemNewDestido.POR_RETENCION = item.POR_RETENCION;
                itemNewDestido.MONTO_RETENCION = item.MONTO_RETENCION;
                itemNewDestido.EXTRA1 = item.EXTRA1;
                itemNewDestido.EXTRA2 = item.EXTRA2;
                itemNewDestido.EXTRA3 = item.EXTRA3;
                itemNewDestido.USUARIO_INS = item.USUARIO_INS;
                itemNewDestido.FECHA_INS = item.FECHA_INS;
                itemNewDestido.USUARIO_UPD = item.USUARIO_UPD;
                itemNewDestido.FECHA_UPD = item.FECHA_UPD;
                itemNewDestido.CODIGO_EMPRESA = item.CODIGO_EMPRESA;
                itemNewDestido.CODIGO_PRESUPUESTO = item.CODIGO_PRESUPUESTO;
                itemNewDestido.EXTRA4 = item.EXTRA4;
                itemNewDestido.BASE_IMPONIBLE = item.BASE_IMPONIBLE;
                newDestido.Add(itemNewDestido);
            }

            return newDestido;
        }

        public List<ADM_CONTACTO_PROVEEDOR> MapProveedorContactos(List<Data.Entities.Adm.ADM_CONTACTO_PROVEEDOR> retencionesOrigen)
        {
                List<ADM_CONTACTO_PROVEEDOR> newDestido = new List<ADM_CONTACTO_PROVEEDOR>();
                foreach (var item in retencionesOrigen)
                {
                    ADM_CONTACTO_PROVEEDOR itemNewDestido = new ADM_CONTACTO_PROVEEDOR();
                    itemNewDestido.CODIGO_CONTACTO_PROVEEDOR = item.CODIGO_CONTACTO_PROVEEDOR;
                    itemNewDestido.CODIGO_PROVEEDOR  = item.CODIGO_PROVEEDOR;
                    itemNewDestido.NOMBRE= item.NOMBRE;
                    itemNewDestido.APELLIDO= item.APELLIDO;
                    itemNewDestido.IDENTIFICACION_ID = item.IDENTIFICACION_ID;
                    itemNewDestido.IDENTIFICACION = item.IDENTIFICACION;
                    itemNewDestido.SEXO = item.SEXO;
                    itemNewDestido.TIPO_CONTACTO_ID  = item.TIPO_CONTACTO_ID;
                    itemNewDestido.PRINCIPAL = item.PRINCIPAL;
                    itemNewDestido.EXTRA1= item.EXTRA1;
                    itemNewDestido.EXTRA2 = item.EXTRA2;
                    itemNewDestido.EXTRA3 = item.EXTRA3;
                    itemNewDestido.USUARIO_INS = item.USUARIO_INS;
                    itemNewDestido.FECHA_INS = item.FECHA_INS;
                    itemNewDestido.USUARIO_UPD = item.USUARIO_UPD;
                    itemNewDestido.FECHA_UPD = item.FECHA_UPD;
                    itemNewDestido.CODIGO_EMPRESA= item.CODIGO_EMPRESA;
                    itemNewDestido.CODIGO_PRESUPUESTO = item.CODIGO_PRESUPUESTO;

                    newDestido.Add(itemNewDestido);
                }
           
                return newDestido;
        }

        
        
        
        public async  Task<ResultDto<bool>> CopiarOrdenPago(int codigoOrdenPago)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            var ordenPagoOrigen = await _repository.GetCodigoOrdenPago(codigoOrdenPago);
            if (ordenPagoOrigen == null)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = "Orden de Pago No existe";
                return result;
            }
            
            await _pucOrdenPagoDestinoRepository.Delete(codigoOrdenPago);
            await _admBeneficiariosOpDestinoRepository.Delete(codigoOrdenPago);
            await _admRetencionesOpDestinoRepository.Delete(codigoOrdenPago);
            await _destinoRepository.Delete(codigoOrdenPago);
            await _admContactosProveedorDestinoRepository.Delete(ordenPagoOrigen.CODIGO_PROVEEDOR);
            await _admProveedoresDestinoRepository.Delete(ordenPagoOrigen.CODIGO_PROVEEDOR);
            await _admDescriptivaDestinoRepository.Delete(ordenPagoOrigen.TIPO_ORDEN_PAGO_ID);
            await _admDescriptivaDestinoRepository.Delete((int)ordenPagoOrigen.TIPO_PAGO_ID);
            await _admDescriptivaDestinoRepository.Delete((int)ordenPagoOrigen.FRECUENCIA_PAGO_ID);


            var descriptiva = await _admDescriptivaRepository.GetByCodigo(ordenPagoOrigen.TIPO_ORDEN_PAGO_ID);
            if (descriptiva != null)
            {
                var newDestido = MapDescriptiva(descriptiva);

                await _admDescriptivaDestinoRepository.Add(newDestido);
            }
            descriptiva = await _admDescriptivaRepository.GetByCodigo((int)ordenPagoOrigen.TIPO_PAGO_ID);
            if (descriptiva != null)
            {
                var newDestido = MapDescriptiva(descriptiva);

                await _admDescriptivaDestinoRepository.Add(newDestido);
            }
            descriptiva = await _admDescriptivaRepository.GetByCodigo((int)ordenPagoOrigen.FRECUENCIA_PAGO_ID);
            if (descriptiva != null)
            {
                var newDestido = MapDescriptiva(descriptiva);

                await _admDescriptivaDestinoRepository.Add(newDestido);
            }
            
            var contactosOrigen = await _admContactosProveedorRepository.GetByProveedor(ordenPagoOrigen.CODIGO_PROVEEDOR);
            if (contactosOrigen != null)
            {   
                var newDestido = MapProveedorContactos(contactosOrigen);

                await _admContactosProveedorDestinoRepository.Add(newDestido);
                
            }
            
            var proveedorOrigen = await _admProveedoresRepository.GetByCodigo(ordenPagoOrigen.CODIGO_PROVEEDOR);
            if (proveedorOrigen != null)
            {   
                var newDestido = MapProveedor(proveedorOrigen);

                await _admProveedoresDestinoRepository.Add(newDestido);
                
            }
            
            var newOrden = Map(ordenPagoOrigen);
            var orderCreated = await _destinoRepository.Add(newOrden);
            if (orderCreated.IsValid == true)
            {

               var pucOrdenPagoOrigen=await  _pucOrdenPagoRepository.GetByOrdenPago(codigoOrdenPago);
               if (pucOrdenPagoOrigen.Count > 0)
               {
              

                   var newDestido = MapPuc(pucOrdenPagoOrigen);

                   await _pucOrdenPagoDestinoRepository.Add(newDestido);

                   
                   var groupedData = pucOrdenPagoOrigen.GroupBy(item => new { item.CODIGO_PRESUPUESTO, item.CODIGO_SALDO });
                   
                   foreach (var item in groupedData)
                   {
                       await _preVSaldoDestinoRepository.Delete(item.Key.CODIGO_PRESUPUESTO, item.Key.CODIGO_SALDO);
                       var saldo = await _preVSaldosRepository.GetByCodigo(item.Key.CODIGO_SALDO);
                       var newSaldo = MapPreVSaldo(saldo);
                       await _preVSaldoDestinoRepository.Add(newSaldo);
                   }
               }

               var beneficiariosOp =await _admBeneficiariosOpRepository.GetByOrdenPago(codigoOrdenPago);
               if (beneficiariosOp.Count > 0)
               {
                   var newBeneficiario = MapBeneficiario(beneficiariosOp);

                   await _admBeneficiariosOpDestinoRepository.Add(newBeneficiario);
               }

               var retencionesOp = await _admRetencionesOpRepository.GetByOrdenPago(codigoOrdenPago);
              
               if (retencionesOp.Count>0)
               {
                   
           
                   var newRetenciones = MapRetenciones(retencionesOp);

                   await _admRetencionesOpDestinoRepository.Add(newRetenciones);
               }
            }
            else
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = orderCreated.Message;
            }

            result.Data = true;
            result.IsValid = true;
            result.Message = "";
            
            return result;
        }
    
   
        
    }
 }

