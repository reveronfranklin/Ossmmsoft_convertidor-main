using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
	public class CAT_UBICACION_NACRepository : ICAT_UBICACION_NACRepository
    {
		

        private readonly DataContextCat _context;
        private readonly IConfiguration _configuration;
        public CAT_UBICACION_NACRepository(DataContextCat context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

     


        public async Task<List<CAT_UBICACION_NAC>> Get()
        {
            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<bool> Add(CAT_UBICACION_NAC entity)
        {
            try
            {
                _context.CAT_UBICACION_NAC.Add(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task Update(CAT_UBICACION_NAC entity)
        {
            CAT_UBICACION_NAC entityExist = await GetByKey(entity.CODIGO_UBICACION_NAC);
            if (entityExist != null)
            {

                _context.CAT_UBICACION_NAC.Update(entity);
                await _context.SaveChangesAsync();
            }


        }


        public async Task<int> GetNext()
        {
            try
            {

                int result = 0;

           
                var entity = await _context.CAT_UBICACION_NAC.DefaultIfEmpty().OrderByDescending(c=> c.CODIGO_UBICACION_NAC).FirstOrDefaultAsync();
                if (entity != null ) {
                    result = entity.CODIGO_UBICACION_NAC + 1;

                }
                else
                {
                    result = 1;
                }
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return 0;
            }
        }


        public async Task<CAT_UBICACION_NAC> GetPais(int pais)
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty().Where(x => x.PAIS == pais && x.ENTIDAD == 0).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        public async Task<List<CAT_UBICACION_NAC>> GetPaises()
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS != 0 && x.ENTIDAD == 0)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<List<CAT_UBICACION_NAC>> GetEstados()
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS != 0 && x.ENTIDAD != 0 && x.MUNICIPIO == 0 && x.CIUDAD == 0 && x.PARROQUIA == 0 &&
                    x.SECTOR == 0 && x.URBANIZACION == 0)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<List<CAT_UBICACION_NAC>> GetMunicipios()
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS != 0 && x.ENTIDAD != 0 && x.MUNICIPIO != 0 && x.CIUDAD == 0 && x.PARROQUIA == 0 &&
                    x.SECTOR == 0 && x.URBANIZACION == 0)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<List<CAT_UBICACION_NAC>> GetCiudades()
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS != 0 && x.ENTIDAD != 0 && x.MUNICIPIO == 0 && x.CIUDAD != 0 && x.PARROQUIA == 0 &&
                    x.SECTOR == 0 && x.URBANIZACION == 0)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<List<CAT_UBICACION_NAC>> GetParroquias()
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS != 0 && x.ENTIDAD != 0 && x.MUNICIPIO == 0 && x.CIUDAD == 0 && x.PARROQUIA != 0 &&
                    x.SECTOR == 0 && x.URBANIZACION == 0)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<List<CAT_UBICACION_NAC>> GetSectores()
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS != 0 && x.ENTIDAD != 0 && x.MUNICIPIO == 0 && x.CIUDAD == 0 && x.PARROQUIA == 0 &&
                    x.SECTOR != 0 && x.URBANIZACION == 0)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<List<CAT_UBICACION_NAC>> GetUrbanizaciones()
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS != 0 && x.ENTIDAD != 0 && x.MUNICIPIO == 0 && x.CIUDAD == 0 && x.PARROQUIA == 0 &&
                    x.SECTOR == 0 && x.URBANIZACION != 0)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }

        }



        public async Task<CAT_UBICACION_NAC> GetEstado(int pais, int estado)
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS == pais && x.ENTIDAD == estado).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<CAT_UBICACION_NAC> GetMunicipio(int pais, int estado, int municipio)
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS == pais && x.ENTIDAD == estado && x.MUNICIPIO == municipio).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<CAT_UBICACION_NAC> GetCiudad(int pais, int estado, int municipio, int ciudad)
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS == pais && x.ENTIDAD == estado && x.MUNICIPIO == municipio &&
                    x.CIUDAD == ciudad && x.PARROQUIA == 0 && x.SECTOR == 0 && x.URBANIZACION == 0).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<CAT_UBICACION_NAC> GetParroquia(int pais, int estado, int municipio, int ciudad, int Parroquia)
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS == pais && x.ENTIDAD == estado && x.MUNICIPIO == 0 &&
                    x.CIUDAD == 0 && x.PARROQUIA == Parroquia && x.SECTOR == 0 && x.URBANIZACION == 0).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<CAT_UBICACION_NAC> GetSector(int pais, int estado, int municipio, int ciudad, int parroquia, int Sector)
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS == pais && x.ENTIDAD == estado && x.MUNICIPIO == 0 &&
                    x.CIUDAD == 0 && x.PARROQUIA == 0 && x.SECTOR == Sector && x.URBANIZACION == 0).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<CAT_UBICACION_NAC> GetUrbanizacion(int pais, int estado, int municipio,
            int ciudad, int parroquia, int Sector, int urbanizacion)
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS == pais && x.ENTIDAD == estado && x.MUNICIPIO == 0 &&
                    x.CIUDAD == 0 && x.PARROQUIA == 0 && x.SECTOR == 0 && x.URBANIZACION == urbanizacion).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<CAT_UBICACION_NAC> GetManzana(int pais, int estado, int municipio,
           int ciudad, int parroquia, int Sector, int urbanizacion,int manzana)
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS == pais && x.ENTIDAD == estado && x.MUNICIPIO == 0 &&
                    x.CIUDAD == 0 && x.PARROQUIA == 0 && x.SECTOR == 0 && x.URBANIZACION == 0 && x.MANZANA == manzana).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<CAT_UBICACION_NAC> GetParcela(int pais, int estado, int municipio,
           int ciudad, int parroquia, int Sector, int urbanizacion, int manzana,int parcela)
        {

            try
            {
                var result = await _context.CAT_UBICACION_NAC.DefaultIfEmpty()
                    .Where(x => x.PAIS == pais && x.ENTIDAD == estado && x.MUNICIPIO == 0 &&
                    x.CIUDAD == 0 && x.PARROQUIA == 0 && x.SECTOR == 0 && x.URBANIZACION == 0 && x.MANZANA == 0 && x.PARCELA == parcela).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }


        public async Task<CAT_UBICACION_NAC> GetByKey(int codigoUbicacion)
        {
             
            var result = await _context.CAT_UBICACION_NAC.Where(x => x.CODIGO_UBICACION_NAC == codigoUbicacion).FirstOrDefaultAsync();
            return result;
        }



        public async Task Delete(int id)
        {
            CAT_UBICACION_NAC entity = await GetByKey(id);
            if (entity != null)
            {
                _context.CAT_UBICACION_NAC.Remove(entity);
                await _context.SaveChangesAsync();
            }


        }

      
    }
}

