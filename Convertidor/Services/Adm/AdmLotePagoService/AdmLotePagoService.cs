using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Adm;
using Convertidor.Services.Adm.Pagos;
using Convertidor.Utility;


namespace Convertidor.Services.Adm.AdmLotePagoService
{
    
	public partial class AdmLotePagoService: IAdmLotePagoService
    {
        private readonly IAdmLotePagoRepository _repository;
        private readonly ISisCuentaBancoRepository _sisCuentaBancoRepository;
        private readonly ISisDescriptivaRepository _sisDescriptivaRepository;
        private readonly ISisBancoRepository _bancoRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _presupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmChequesRepository _chequesRepository;
        private readonly IAdmPagoElectronicoService _admPagoElectronicoService;
        private readonly IAdmBeneficiariosOpRepository _admBeneficiariosOpRepository;
        private readonly IAdmBeneficiariosPagosRepository _admBeneficiariosPagosRepository;
        private readonly IAdmPagosService _admPagosService;


        public AdmLotePagoService(IAdmLotePagoRepository repository,
                                        ISisCuentaBancoRepository sisCuentaBancoRepository,
                                        ISisDescriptivaRepository sisDescriptivaRepository,
                                        ISisBancoRepository bancoRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                        IPRE_PRESUPUESTOSRepository presupuestosRepository,
                                        IAdmDescriptivaRepository admDescriptivaRepository,
                                        IAdmChequesRepository chequesRepository,
                                        IAdmPagoElectronicoService admPagoElectronicoService,
                                        IAdmBeneficiariosOpRepository admBeneficiariosOpRepository,
                                        IAdmBeneficiariosPagosRepository admBeneficiariosPagosRepository,
                                        IAdmPagosService admPagosService)
		{
            _repository = repository;
            _repository = repository;
            _sisCuentaBancoRepository = sisCuentaBancoRepository;
            _sisDescriptivaRepository = sisDescriptivaRepository;
            _bancoRepository = bancoRepository;
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _presupuestosRepository = presupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _chequesRepository = chequesRepository;
            _admPagoElectronicoService = admPagoElectronicoService;
            _admBeneficiariosOpRepository = admBeneficiariosOpRepository;
            _admBeneficiariosPagosRepository = admBeneficiariosPagosRepository;
            _admPagosService = admPagosService;
        }
        
    

  
     
    
        
    }
}

