using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;


namespace Convertidor.Services.Adm.Proveedores.AdmProveedores
{
	public partial class AdmProveedoresService: IAdmProveedoresService
    {

      
        private readonly IAdmProveedoresRepository _repository;
        private readonly IAdmDescriptivaRepository _repositoryPreDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonaService _personaServices;
        public AdmProveedoresService(IAdmProveedoresRepository repository,
                                      IAdmDescriptivaRepository repositoryPreDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IRhPersonaService personaServices)
		{
            _repository = repository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;
            _personaServices = personaServices;
        }



       
       



        
    }
}

