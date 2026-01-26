using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;
using Microsoft.Extensions.Caching.Distributed;


namespace Convertidor.Services.Adm.Proveedores.AdmProveedores
{
	public partial class AdmProveedoresService: IAdmProveedoresService
    {

      
        private readonly IAdmProveedoresRepository _repository;
        private readonly IAdmDescriptivaRepository _repositoryPreDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonaService _personaServices;
         private readonly IDistributedCache _distributedCache;
        public AdmProveedoresService(IAdmProveedoresRepository repository,
                                      IAdmDescriptivaRepository repositoryPreDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IRhPersonaService personaServices,
                                      IDistributedCache distributedCache)
		{
            _repository = repository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;
            _personaServices = personaServices;
            _distributedCache = distributedCache;
        }



       
       



        
    }
}

