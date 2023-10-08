using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Sis;

public interface ISisUbicacionService
{
    Task<List<SelectListDescriptiva>> GetPaises();
    Task<List<SelectListDescriptiva>> GetEstados();

}