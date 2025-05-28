using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Services.Adm.AdmRetencionesOp;
using Convertidor.Utility;

namespace Convertidor.Services.Adm.AdmDocumentosOp
{
    public partial class AdmDocumentosOpService : IAdmDocumentosOpService
    {
        private readonly IAdmDocumentosOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmRetencionesOpService _admRetencionesOpService;
        private readonly IAdmRetencionesRepository _admRetencionesRepository;
        private readonly IAdmRetencionesOpRepository _admRetencionesOpRepository;
        private readonly IAdmPucOrdenPagoRepository _admPucOrdenPagoRepository;
        private readonly IAdmImpuestosDocumentosOpRepository _admImpuestosDocumentosOpRepository;

        public AdmDocumentosOpService(IAdmDocumentosOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IAdmRetencionesOpService admRetencionesOpService,
                                     IAdmRetencionesRepository admRetencionesRepository,
                                     IAdmRetencionesOpRepository admRetencionesOpRepository,
                                     IAdmPucOrdenPagoRepository admPucOrdenPagoRepository,
                                     IAdmImpuestosDocumentosOpRepository admImpuestosDocumentosOpRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _admRetencionesOpService = admRetencionesOpService;
            _admRetencionesRepository = admRetencionesRepository;
            _admRetencionesOpRepository = admRetencionesOpRepository;
            _admPucOrdenPagoRepository = admPucOrdenPagoRepository;
            _admImpuestosDocumentosOpRepository = admImpuestosDocumentosOpRepository;
        }

      


        
 

        
        

  
      
    }
 }

