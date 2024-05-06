
using Convertidor.Data;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository;
using Convertidor.Data.Repository.Adm;
using Convertidor.Data.Repository.Bm;
using Convertidor.Data.Repository.Catastro;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Data.Repository.Rh;
using Convertidor.Data.Repository.Sis;
using Convertidor.Services;
using Convertidor.Services.Adm;
using Convertidor.Services.Bm;
using Convertidor.Services.Catastro;
using Convertidor.Services.Sis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Convertidor.Services.Rh.Report.Example;
using Convertidor.Services.Rh.Report.HistoricoNomina;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();



builder.Services.AddTransient<IRH_HISTORICO_NOMINARepository, RH_HISTORICO_NOMINARepository>();
builder.Services.AddTransient<IHistoricoNominaRepository, HistoricoNominaRepository>();
builder.Services.AddTransient<IRH_HISTORICO_PERSONAL_CARGORepository, RH_HISTORICO_PERSONAL_CARGORepository>();
builder.Services.AddTransient<IHistoricoPersonalCargoRepository, HistoricoPersonalCargoRepository>();

builder.Services.AddTransient<IIndiceCategoriaProgramaRepository, IndiceCategoriaProgramaRepository>();
builder.Services.AddTransient<IConceptosRetencionesRepository, ConceptosRetencionesRepository>();
builder.Services.AddTransient<IHistoricoRetencionesRepository, HistoricoRetencionesRepository>();

//Repository Presupuesto
builder.Services.AddTransient<IPRE_PRESUPUESTOSRepository, PRE_PRESUPUESTOSRepository>();
builder.Services.AddTransient<IPRE_INDICE_CAT_PRGRepository, PRE_INDICE_CAT_PRGRepository>();
builder.Services.AddTransient<IPRE_V_SALDOSRepository, PRE_V_SALDOSRepository>();
builder.Services.AddTransient<IPRE_V_DENOMINACION_PUCRepository, PRE_V_DENOMINACION_PUCRepository>();
builder.Services.AddTransient<IPRE_V_DOC_COMPROMISOSRepository, PRE_V_DOC_COMPROMISOSRepository>();
builder.Services.AddTransient<IPRE_V_DOC_CAUSADORepository, PRE_V_DOC_CAUSADORepository>();
builder.Services.AddTransient<IPRE_V_DOC_PAGADORepository, PRE_V_DOC_PAGADORepository>();
builder.Services.AddTransient<IPRE_V_DOC_BLOQUEADORepository, PRE_V_DOC_BLOQUEADORepository>();
builder.Services.AddTransient<IPRE_V_DOC_MODIFICADORepository, PRE_V_DOC_MODIFICADORepository>();
builder.Services.AddTransient<IPRE_ASIGNACIONESRepository, PRE_ASIGNACIONESRepository>();
builder.Services.AddTransient<IPreCompromisosRepository, PreCompromisosRepository>();
builder.Services.AddTransient<IPreDetalleCompromisosRepository, PreDetalleCompromisosRepository>();
builder.Services.AddTransient<IPrePucCompromisosRepository, PrePucCompromisosRepository>();
builder.Services.AddTransient<IPreSolModificacionRepository, PreSolModificacionRepository>();
builder.Services.AddTransient<IPreModificacionRepository, PreModificacionRepository>();
builder.Services.AddTransient<IPrePucModificacionRepository, PrePucModificacionRepository>();
builder.Services.AddTransient<IPreMetasRepository, PreMetasRepository>();
builder.Services.AddTransient<IPreNivelesPucRepository, PreNivelesPucRepository>();
builder.Services.AddTransient<IPreObjetivosRepository, PreObjetivosRepository>();
builder.Services.AddTransient<IPreOrganismosRepository, PreOrganismosRepository>();
builder.Services.AddTransient<IPreParticipaFinacieraOrgRepository, PreParticipaFinacieraOrgRepository>();
builder.Services.AddTransient<IPrePorcGastosMensualesRepository, PrePorcGastosMensualesRepository>();
builder.Services.AddTransient<IPreProgramasSocialesRepository, PreProgramasSocialesRepository>();
builder.Services.AddTransient<IPreProyectosRepository, PreProyectosRepository>();









