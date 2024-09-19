using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICAT_UBICACION_NACRepository
    {
        Task<List<CAT_UBICACION_NAC>> GetPaises();
        Task<List<CAT_UBICACION_NAC>> GetEstados();
        Task<List<CAT_UBICACION_NAC>> GetMunicipios();
        Task<List<CAT_UBICACION_NAC>> GetCiudades();
        Task<List<CAT_UBICACION_NAC>> GetParroquias();
        Task<List<CAT_UBICACION_NAC>> GetSectores();
        Task<List<CAT_UBICACION_NAC>> GetUrbanizaciones();
        Task<CAT_UBICACION_NAC> GetPais(int pais);
        Task<CAT_UBICACION_NAC> GetEstado(int pais, int estado);
        Task<CAT_UBICACION_NAC> GetMunicipio(int pais, int estado, int municipio);
        Task<CAT_UBICACION_NAC> GetCiudad(int pais, int estado, int municipio, int ciudad);
        Task<CAT_UBICACION_NAC> GetParroquia(int pais, int estado, int municipio, int ciudad, int parroquia);
        Task<CAT_UBICACION_NAC> GetSector(int pais, int estado, int municipio, int ciudad, int parroquia, int sector);
        Task<CAT_UBICACION_NAC> GetUrbanizacion(int pais, int estado, int municipio, int ciudad, int parroquia, int sector,
            int urbanizacion);

    }
}
