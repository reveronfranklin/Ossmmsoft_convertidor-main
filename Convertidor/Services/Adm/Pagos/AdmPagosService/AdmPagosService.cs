using System.DirectoryServices;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Pagos;
using Convertidor.Utility;

namespace Convertidor.Services.Adm.Pagos.AdmPagosService
{
    public partial class AdmPagosService : IAdmPagosService
    {
        private readonly IAdmChequesRepository _repository;
        private readonly IAdmProveedoresRepository _proveedoresRepository;
        private readonly IAdmContactosProveedorRepository _contactosProveedorRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly ISisCuentaBancoRepository _sisCuentaBancoRepository;
        private readonly ISisBancoRepository _sisBancoRepository;
        private readonly IAdmLotePagoRepository _admLotePagoRepository;
        private readonly IAdmBeneficiariosPagosRepository _beneficiariosPagosRepository;
        private readonly IOssConfigRepository _ossConfigRepository;
        private readonly IAdmBeneficiariosOpRepository _admBeneficiariosOpRepository;
        private readonly IAdmOrdenPagoRepository _ordenPagoRepository;

        public AdmPagosService( IAdmChequesRepository repository,
                                      IAdmProveedoresRepository proveedoresRepository,
                                      IAdmContactosProveedorRepository contactosProveedorRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                      IAdmDescriptivaRepository admDescriptivaRepository,
                                      ISisCuentaBancoRepository sisCuentaBancoRepository,
                                      ISisBancoRepository sisBancoRepository,
                                      IAdmLotePagoRepository admLotePagoRepository,
                                        IAdmBeneficiariosPagosRepository beneficiariosPagosRepository,
                                      IOssConfigRepository ossConfigRepository,
                                      IAdmBeneficiariosOpRepository admBeneficiariosOpRepository,
                                      IAdmOrdenPagoRepository ordenPagoRepository
                                    )
        {
            _repository = repository;
            _proveedoresRepository = proveedoresRepository;
            _contactosProveedorRepository = contactosProveedorRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _sisCuentaBancoRepository = sisCuentaBancoRepository;
            _sisBancoRepository = sisBancoRepository;
            _admLotePagoRepository = admLotePagoRepository;
            _beneficiariosPagosRepository = beneficiariosPagosRepository;
            _ossConfigRepository = ossConfigRepository;
            _admBeneficiariosOpRepository = admBeneficiariosOpRepository;
            _ordenPagoRepository = ordenPagoRepository;
        }
        
        
        
    }
 }