//Services Presupuesto
builder.Services.AddTransient<IPRE_PRESUPUESTOSService, PRE_PRESUPUESTOSService>();
builder.Services.AddTransient<IPRE_V_SALDOSServices, PRE_V_SALDOSServices>();
builder.Services.AddTransient<IPRE_V_DENOMINACION_PUCServices, PRE_V_DENOMINACION_PUCServices>();
builder.Services.AddTransient<IPRE_V_MTR_DENOMINACION_PUCRepository, PRE_V_MTR_DENOMINACION_PUCRepository>();
builder.Services.AddTransient<IPRE_V_MTR_DENOMINACION_PUCService, PRE_V_MTR_DENOMINACION_PUCService>();
builder.Services.AddTransient<IPRE_V_MTR_UNIDAD_EJECUTORARepository, PRE_V_MTR_UNIDAD_EJECUTORARepository>();
builder.Services.AddTransient<IPRE_V_MTR_UNIDAD_EJECUTORAService, PRE_V_MTR_UNIDAD_EJECUTORAService>();
builder.Services.AddTransient<IPRE_V_DOC_COMPROMISOSServices, PRE_V_DOC_COMPROMISOSServices>();
builder.Services.AddTransient<IPRE_V_DOC_CAUSADOServices, PRE_V_DOC_CAUSADOServices>();
builder.Services.AddTransient<IPRE_V_DOC_PAGADOServices, PRE_V_DOC_PAGADOServices>();
builder.Services.AddTransient<IPRE_V_DOC_BLOQUEADOServices, PRE_V_DOC_BLOQUEADOServices>();
builder.Services.AddTransient<IPRE_V_DOC_MODIFICADOServices, PRE_V_DOC_MODIFICADOServices>();
builder.Services.AddTransient<IPRE_PLAN_UNICO_CUENTASRepository, PRE_PLAN_UNICO_CUENTASRepository>();
builder.Services.AddTransient<IPreAsignacionesDetalleRepository, PreAsignacionesDetalleRepository>();
builder.Services.AddTransient<IPRE_ASIGNACIONESRepository, PRE_ASIGNACIONESRepository>();
builder.Services.AddTransient<IPreAsignacionesDetalleRepository, PreAsignacionesDetalleRepository>();

builder.Services.AddTransient<IPrePlanUnicoCuentasService, PrePlanUnicoCuentasService>();

builder.Services.AddTransient<IPRE_RELACION_CARGOSRepository, PRE_RELACION_CARGOSRepository>();
builder.Services.AddTransient<IPreDescriptivaRepository, PreDescriptivaRepository>();
builder.Services.AddTransient<IPreDescriptivasService, PreDescriptivasService>();
builder.Services.AddTransient<IPreTitulosRepository, PreTitulosRepository>();
builder.Services.AddTransient<IPreTituloService, PreTituloService>();
builder.Services.AddTransient<IPreCargosRepository, PreCargosRepository>();
builder.Services.AddTransient<IPreCargosService, PreCargosService>();
builder.Services.AddTransient<IPRE_RELACION_CARGOSRepository, PRE_RELACION_CARGOSRepository>();
builder.Services.AddTransient<IPreRelacionCargosService, PreRelacionCargosService>();
builder.Services.AddTransient<IPreAsignacionService, PreAsignacionService>();
builder.Services.AddTransient<IPreAsignacionDetalleService, PreAsignacionDetalleService>();
builder.Services.AddTransient<IPrePucSolicitudModificacionRepository, PrePucSolicitudModificacionRepository>();
builder.Services.AddTransient<IPrePucSolicitudModificacionService, PrePucSolicitudModificacionService>();
builder.Services.AddTransient<IPRE_SALDOSRepository, PRE_SALDOSRepository>();
builder.Services.AddTransient<IPreResumenSaldoRepository, PreResumenSaldoRepository>();


