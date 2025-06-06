﻿
using Convertidor.Data.DestinoInterfaces.ADM;
using Convertidor.Data.DestinoInterfaces.PRE;
using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.EntitiesDestino.PRE;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;
using ADM_DOCUMENTOS_OP = Convertidor.Data.Entities.Adm.ADM_DOCUMENTOS_OP;


namespace Convertidor.Services.Destino.ADM
{
    public class AdmOrdenPagoDestinoService : IAdmOrdenPagoDestinoService
    {
        private readonly IMapper _mapper;
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
        private readonly IAdmCompromisoOpRepository _admCompromisoOpRepository;
        private readonly IPreCompromisosRepository _preCompromisosRepository;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;
        private readonly IOssConfigRepository _ossConfigRepository;
        private readonly IAdmComprobantesDocumentosOpDestinoRepository _admComprobantesDocumentosOpDestinoRepository;
        private readonly IAdmComprobantesDocumentosOpRepository _admComprobantesDocumentosOpRepository;
        private readonly ISisEmpresaRepository _sisEmpresaRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ISisDescriptivaRepository _sisDescriptivaRepository;
        private readonly IAdmDocumentosOpDestinoRepository _admDocumentosOpDestinoRepository;
        private readonly IAdmDocumentosOpRepository _admDocumentosOpRepository;
        private readonly IAdmImpuestosDocumentosOpRepository _admImpuestosDocumentosOpRepository;
        private readonly IAdmImpuestoDocumentosOpDestinoRepository _admImpuestoDocumentosOpDestinoRepository;


