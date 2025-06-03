
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Ganss.Excel;

namespace Convertidor.Services.Adm.Pagos.AdmPagoElectronicoService
{
    public partial class AdmPagoElectronicoService : IAdmPagoElectronicoService
    {
        private readonly IAdmPagoElectronicoRepository _repository;
        private readonly IAdmLotePagoRepository _admLotePagoRepository;
        private readonly IConfiguration _configuration;

        public AdmPagoElectronicoService( IAdmPagoElectronicoRepository repository,IAdmLotePagoRepository admLotePagoRepository,IConfiguration configuration)
        {
            _repository = repository;
            _admLotePagoRepository = admLotePagoRepository;
            _configuration = configuration;
        }


  
        

        
        
    }
 }

