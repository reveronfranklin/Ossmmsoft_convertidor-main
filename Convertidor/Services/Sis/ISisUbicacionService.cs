using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Sis;

public interface ISisUbicacionService
{
    Task<List<SelectListDescriptiva>> GetPaises();
    Task<List<SelectListDescriptiva>> GetEstados();
    Task<List<SelectListDescriptiva>> GetMunicipios();
    Task<List<SelectListDescriptiva>> GetCiudades();
    Task<List<SelectListDescriptiva>> GetParroquias();
    Task<List<SelectListDescriptiva>> Getsectores();
    Task<List<SelectListDescriptiva>> GetUrbanizaciones();
    Task<SelectListDescriptiva> GetPais(int Pais);
    Task<SelectListDescriptiva> GetEstado(int pais, int codigoEstado);
    Task<SelectListDescriptiva> GetMunicipio(int pais, int estado, int municipio);
    Task<SelectListDescriptiva> GetCiudad(int pais, int estado,int municipio, int ciudad);
    Task<SelectListDescriptiva> GetParroquia(int pais, int estado,int municipio, int ciudad,int Parroquia);
    Task<SelectListDescriptiva> GetSector(int pais, int estado,int municipio, int ciudad, int parroquia, int sector);
    Task<SelectListDescriptiva> GetUrbanizacion(int pais, int estado, int municipio, int ciudad, int parroquia,int sector, int urbanizacion);
   

}