builder.Services.AddTransient<IPreResumenSaldoServices, PreResumenSaldoServices>();
builder.Services.AddTransient<IReportPreResumenSaldoService, ReportPreResumenSaldoService>();
builder.Services.AddTransient<IPreCompromisosService, PreCompromisosService>();
builder.Services.AddTransient<IPreDetalleCompromisosService, PreDetalleCompromisosService>();
builder.Services.AddTransient<IPrePucCompromisosService, PrePucCompromisosService>();
builder.Services.AddTransient<IPreSolModificacionService, PreSolModificacionService>();
builder.Services.AddTransient<IPreModificacionService, PreModificacionService>();
builder.Services.AddTransient<IPrePucModificacionService, PrePucModificacionService>();
builder.Services.AddTransient<IPreMetasService, PreMetasService>();
builder.Services.AddTransient<IPreNivelesPucService, PreNivelesPucService>();
builder.Services.AddTransient<IPreObjetivosService, PreObjetivosService>();
builder.Services.AddTransient<IPreOrganismosService, PreOrganismosService>();
builder.Services.AddTransient<IPreParticipaFinacieraOrgService, PreParticipaFinacieraOrgService>();
builder.Services.AddTransient<IPrePorcGastosMensualesService, PrePorcGastosMensualesService>();
builder.Services.AddTransient<IPreProgramasSocialesService, PreProgramasSocialesService>();
builder.Services.AddTransient<IPreProyectosService, PreProyectosService>();







//Repository SIS

builder.Services.AddTransient<ISisUsuarioRepository, SisUsuarioRepository>();
builder.Services.AddTransient<IOssConfigRepository, OssConfigRepository>();
builder.Services.AddTransient<ISisUbicacionNacionalRepository, SisUbicacionNacionalRepository>();
builder.Services.AddTransient<ISisUbicacionService, SisUbicacionService>();
builder.Services.AddTransient<IUtilityService, UtilityService>();
builder.Services.AddTransient<IOssModuloRepository, OssModuloRepository>();
builder.Services.AddTransient<IOssVariableRepository, OssVariableRepository>();
builder.Services.AddTransient<IOssFuncionRepository, OssFuncionRepository>();
builder.Services.AddTransient<IOssFormulaRepository, OssFormulaRepository>();
builder.Services.AddTransient<IOssModeloCalculoRepository, OssModeloCalculoRepository>();
builder.Services.AddTransient<IOssCalculoRepository,OssCalculoRepository>();
builder.Services.AddTransient<ISisParametrosSourceRepository,SisParametrosSourceRepository>();
builder.Services.AddTransient<ISisDetSourceRepository,SisDetSourceRepository>();
builder.Services.AddTransient<ISisSourceRepository,SisSourceRepository>();



//Services Sis
builder.Services.AddTransient<ISisUsuarioServices, SisUsuarioServices>();
builder.Services.AddTransient<IOssConfigServices, OssServices>();
builder.Services.AddTransient<IEmailServices, EmailServices>();
builder.Services.AddTransient<IOssModuloService, OssModuloService>();
builder.Services.AddTransient<IOssVariableService, OssVariableService>();
builder.Services.AddTransient<IOssFuncionService, OssFuncionService>();
builder.Services.AddTransient<IOssFormulaService, OssFormulaService>();
builder.Services.AddTransient<IOssCalculoService, OssCalculoService>();
builder.Services.AddTransient<IOssModeloCalculoService, OssModeloCalculoService>();







//CATASTRO
builder.Services.AddTransient<ICAT_FICHARepository, CAT_FICHARepository>();

builder.Services.AddTransient<ICAT_FICHAService, CAT_FICHAService>();