        public AdmOrdenPagoDestinoService(
                        IMapper mapper,
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
                        IPRE_V_SALDOSRepository preVSaldosRepository,
                        IAdmCompromisoOpRepository admCompromisoOpRepository,
                        IPreCompromisosRepository preCompromisosRepository,
                        IAdmSolicitudesRepository admSolicitudesRepository,
                        IOssConfigRepository ossConfigRepository,
                        IAdmComprobantesDocumentosOpDestinoRepository admComprobantesDocumentosOpDestinoRepository,
                        IAdmComprobantesDocumentosOpRepository admComprobantesDocumentosOpRepository,
                        ISisEmpresaRepository sisEmpresaRepository,
                        ISisUsuarioRepository sisUsuarioRepository,
                        ISisDescriptivaRepository sisDescriptivaRepository,
                        IAdmDocumentosOpDestinoRepository admDocumentosOpDestinoRepository,
                        IAdmDocumentosOpRepository admDocumentosOpRepository,
                        IAdmImpuestosDocumentosOpRepository admImpuestosDocumentosOpRepository,
                        IAdmImpuestoDocumentosOpDestinoRepository admImpuestoDocumentosOpDestinoRepository
                        )
        {
            _mapper = mapper;
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
            _admCompromisoOpRepository = admCompromisoOpRepository;
            _preCompromisosRepository = preCompromisosRepository;
            _admSolicitudesRepository = admSolicitudesRepository;
            _ossConfigRepository = ossConfigRepository;
            _admComprobantesDocumentosOpDestinoRepository = admComprobantesDocumentosOpDestinoRepository;
            _admComprobantesDocumentosOpRepository = admComprobantesDocumentosOpRepository;
            _sisEmpresaRepository = sisEmpresaRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _sisDescriptivaRepository = sisDescriptivaRepository;
            _admDocumentosOpDestinoRepository = admDocumentosOpDestinoRepository;
            _admDocumentosOpRepository = admDocumentosOpRepository;
            _admImpuestosDocumentosOpRepository = admImpuestosDocumentosOpRepository;
            _admImpuestoDocumentosOpDestinoRepository = admImpuestoDocumentosOpDestinoRepository;
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
            newProveedor.CEDULA = (int)proveedorOrigen.CEDULA;
            newProveedor.RIF  = proveedorOrigen.RIF;
            newProveedor.FECHA_RIF  = (DateTime)proveedorOrigen.FECHA_RIF;
            newProveedor.NIT = proveedorOrigen.NIT;
            newProveedor.FECHA_NIT = (DateTime)proveedorOrigen.FECHA_NIT;
            newProveedor.NUMERO_REGISTRO_CONTRALORIA  = proveedorOrigen.NUMERO_REGISTRO_CONTRALORIA;
            newProveedor.FECHA_REGISTRO_CONTRALORIA = (DateTime)proveedorOrigen.FECHA_REGISTRO_CONTRALORIA;
            newProveedor.CAPITAL_PAGADO  = (decimal)proveedorOrigen.CAPITAL_PAGADO;
            newProveedor.CAPITAL_SUSCRITO  = (decimal)proveedorOrigen.CAPITAL_SUSCRITO;
            newProveedor.TIPO_IMPUESTO_ID = (int)proveedorOrigen.TIPO_IMPUESTO_ID;
            newProveedor.STATUS  = proveedorOrigen.STATUS;
            newProveedor.CODIGO_PERSONA = (int)proveedorOrigen.CODIGO_PERSONA;
            newProveedor.CODIGO_AUXILIAR_GASTO_X_PAGAR  =(int) proveedorOrigen.CODIGO_AUXILIAR_GASTO_X_PAGAR;
            newProveedor.CODIGO_AUXILIAR_ORDEN_PAGO  = (int)proveedorOrigen.CODIGO_AUXILIAR_ORDEN_PAGO;
            newProveedor.ESTATUS_FISCO_ID = (int)proveedorOrigen.ESTATUS_FISCO_ID;
            newProveedor.NUMERO_CUENTA = proveedorOrigen.NUMERO_CUENTA;
            newProveedor.EXTRA1 = proveedorOrigen.EXTRA1;
            newProveedor.EXTRA2 = proveedorOrigen.EXTRA2;
            newProveedor.EXTRA3 = proveedorOrigen.EXTRA3;
            newProveedor.USUARIO_INS = (int)proveedorOrigen.USUARIO_INS;
            newProveedor.FECHA_INS =(DateTime) proveedorOrigen.FECHA_INS;
            newProveedor.USUARIO_UPD = (int)proveedorOrigen.USUARIO_UPD;
            newProveedor.FECHA_UPD = (DateTime)proveedorOrigen.FECHA_UPD;
            newProveedor.CODIGO_EMPRESA =(int) proveedorOrigen.CODIGO_EMPRESA;
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
                itemNewDestido.CODIGO_CONTACTO_PROVEEDOR = (int)item.CODIGO_CONTACTO_PROVEEDOR;
                itemNewDestido.MONTO = item.MONTO;
                itemNewDestido.MONTO_PAGADO = item.MONTO_PAGADO;
                itemNewDestido.MONTO_ANULADO = item.MONTO_ANULADO;
                itemNewDestido.EXTRA1 = item.EXTRA1;
                itemNewDestido.EXTRA2 = item.EXTRA2;
                itemNewDestido.EXTRA3 = item.EXTRA3;
                itemNewDestido.USUARIO_INS = (int)item.USUARIO_INS;
                itemNewDestido.FECHA_INS = (DateTime)item.FECHA_INS;
                itemNewDestido.USUARIO_UPD = (int)item.USUARIO_UPD;
                itemNewDestido.FECHA_UPD = (DateTime)item.FECHA_UPD;
                itemNewDestido.CODIGO_EMPRESA = (int)item.CODIGO_EMPRESA;
                itemNewDestido.CODIGO_PRESUPUESTO = (int)item.CODIGO_PRESUPUESTO;
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

        public async Task<string> LimpiaTablasOrdenPago(int codigoOrdenPago,Data.Entities.Adm.ADM_ORDEN_PAGO ordenPagoOrigen)
        {
            try
            {
                await _pucOrdenPagoDestinoRepository.Delete(codigoOrdenPago);                              
                await _admBeneficiariosOpDestinoRepository.Delete(codigoOrdenPago);                        
                await _admRetencionesOpDestinoRepository.Delete(codigoOrdenPago);
                await _admComprobantesDocumentosOpDestinoRepository.Delete(codigoOrdenPago);
                await _destinoRepository.Delete(codigoOrdenPago);                                          
                await _admContactosProveedorDestinoRepository.Delete(ordenPagoOrigen.CODIGO_PROVEEDOR);    
                await _admProveedoresDestinoRepository.Delete(ordenPagoOrigen.CODIGO_PROVEEDOR);           
                await _admDescriptivaDestinoRepository.Delete(ordenPagoOrigen.TIPO_ORDEN_PAGO_ID);         
                await _admDescriptivaDestinoRepository.Delete((int)ordenPagoOrigen.TIPO_PAGO_ID);          
                await _admDescriptivaDestinoRepository.Delete((int)ordenPagoOrigen.FRECUENCIA_PAGO_ID);
                await _admDocumentosOpDestinoRepository.DeleteByOrdenPago(codigoOrdenPago);
          
                
                return "";
            }
            catch (Exception e)
            {
                  return e.Message;  
               
            }
           
            
        }

        public async Task<string> GetTituloReporteOrdenPago(int codigoOrdenPago,Data.Entities.Adm.ADM_ORDEN_PAGO ordenPago)
        {

            string tituLo = "ORDEN DE PAGO";
            
          
            
            var compromisoOp = await _admCompromisoOpRepository.GetCodigoOrdenPago(codigoOrdenPago,ordenPago.CODIGO_PRESUPUESTO);
            if (compromisoOp.Count > 0)
            {
                var compromisoItem = compromisoOp.Where(x=>x.CODIGO_ORDEN_PAGO==codigoOrdenPago).FirstOrDefault();
                if (compromisoItem != null)
                {
                    var preCompromiso =
                        await _preCompromisosRepository.GetByCodigo(compromisoItem.CODIGO_IDENTIFICADOR);
                    if (preCompromiso != null)
                    {
                        var solicitud =
                            await _admSolicitudesRepository.GetByCodigoSolicitud(preCompromiso.CODIGO_SOLICITUD);
                        if(solicitud!=null)
                        { 
                            var ossConfig = await _ossConfigRepository.GetByClave($"TITULO_ORDEN_PAGO_{solicitud.TIPO_SOLICITUD_ID}");
                           if (ossConfig != null)
                           {
                               tituLo =ossConfig.VALOR;
                           }
                        }
                    }
                }
            }

            return tituLo;


        }

        public async Task<string> GetNumeroCompromisoByOrdenPago(int codigoOrdenPago,int presupuesto)
        {
            var result = "";
            var compromisosOp =await _admCompromisoOpRepository.GetCodigoOrdenPago(codigoOrdenPago,presupuesto);
            if (compromisosOp.Count > 0)
            {
                var firstCompromiso = compromisosOp.FirstOrDefault();
                var preCompromiso = await _preCompromisosRepository.GetByCodigo(firstCompromiso.CODIGO_IDENTIFICADOR);
                if (preCompromiso != null)
                {
                    result = preCompromiso.NUMERO_COMPROMISO;
                }
            }
      
            
            return result;
        }   
        
        public async Task<DateTime> GetFechaCompromisoByOrdenPago(int codigoOrdenPago,int presupuesto)
        {
            DateTime result=DateTime.Now;
            var compromisosOp =await _admCompromisoOpRepository.GetCodigoOrdenPago(codigoOrdenPago,presupuesto);
            if (compromisosOp.Count > 0)
            {
                var firstCompromiso = compromisosOp.FirstOrDefault();
                var preCompromiso = await _preCompromisosRepository.GetByCodigo(firstCompromiso.CODIGO_IDENTIFICADOR);
                if (preCompromiso != null)
                {
                    result = preCompromiso.FECHA_COMPROMISO;
                }
            }
      
            
            return result;
        }   
        
        public async  Task<ResultDto<bool>> CopiarOrdenPago(int codigoOrdenPago)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            var conectado = await _sisUsuarioRepository.GetConectado();
            
            decimal monto = 0;
            var pucOrdenPagoOrigen = await _pucOrdenPagoRepository.GetByOrdenPago(codigoOrdenPago);
            if (pucOrdenPagoOrigen.Count > 0)
            {
                monto = pucOrdenPagoOrigen.Sum(x => x.MONTO);
            }

           
            await _repository.UpdateMontoEnLetras(codigoOrdenPago, monto);

          
            
            var ordenPagoOrigen = await _repository.GetCodigoOrdenPago(codigoOrdenPago);
            if (ordenPagoOrigen == null)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = "Orden de Pago No existe";
                return result;
            }
            
            
            var empresa = await _sisEmpresaRepository.GetByCodigo(conectado.Empresa);
            if (empresa != null)
            {
                AdmAgenteRetencionDto agenteRetencion = new AdmAgenteRetencionDto();
                agenteRetencion.NOMBRE_AGENTE_RETENCION = empresa.NOMBRE_EMPRESA;
                agenteRetencion.DIRECCION_AGENTE_RETENCION = empresa.EXTRA4;
                agenteRetencion.TELEFONO_AGENTE_RETENCION = empresa.EXTRA6;
                var sisDescriptiva = await _sisDescriptivaRepository.GetById(empresa.IDENTIFICACION_ID);
                agenteRetencion.RIF_AGENTE_RETENCION = "";
                if (sisDescriptiva != null)
                {
                    agenteRetencion.RIF_AGENTE_RETENCION = $"{sisDescriptiva.CODIGO_DESCRIPCION}{empresa.NUMERO_IDENTIFICACION}";
                }

                ordenPagoOrigen.NOMBRE_AGENTE_RETENCION = agenteRetencion.NOMBRE_AGENTE_RETENCION;
                ordenPagoOrigen.DIRECCION_AGENTE_RETENCION = agenteRetencion.DIRECCION_AGENTE_RETENCION;
                ordenPagoOrigen.TELEFONO_AGENTE_RETENCION = agenteRetencion.TELEFONO_AGENTE_RETENCION;
                ordenPagoOrigen.RIF_AGENTE_RETENCION = agenteRetencion.RIF_AGENTE_RETENCION;
                
            }
            
