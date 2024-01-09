using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhDescriptivasService
	{

        Task<string> GetDescripcionByCodigoDescriptiva(int descripcionId);
        Task<List<RH_DESCRIPTIVAS>> GetAll();
        Task<List<SelectListDescriptiva>> GetByTitulo(int tituloId);
	}
}