//RH
builder.Services.AddTransient<IHistoricoNominaService, HistoricoNominaService>();
builder.Services.AddTransient<IHistoricoPersonalCargoService, HistoricoPersonalCargoService>();
builder.Services.AddTransient<IIndiceCategoriaProgramaService, IndiceCategoriaProgramaService>();
builder.Services.AddTransient<IConceptosRetencionesService, ConceptosRetencionesService>();
builder.Services.AddTransient<IHistoricoRetencionesService, HistoricoRetencionesService>();
builder.Services.AddHttpClient<PetroClientService>();
builder.Services.AddTransient<IPetroClientService, PetroClientService>();

builder.Services.AddTransient<IRhPeriodoService, RhPeriodoService>();
builder.Services.AddTransient<IRhHistoricoMovimientoService, RhHistoricoMovimientoService>();
builder.Services.AddTransient<IRhTmpRetencionesSsoRepository, RhTmpRetencionesSsoRepository>(); 
builder.Services.AddTransient<IRhTmpRetencionesSsoService, RhTmpRetencionesSsoService>();
builder.Services.AddTransient<IRhTmpRetencionesFaovRepository, RhTmpRetencionesFaovRepository>();
builder.Services.AddTransient<IRhTmpRetencionesFaovService, RhTmpRetencionesFaovService>();
builder.Services.AddTransient<IRhTmpRetencionesIncesRepository, RhTmpRetencionesIncesRepository>();
builder.Services.AddTransient<IRhTmpRetencionesIncesService, RhTmpRetencionesIncesService>();
builder.Services.AddTransient<IRhTmpRetencionesFjpRepository, RhTmpRetencionesFjpRepository>();
builder.Services.AddTransient<IRhTmpRetencionesFjpService, RhTmpRetencionesFjpService>();
builder.Services.AddTransient<IRhTmpRetencionesCahRepository, RhTmpRetencionesCahRepository>();
builder.Services.AddTransient<IRhTmpRetencionesCahService, RhTmpRetencionesCahService>();
builder.Services.AddTransient<IRhTmpRetencionesSindRepository, RhTmpRetencionesSindRepository>();
builder.Services.AddTransient<IRhTmpRetencionesSindService, RhTmpRetencionesSindService>();
builder.Services.AddTransient<IRhHRetencionesSsoRepository, RhHRetencionesSsoRepository>();
builder.Services.AddTransient<IRhHRetencionesSsoService, RhHRetencionesSsoService>();
builder.Services.AddTransient<IRhHRetencionesFaovRepository, RhHRetencionesFaovRepository>();
builder.Services.AddTransient<IRhHRetencionesFaovService, RhHRetencionesFaovService>();
builder.Services.AddTransient<IRhHRetencionesFjpRepository, RhHRetencionesFjpRepository>();
builder.Services.AddTransient<IRhHRetencionesFjpService, RhHRetencionesFjpService>();
builder.Services.AddTransient<IRhHRetencionesIncesRepository, RhHRetencionesIncesRepository>();
builder.Services.AddTransient<IRhHRetencionesIncesService, RhHRetencionesIncesService>();
builder.Services.AddTransient<IRhHRetencionesCahRepository, RhHRetencionesCahRepository>();
builder.Services.AddTransient<IRhHRetencionesCahService, RhHRetencionesCahService>();
builder.Services.AddTransient<IRhHRetencionesSindRepository, RhHRetencionesSindRepository>();
builder.Services.AddTransient<IRhHRetencionesSindService, RhHRetencionesSindService>();


builder.Services.AddTransient<IRhTipoNominaRepository, RhTipoNominaRepository>();
builder.Services.AddTransient<IRhPeriodoRepository, RhPeriodoRepository>();
builder.Services.AddTransient<IRhPersonasRepository, RhPersonasRepository>();
builder.Services.AddTransient<IRhPersonaService, RhPersonaService>();
builder.Services.AddTransient<IRhHistoricoMovimientoRepository, RhHistoricoMovimientoRepository>();

builder.Services.AddTransient<IRhTipoNominaService, RhTipoNominaService>();
builder.Services.AddTransient<IRhDescriptivaRepository, RhDescriptivaRepository>();
builder.Services.AddTransient<IRhDescriptivasService, RhDescriptivasService>();