            var tituloReporteOrden = await GetTituloReporteOrdenPago(codigoOrdenPago,ordenPagoOrigen);
            ordenPagoOrigen.TITULO_REPORTE = tituloReporteOrden;
            
            await _repository.Update(ordenPagoOrigen);
            

           var resultDeleted = await  LimpiaTablasOrdenPago(codigoOrdenPago,ordenPagoOrigen);
           if (resultDeleted != "")
           {
               result.Data = false;                       
               result.IsValid = false;                    
               result.Message = $"Error eliminando datos de Orden de Pago: {resultDeleted}";
               return result;                             
           }
            
            var descriptiva = await _admDescriptivaRepository.GetByCodigo(ordenPagoOrigen.TIPO_ORDEN_PAGO_ID);
            if (descriptiva != null)
            {
                //var newDestido = MapDescriptiva(descriptiva);
                var newDestido = _mapper.Map<ADM_DESCRIPTIVAS>(descriptiva);
                await _admDescriptivaDestinoRepository.Add(newDestido);
            }
            descriptiva = await _admDescriptivaRepository.GetByCodigo((int)ordenPagoOrigen.TIPO_PAGO_ID);
            if (descriptiva != null)
            {
                //var newDestido = MapDescriptiva(descriptiva);
                var newDestido = _mapper.Map<ADM_DESCRIPTIVAS>(descriptiva);

                await _admDescriptivaDestinoRepository.Add(newDestido);
            }
            descriptiva = await _admDescriptivaRepository.GetByCodigo((int)ordenPagoOrigen.FRECUENCIA_PAGO_ID);
            if (descriptiva != null)
            {
                //var newDestido = MapDescriptiva(descriptiva);
                var newDestido = _mapper.Map<ADM_DESCRIPTIVAS>(descriptiva);
                await _admDescriptivaDestinoRepository.Add(newDestido);
            }
            
