using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmRetencionesOp
{
    public partial class AdmRetencionesOpService : IAdmRetencionesOpService
    {
        private readonly IAdmRetencionesOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmRetencionesRepository _admRetencionesRepository;
        private readonly ISisSerieDocumentosRepository _serieDocumentosRepository;
        private readonly ISisDescriptivaRepository _sisDescriptivaRepository;
        private readonly IAdmDocumentosOpRepository _admDocumentosOpRepository;
        private readonly IOssConfigRepository _ossConfigRepository;
        private readonly IAdmBeneficariosOpService _admBeneficariosOpService;
        private readonly IAdmCompromisoOpRepository _admCompromisoOpRepository;

        public AdmRetencionesOpService(IAdmRetencionesOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IAdmRetencionesRepository admRetencionesRepository,
                                     ISisSerieDocumentosRepository serieDocumentosRepository,
                                     ISisDescriptivaRepository sisDescriptivaRepository,
                                     IAdmDocumentosOpRepository admDocumentosOpRepository,
                                     IOssConfigRepository ossConfigRepository,
                                     IAdmBeneficariosOpService admBeneficariosOpService,
                                     IAdmCompromisoOpRepository admCompromisoOpRepository
                                     )
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _admRetencionesRepository = admRetencionesRepository;
            _serieDocumentosRepository = serieDocumentosRepository;
            _sisDescriptivaRepository = sisDescriptivaRepository;
            _admDocumentosOpRepository = admDocumentosOpRepository;
            _ossConfigRepository = ossConfigRepository;
            _admBeneficariosOpService = admBeneficariosOpService;
            _admCompromisoOpRepository = admCompromisoOpRepository;
        }

      
       
        
        
    }
    
    
 }