builder.Services.AddTransient<IRhDireccionesRepository, RhDireccionesRepository>();
builder.Services.AddTransient<IRhDireccionesService, RhDireccionesService>();

builder.Services.AddTransient<IRhConceptosRepository, RhConceptosRepository>();
builder.Services.AddTransient<IRhConceptosService, RhConceptosService>();

builder.Services.AddTransient<IRhProcesoRepository, RhProcesoRepository>();
builder.Services.AddTransient<IRhProcesosService, RhProcesosService>();
builder.Services.AddTransient<IRhProcesoDetalleRepository, RhProcesoDetalleRepository>();
builder.Services.AddTransient<IRhRelacionCargosRepository, RhRelacionCargosRepository>();
builder.Services.AddTransient<IRhRelacionCargosService, RhRelacionCargosService>();

builder.Services.AddTransient<IRhMovNominaRepository, RhMovNominaRepository>();
builder.Services.AddTransient<IRhComunicacionesRepository, RhComunicacionesRepository>();
builder.Services.AddTransient<IRhAdministrativosRepository, RhAdministrativosRepository>();
builder.Services.AddTransient<IRhAdministrativosService, RhAdministrativosService>();
builder.Services.AddTransient<IRhComunicacionService, RhComunicacionService>();
builder.Services.AddTransient<IRhFamiliaresRepository, RhFamiliaresRepository>();
builder.Services.AddTransient<IRhFamiliaresService, RhFamiliaresService>();
builder.Services.AddTransient<IRhEducacionRepository, RhEducacionRepository>();
builder.Services.AddTransient<IRhEducacionService, RhEducacionService>();
builder.Services.AddTransient<IRhDocumentosRepository, RhDocumentosRepository>();
builder.Services.AddTransient<IRhDocumentosService, RhDocumentosService>();
builder.Services.AddTransient<IRhDocumentosDetallesRepository, RhDocumentosDetallesRepository>();
builder.Services.AddTransient<IRhDocumentosDetallesService, RhDocumentosDetallesService>();
builder.Services.AddTransient<IRhExpLaboralRepository, RhExpLaboralRepository>();
builder.Services.AddTransient<IRhExpLaboralService, RhExpLaboralService>();
builder.Services.AddTransient<IRhPersonasMovControlRepository, RhPersonasMovControlRepository>();
builder.Services.AddTransient<IRhPersonasMovControlService, RhPersonasMovControlService>();
builder.Services.AddTransient<IRhAriRepository, RhAriRepository>();
builder.Services.AddTransient<IRhAriService, RhAriService>();
builder.Services.AddTransient<IRhHPeriodoRepository, RhHPeriodoRepository>();
builder.Services.AddTransient<IRhHPeriodoService, RhHPeriodoService>();
builder.Services.AddTransient<IRhProcesosDetalleService, RhProcesosDetalleService>();

builder.Services.AddTransient<IRhConceptosAcumulaRepository, RhConceptosAcumulaRepository>();
builder.Services.AddTransient<IRhConceptosAcumuladoService, RhConceptosAcumuladoService>();
builder.Services.AddTransient<IRhConceptosFormulaRepository, RhConceptosFormulaRepository>();
builder.Services.AddTransient<IRhConceptosFormulaService, RhConceptosFormulaService>();
builder.Services.AddTransient<IRhConceptosPUCRepository, RhConceptosPUCRepository>();
builder.Services.AddTransient<IRhConceptosPUCService, RhConceptosPUCService>();
builder.Services.AddTransient<IRhMovNominaService, RhMovNominaService>();

