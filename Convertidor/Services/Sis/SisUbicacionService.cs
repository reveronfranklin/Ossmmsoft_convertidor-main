using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Sis;

public class SisUbicacionService:ISisUbicacionService
{
    
    private readonly ISisUbicacionNacionalRepository _repository;
    public SisUbicacionService(ISisUbicacionNacionalRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<SelectListDescriptiva>> GetPaises()
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
          
        try
        {
            var paises = await _repository.GetPaises();
            foreach (var item in paises)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.PAIS;
                if (item.EXTRA1 == null) item.EXTRA1 = "";
                resultItem.Descripcion = item.EXTRA1;
                result.Add(resultItem);
            }
              
            return result;
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException.Message;
            return null;
        }


    }
    
    public async Task<List<SelectListDescriptiva>> GetEstados()
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var estados = await _repository.GetEstados();
            foreach (var item in estados)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.ENTIDAD;
                resultItem.Descripcion = item.EXTRA1;
                result.Add(resultItem);
            }
            
            return result;
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException.Message;
            return null;
        }


    }

    
    
}