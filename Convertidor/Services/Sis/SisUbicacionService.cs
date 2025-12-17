namespace Convertidor.Services.Sis;

public class SisUbicacionService : ISisUbicacionService
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

    public async Task<SelectListDescriptiva> GetPais(int codigoPais)
    {
        SelectListDescriptiva result = new SelectListDescriptiva();

        try
        {
            var pais = await _repository.GetPais(codigoPais);
            if (pais != null)
            {

                result.Id = (int)pais.PAIS;
                if (pais.EXTRA1 == null) pais.EXTRA1 = "";
                result.Descripcion = pais.EXTRA1;
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


 public async Task<List<SelectListDescriptiva>> GetEstadosPorPais(FiltersEstado filter)
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var estados = await _repository.GetEstadosPorPais(filter.CodigoPais);
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

    public async Task<SelectListDescriptiva> GetEstado(int pais, int codigoEstado)
    {
        SelectListDescriptiva result = new SelectListDescriptiva();
        try
        {
            var estado = await _repository.GetEstado(pais, codigoEstado);
            if (estado != null)
            {
                result.Id = (int)estado.ENTIDAD;
                if (estado.EXTRA1 == null) estado.EXTRA1 = "";
                result.Descripcion = estado.EXTRA1;
            }

            return result;
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException.Message;
            return null;
        }


    }

    public async Task<List<SelectListDescriptiva>> GetMunicipios()
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var municipios = await _repository.GetMunicipios();
            foreach (var item in municipios)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.MUNICIPIO;
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

     public async Task<List<SelectListDescriptiva>> GetMunicipiosPorPaisEstado(FiltersMunicipio filter)
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var municipios = await _repository.GetMunicipiosPorPaisEstado(filter.CodigoPais, filter.CodigoEstado);  
            foreach (var item in municipios)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.MUNICIPIO;
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

    public async Task<SelectListDescriptiva> GetMunicipio(int pais, int estado, int codigoMunicipio)
    {
        SelectListDescriptiva result = new SelectListDescriptiva();
        try
        {
            var municipio = await _repository.GetMunicipio(pais, estado, codigoMunicipio);
            if (municipio != null)
            {
                result.Id = (int)municipio.MUNICIPIO;
                if (municipio.EXTRA1 == null) municipio.EXTRA1 = "";
                result.Descripcion = municipio.EXTRA1;
            }

            return result;
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException.Message;
            return null;
        }


    }

    public async Task<List<SelectListDescriptiva>> GetCiudades()
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var municipios = await _repository.GetCiudades();
            foreach (var item in municipios)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.CIUDAD;
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
     public async Task<List<SelectListDescriptiva>> GetCiudadesPorPaisEstadoMunicipio(FiltersCiudad filter)
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var municipios = await _repository.GetCiudadesPorPaisEstadoMunicipio(filter.CodigoPais, filter.CodigoEstado, filter.CodigoMunicipio);
            foreach (var item in municipios)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.CIUDAD;
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

     public async Task<List<SelectListDescriptiva>> GetParroquiasPorPaisEstadoMunicipioCiudad(FiltersParroquia filter)
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var parroquias = await _repository.GetParroquiasPorPaisEstadoMunicipioCiudad(filter.CodigoPais, filter.CodigoEstado, filter.CodigoMunicipio,filter.CodigoCiudad);
            foreach (var item in parroquias)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.PARROQUIA;
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

    public async Task<SelectListDescriptiva> GetCiudad(int pais, int estado,int municipio, int codigoCiudad)
    {
        SelectListDescriptiva result = new SelectListDescriptiva();
        try
        {
            var ciudad = await _repository.GetCiudad(pais, estado,municipio,codigoCiudad);
            if (ciudad != null)
            {
                result.Id = (int)ciudad.CIUDAD;
                if (ciudad.EXTRA1 == null) ciudad.EXTRA1 = "";
                result.Descripcion = ciudad.EXTRA1;
            }

            return result;
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException.Message;
            return null;
        }


    }
    public async Task<List<SelectListDescriptiva>> GetParroquias()
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var parroquias = await _repository.GetParroquias();
            foreach (var item in parroquias)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.PARROQUIA;
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

    public async Task<SelectListDescriptiva> GetParroquia(int pais, int estado,int municipio, int ciudad, int codigoParroquia)
    {
        SelectListDescriptiva result = new SelectListDescriptiva();
        try
        {
            var parroquia = await _repository.GetParroquia(pais, estado,municipio,ciudad, codigoParroquia);
            if (parroquia != null)
            {
                result.Id = (int)parroquia.PARROQUIA;
                if (parroquia.EXTRA1 == null) parroquia.EXTRA1 = "";
                result.Descripcion = parroquia.EXTRA1;
            }

            return result;
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException.Message;
            return null;
        }
    }

    public async Task<List<SelectListDescriptiva>> Getsectores()
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var sectores = await _repository.GetSectores();
            foreach (var item in sectores)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.SECTOR;
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

    public async Task<SelectListDescriptiva> GetSector(int pais, int estado,int municipio,int ciudad,int parroquia, int codigoSector)
    {
        SelectListDescriptiva result = new SelectListDescriptiva();
        try
        {
            var sector = await _repository.GetSector(pais, estado,municipio,ciudad,parroquia, codigoSector);
            if (sector != null)
            {
                result.Id = (int)sector.SECTOR;
                if (sector.EXTRA1 == null) sector.EXTRA1 = "";
                result.Descripcion = sector.EXTRA1;
            }

            return result;
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException.Message;
            return null;
        }
    }

 
 public async Task<List<SelectListDescriptiva>> GetSectoresPorPaisEstadoMunicipioCiudadParroquias(FiltersSector filter)
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var parroquias = await _repository.GetSectoresPorPaisEstadoMunicipioCiudadParroquias(filter);
            foreach (var item in parroquias)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.SECTOR;
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


public async Task<List<SelectListDescriptiva>> GetUrnanizacionesPorPaisEstadoMunicipioCiudadParroquiasSector(FiltersUrbanizacion filter)
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var parroquias = await _repository.GetUrbanizacionesPorPaisEstadoMunicipioCiudadParroquiasSector(filter);
            foreach (var item in parroquias)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.SECTOR;
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

    public async Task<List<SelectListDescriptiva>> GetUrbanizaciones()
    {
        List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
        try
        {
            var urbanizaciones = await _repository.GetUrbanizaciones();
            foreach (var item in urbanizaciones)
            {
                SelectListDescriptiva resultItem = new SelectListDescriptiva();
                resultItem.Id = (int)item.URBANIZACION;
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

    public async Task<SelectListDescriptiva> GetUrbanizacion(int pais, int estado, int municipio,
        int ciudad,int parroquia,int sector, int codigoUrbanizacion)
    {
        SelectListDescriptiva result = new SelectListDescriptiva();
        try
        {
            var urbanizacion = await _repository.GetUrbanizacion(pais, estado,municipio,ciudad,parroquia,sector, codigoUrbanizacion);
            if (urbanizacion != null)
            {
                result.Id = (int)urbanizacion.URBANIZACION;
                if (urbanizacion.EXTRA1 == null) urbanizacion.EXTRA1 = "";
                result.Descripcion = urbanizacion.EXTRA1;
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