builder.Services.AddTransient<IRhVReciboPagoService, RhVReciboPagoService>();
builder.Services.AddTransient<IRhReporteNominaTemporalRepository, RhReporteNominaTemporalRepository>();
builder.Services.AddTransient<IRhReporteNominaHistoricoRepository, RhReporteNominaHistoricoRepository>();
builder.Services.AddTransient<IRhReporteNominaHistoricoService, RhReporteNominaHistoricoService>();
builder.Services.AddTransient<IRhReporteNominaTemporalService, RhReporteNominaTemporalService>();


builder.Services.AddTransient<IReportExampleService, ReportExampleService>();
builder.Services.AddTransient<IReportHistoricoNominaService, ReportHistoricoNominaService>();
builder.Services.AddTransient<IRhReporteFirmaRepository, RhReporteFirmaRepository>();
builder.Services.AddTransient<IRhReporteFirmaService, RhReporteFirmaService>();
builder.Services.AddTransient<IReportReciboPagoService, ReportReciboPagoService>();




//BM Repository
builder.Services.AddTransient<IBM_V_BM1Repository, BM_V_BM1Repository>();
builder.Services.AddTransient<IBmTitulosRepository, BmTitulosRepository>();
builder.Services.AddTransient<IBmDescriptivaRepository, BmDescriptivaRepository>();
builder.Services.AddTransient<IBmArticulosRepository, BmArticulosRepository>();
builder.Services.AddTransient<IBmBienesRepository, BmBienesRepository>();
builder.Services.AddTransient<IBmClasificacionBienesRepository, BmClasificacionBienesRepository>();
builder.Services.AddTransient<IBmDetalleBienesRepository, BmDetalleBienesRepository>();
builder.Services.AddTransient<IBmDetalleArticulosRepository, BmDetalleArticulosRepository>();
builder.Services.AddTransient<IBmDirBienRepository, BmDirBienRepository>();
builder.Services.AddTransient<IBmDirHBienRepository, BmDirHBienRepository>();
builder.Services.AddTransient<IBmMovBienesRepository, BmMovBienesRepository>();
builder.Services.AddTransient<IBmSolMovBienesRepository, BmSolMovBienesRepository>();
builder.Services.AddTransient<IBmBienesFotoRepository, BmBienesFotoRepository>();
builder.Services.AddTransient<IBmConteoDetalleRepository, BmConteoDetalleRepository>();
builder.Services.AddTransient<IBmConteoRepository, BmConteoRepository>();
builder.Services.AddTransient<IBmConteoDetalleHistoricoRepository, BmConteoDetalleHistoricoRepository>();
builder.Services.AddTransient<IBmConteoHistoricoRepository, BmConteoHistoricoRepository>();
builder.Services.AddTransient<IRhVReciboPagoRepository, RhVReciboPagoRepository>();




//BM Services
builder.Services.AddTransient<IBM_V_BM1Service, BM_V_BM1Service>();
builder.Services.AddTransient<IBmTituloService, BmTitulosService>();
builder.Services.AddTransient<IBmDescriptivasService, BmDescriptivasService>();
builder.Services.AddTransient<IBmArticulosService, BmArticulosService>();
builder.Services.AddTransient<IBmBienesService, BmBienesService>();
builder.Services.AddTransient<IBmClasificacionBienesService, BmClasificacionBienesService>();
builder.Services.AddTransient<IBmDetalleBienesService, BmDetalleBienesService>();
builder.Services.AddTransient<IBmDetalleArticulosService, BmDetalleArticulosService>();
builder.Services.AddTransient<IBmDirBienService, BmDirBienService>();
builder.Services.AddTransient<IBmDirHBienService, BmDirHBienService>();
builder.Services.AddTransient<IBmMovBienesService, BmMovBienesService>();
builder.Services.AddTransient<IBmSolMovBienesService, BmSolMovBienesService>();
builder.Services.AddTransient<IBmBienesFotoService, BmBienesFotoService>();
builder.Services.AddTransient<IBmConteoService, BmConteoService>();
builder.Services.AddTransient<IBmConteoDetalleService, BmConteoDetalleService>();
builder.Services.AddTransient<IBmConteoHistoricoService, BmConteoHistoricoService>();
builder.Services.AddTransient<IBmConteoDetalleHistoricoService, BmConteoDetalleHistoricoService>();



