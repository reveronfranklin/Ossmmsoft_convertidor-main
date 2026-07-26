using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm.AdmRetencionesOp;
using Convertidor.Utility;
using Microsoft.Extensions.Logging;

namespace Convertidor.Services.Adm.AdmOrdenPago
{
    public partial class AdmOrdenPagoService : IAdmOrdenPagoService
    {
        public IPRE_V_SALDOSRepository PreSaldosRepository { get; }
        private readonly IAdmOrdenPagoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ISisEmpresaRepository _sisEmpresaRepository;
        private readonly ISisDescriptivaRepository _sisDescriptivaRepository;
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
        private readonly IAdmDocumentosOpRepository _admDocumentosOpRepository;
        private readonly IAdmBeneficiariosOpRepository _admBeneficiariosOpRepository;
        private readonly IAdmCompromisosPendientesRepository _admCompromisosPendientesRepository;
        private readonly IAdmRetencionesOpService _admRetencionesOpService;
        private readonly ILogger<AdmOrdenPagoService> _logger;

        public AdmOrdenPagoService(IAdmOrdenPagoRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     ISisEmpresaRepository sisEmpresaRepository,
                                     ISisDescriptivaRepository sisDescriptivaRepository,
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
                                     IAdmPucOrdenPagoRepository admPucOrdenPagoRepository,
                                     IAdmDocumentosOpRepository admDocumentosOpRepository,
                                     IAdmBeneficiariosOpRepository admBeneficiariosOpRepository,
                                     IAdmCompromisosPendientesRepository  admCompromisosPendientesRepository,
                                     IAdmRetencionesOpService admRetencionesOpServices,
                                     ILogger<AdmOrdenPagoService> logger)
        {

            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _sisEmpresaRepository = sisEmpresaRepository;
            _sisDescriptivaRepository = sisDescriptivaRepository;
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
            _admDocumentosOpRepository = admDocumentosOpRepository;
            _admBeneficiariosOpRepository = admBeneficiariosOpRepository;
            _admCompromisosPendientesRepository = admCompromisosPendientesRepository;
            _admRetencionesOpService = admRetencionesOpServices;
            _logger = logger;
        }
        

  
    }
 }