            var contactosOrigen = await _admContactosProveedorRepository.GetByProveedor(ordenPagoOrigen.CODIGO_PROVEEDOR);
            if (contactosOrigen != null)
            {   
                //var newDestido = MapProveedorContactos(contactosOrigen);
                var newDestido = _mapper.Map<List<ADM_CONTACTO_PROVEEDOR>>(contactosOrigen);
                await _admContactosProveedorDestinoRepository.Add(newDestido);
                
            }
            
            var proveedorOrigen = await _admProveedoresRepository.GetByCodigo(ordenPagoOrigen.CODIGO_PROVEEDOR);
            if (proveedorOrigen is not null)
            {   
                //var newDestido = MapProveedor(proveedorOrigen);
                var newDestido = _mapper.Map<ADM_PROVEEDORES>(proveedorOrigen);
                await _admProveedoresDestinoRepository.Add(newDestido);
                
            }
            
            
            
            //var newOrden = Map(ordenPagoOrigen);
            var newOrden = _mapper.Map<ADM_ORDEN_PAGO>(ordenPagoOrigen);
            newOrden.NUMERO_COMPROMISO = await GetNumeroCompromisoByOrdenPago(newOrden.CODIGO_ORDEN_PAGO,newOrden.CODIGO_PRESUPUESTO);
            newOrden.FECHA_COMPROMISO =await GetFechaCompromisoByOrdenPago(newOrden.CODIGO_ORDEN_PAGO, newOrden.CODIGO_PRESUPUESTO);
            var orderCreated = await _destinoRepository.Add(newOrden);
            if (orderCreated.IsValid == true)
            {

              
               if (pucOrdenPagoOrigen.Count > 0)
               {
              

                   //var newDestido = MapPuc(pucOrdenPagoOrigen);
                   var newDestido = _mapper.Map<List<ADM_PUC_ORDEN_PAGO>>(pucOrdenPagoOrigen);
                   await _pucOrdenPagoDestinoRepository.Add(newDestido);

                   
                   var groupedData = pucOrdenPagoOrigen.GroupBy(item => new { item.CODIGO_PRESUPUESTO, item.CODIGO_SALDO });
                   
                   foreach (var item in groupedData)
                   {
                       await _preVSaldoDestinoRepository.Delete(item.Key.CODIGO_PRESUPUESTO, item.Key.CODIGO_SALDO);
                       var saldo = await _preVSaldosRepository.GetByCodigo(item.Key.CODIGO_SALDO);
                    //   var newSaldo = MapPreVSaldo(saldo);
                       var newSaldo = _mapper.Map<PRE_V_SALDOS>(saldo);
                       await _preVSaldoDestinoRepository.Add(newSaldo);
                   }
               }

               var beneficiariosOp =await _admBeneficiariosOpRepository.GetByOrdenPago(codigoOrdenPago);
               if (beneficiariosOp.Count > 0)
               {
                  // var newBeneficiario = MapBeneficiario(beneficiariosOp);
                   var newBeneficiario = _mapper.Map<List<ADM_BENEFICIARIOS_OP>>(beneficiariosOp);
                   await _admBeneficiariosOpDestinoRepository.Add(newBeneficiario);
               }

               var retencionesOp = await _admRetencionesOpRepository.GetByOrdenPago(codigoOrdenPago);
              
               if (retencionesOp.Count>0)
               {
                   //var newRetenciones = MapRetenciones(retencionesOp);
                   var newRetenciones = _mapper.Map<List<ADM_RETENCIONES_OP>>(retencionesOp);
                   await _admRetencionesOpDestinoRepository.Add(newRetenciones);
               }


               var comprobantesDocumentos = await _admComprobantesDocumentosOpRepository.GetByOrdenPago(codigoOrdenPago);
               if (comprobantesDocumentos.Count>0)
               {
                   var newcomprobantesDocumentos = _mapper.Map<List<ADM_COMPROBANTES_DOCUMENTOS_OP>>(retencionesOp);
                   await _admComprobantesDocumentosOpDestinoRepository.Add(newcomprobantesDocumentos);
               }

               //REPLICA INFORMACION DE LOS DOCUMENTOS DE UNA ORDEN DE PAGO
               var documentosOpOrigen = await _admDocumentosOpRepository.GetByCodigoOrdenPago(codigoOrdenPago);
               if (documentosOpOrigen.Count > 0)
               {
                   foreach (var item in documentosOpOrigen)
                   {
                       var newDocumentos = _mapper.Map<Convertidor.Data.EntitiesDestino.ADM.ADM_DOCUMENTOS_OP>(item);
                       await _admDocumentosOpDestinoRepository.Add(newDocumentos);


                       var impuestosOrigen =
                          await _admImpuestosDocumentosOpRepository.GetByDocumento(item.CODIGO_DOCUMENTO_OP);
                       if (impuestosOrigen.Count > 0)
                       {
                           foreach (var itemImpuesto in impuestosOrigen)
                           {

                               await _admImpuestoDocumentosOpDestinoRepository.DeleteByDocumento(itemImpuesto
                                   .CODIGO_DOCUMENTO_OP);
                               var newImpuestoDocumentos = _mapper.Map<Convertidor.Data.EntitiesDestino.ADM.ADM_IMPUESTOS_DOCUMENTOS_OP>(itemImpuesto);
                               await _admImpuestoDocumentosOpDestinoRepository.Add(newImpuestoDocumentos);
                           }
                       }

                   }
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