//ADM Repository
builder.Services.AddTransient<IAdmDescriptivaRepository, AdmDescriptivaRepository>();
builder.Services.AddTransient<IAdmTitulosRepository, AdmTitulosRepository>();
builder.Services.AddTransient<IAdmProveedoresRepository, AdmProveedoresRepository>();
builder.Services.AddTransient<IAdmComunicacionProveedorRepository, AdmComunicacionProveedorRepository>();
builder.Services.AddTransient<IAdmContactosProveedorRepository, AdmContactosProveedorRepository>();
builder.Services.AddTransient<IAdmDireccionProveedorRepository, AdmDireccionProveedorRepository>();
builder.Services.AddTransient<IAdmActividadProveedorRepository, AdmActividadProveedorRepository>();
builder.Services.AddTransient<IAdmComunicacionProveedorRepository, AdmComunicacionProveedorRepository>();
builder.Services.AddTransient<IAdmSolicitudesRepository, AdmSolicitudesRepository>();
builder.Services.AddTransient<IAdmDetalleSolicitudRepository, AdmDetalleSolicitudRepository>();
builder.Services.AddTransient<IAdmPucSolicitudRepository, AdmPucSolicitudRepository>();
builder.Services.AddTransient<IAdmReintegrosRepository, AdmReintegrosRepository>();
builder.Services.AddTransient<IAdmPucReintegroRepository, AdmPucReintegroRepository>();
builder.Services.AddTransient<IAdmOrdenPagoRepository, AdmOrdenPagoRepository>();
builder.Services.AddTransient<IAdmCompromisoOpRepository, AdmCompromisoOpRepository>();
builder.Services.AddTransient<IAdmPucOrdenPagoRepository, AdmPucOrdenPagoRepository>();
builder.Services.AddTransient<IAdmRetencionesOpRepository, AdmRetencionesOpRepository>();
builder.Services.AddTransient<IAdmDocumentosOpRepository, AdmDocumentosOpRepository>();
builder.Services.AddTransient<IAdmComprobantesDocumentosOpRepository, AdmComprobantesDocumentosOpRepository>();
builder.Services.AddTransient<IAdmImpuestosDocumentosOpRepository, AdmImpuestosDocumentosOpRepository>();
builder.Services.AddTransient<IAdmImpuestosOpRepository, AdmImpuestosOpRepository>();
builder.Services.AddTransient<IAdmBeneficiariosOpRepository, AdmBeneficiariosOpRepository>();
builder.Services.AddTransient<IAdmPeriodicoOpRepository, AdmPeriodicoOpRepository>();
builder.Services.AddTransient<IAdmHOrdenPagoRepository, AdmHOrdenPagoRepository>();
builder.Services.AddTransient<IAdmContratosRepository, AdmContratosRepository>();
builder.Services.AddTransient<IAdmValContratoRepository, AdmValContratoRepository>();
builder.Services.AddTransient<IAdmDetalleValContratoRepository, AdmDetalleValContratoRepository>();
builder.Services.AddTransient<IAdmPucContratoRepository, AdmPucContratoRepository>();
builder.Services.AddTransient<IAdmChequesRepository, AdmChequesRepository>();








