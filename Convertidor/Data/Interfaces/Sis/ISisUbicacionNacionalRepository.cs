using System;
using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Rh;

namespace Convertidor.Data.Interfaces.Sis
{
	public interface 
		ISisUbicacionNacionalRepository
	{
        
        Task<List<SIS_UBICACION_NACIONAL>> GetPaises();
        Task<List<SIS_UBICACION_NACIONAL>> GetEstados();
        Task<List<SIS_UBICACION_NACIONAL>> GetMunicipios();
        Task<List<SIS_UBICACION_NACIONAL>> GetCiudades();
        Task<List<SIS_UBICACION_NACIONAL>> GetParroquias();
        Task<List<SIS_UBICACION_NACIONAL>> GetSectores();
        Task<List<SIS_UBICACION_NACIONAL>> GetUrbanizaciones();
        Task<SIS_UBICACION_NACIONAL> GetPais(int pais);
        Task<SIS_UBICACION_NACIONAL> GetEstado(int pais, int estado);
        Task<SIS_UBICACION_NACIONAL> GetMunicipio(int pais, int estado, int municipio);
        Task<SIS_UBICACION_NACIONAL> GetCiudad(int pais, int estado,int municipio, int ciudad);
        Task<SIS_UBICACION_NACIONAL> GetParroquia(int pais, int estado, int municipio,int ciudad, int parroquia);
        Task<SIS_UBICACION_NACIONAL> GetSector(int pais, int estado, int municipio,int ciudad, int parroquia, int sector);
        Task<SIS_UBICACION_NACIONAL> GetUrbanizacion(int pais, int estado, int municipio, int ciudad, int parroquia, int sector,
            int urbanizacion);
        
    }
}

