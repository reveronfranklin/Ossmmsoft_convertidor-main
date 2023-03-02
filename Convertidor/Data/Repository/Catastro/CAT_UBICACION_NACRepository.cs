using System;
using System.Threading.Tasks;
using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
	public class CAT_UBICACION_NACRepository 
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
                _context.SaveChanges();

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
                _context.SaveChanges();
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


      
        public async Task<CAT_UBICACION_NAC> getPais()
        {

            CAT_UBICACION_NAC result = new CAT_UBICACION_NAC();
            var paisConfig = _configuration.GetValue<string>("PaisConfig");
            if (paisConfig.ToString().Length > 0)
            {
                result = await GetByKey(Int16.Parse(paisConfig));

            }
            return result;
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
                _context.SaveChanges();
            }


        }

      
    }
}