//ADM Services
builder.Services.AddTransient<IAdmTituloService, AdmTituloService>();
builder.Services.AddTransient<IAdmDescriptivasService, AdmDescriptivasService>();
builder.Services.AddTransient<IAdmProveedoresService, AdmProveedoresService>();
builder.Services.AddTransient<IAdmProveedoresActividadService, AdmProveedoresActividadService>();
builder.Services.AddTransient<IAdmProveedoresComunicacionService, AdmProveedoresComunicacionService>();
builder.Services.AddTransient<IAdmProveedoresContactoService, AdmProveedoresContactosService>();
builder.Services.AddTransient<IAdmProveedoresDireccionesService, AdmProveedoresDireccionesService>();
builder.Services.AddTransient<IAdmSolicitudesService, AdmSolicitudesService>();
builder.Services.AddTransient<IAdmDetalleSolicitudService, AdmDetalleSolicitudService>();
builder.Services.AddTransient<IAdmPucSolicitudService, AdmPucSolicitudService>();
builder.Services.AddTransient<IAdmReintegrosService, AdmReintegrosService>();
builder.Services.AddTransient<IAdmPucReintegroService, AdmPucReintegroService>();
builder.Services.AddTransient<IAdmOrdenPagoService, AdmOrdenPagoService>();
builder.Services.AddTransient<IAdmCompromisoOpService, AdmCompromisoOpService>();
builder.Services.AddTransient<IAdmPucOrdenPagoService, AdmPucOrdenPagoService>();
builder.Services.AddTransient<IAdmRetencionesOpService, AdmRetencionesOpService>();
builder.Services.AddTransient<IAdmDocumentosOpService, AdmDocumentosOpService>();
builder.Services.AddTransient<IAdmComprobantesDocumentosOpService, AdmComprobantesDocumentosOpService>();
builder.Services.AddTransient<IAdmImpuestosDocumentosOpService, AdmImpuestosDocumentosOpService>();
builder.Services.AddTransient<IAdmImpuestosOpService, AdmImpuestosOpService>();
builder.Services.AddTransient<IAdmBeneficariosOpService, AdmBeneficiariosOpService>();
builder.Services.AddTransient<IAdmPeriodicoOpService, AdmPeriodicoOpService>();
builder.Services.AddTransient<IAdmHOrdenPagoService, AdmHOrdenPagoService>();
builder.Services.AddTransient<IAdmContratosService, AdmContratosService>();
builder.Services.AddTransient<IAdmValContratoService, AdmValContratoService>();
builder.Services.AddTransient<IAdmPucContratoService, AdmPucContratoService>();
builder.Services.AddTransient<IAdmChequesService, AdmChequesService>();












// Register AutoMapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<SeedDb>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>(); 
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionRH");
builder.Services.AddDbContext<DataContext>(options =>
      options.UseOracle(connectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var preConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionPRE");
builder.Services.AddDbContext<DataContextPre>(options =>
      options.UseOracle(preConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));



var rmConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionRM");
builder.Services.AddDbContext<DataContextRm>(options =>
      options.UseOracle(rmConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var catConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionCAT");
builder.Services.AddDbContext<DataContextCat>(options =>
      options.UseOracle(catConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var sisConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionSIS");
builder.Services.AddDbContext<DataContextSis>(options =>
      options.UseOracle(sisConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var bmConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionBM");
builder.Services.AddDbContext<DataContextBm>(options =>
    options.UseOracle(bmConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var admConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionADM");
builder.Services.AddDbContext<DataContextAdm>(options =>
    options.UseOracle(admConnectionString, b => b.UseOracleSQLCompatibility("11")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));



var destinoConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionPostgres");
builder.Services.AddDbContext<DestinoDataContext>(options =>
      options.UseNpgsql(destinoConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

/*var redisConnectionString = builder.Configuration.GetConnectionString("redisConnection");
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect(redisConnectionString));*/

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey=true,
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value )),
            ValidateIssuer=false,
            ValidateAudience=false
        };

    });

var app = builder.Build();

QuestPDF.Settings.License = LicenseType.Community;
//SeedData(app);

void SeedData(WebApplication app)
{
    IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();
    using (IServiceScope? scope = scopeFactory!.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
        service!.GetAll().Wait();

    }
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<DestinoDataContext>();
//    db.Database.Migrate();
//}


//dotnet ef migrations add InitialCreate --context DestinoDataContext --output-dir Migrations/DestinoDataContext

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
//Server de desarrollo
//app.Urls.Add("http://216.244.81.116:5000");
//app.Urls.Add("http://localhost:5000");
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("corspolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

