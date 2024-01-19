using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhComunicacionesRepository : IRhComunicacionesRepository
    {
		
        private readonly DataContext _context;

        public RhComunicacionesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_COMUNICACIONES>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_COMUNICACIONES.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona).ToListAsync();
        
                return (List<RH_COMUNICACIONES>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_COMUNICACIONES> GetByCodigo(int codigoComunicacion)
        {
            try
            {
                var result = await _context.RH_COMUNICACIONES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_COMUNICACION == codigoComunicacion)
                    .FirstOrDefaultAsync();

                return (RH_COMUNICACIONES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<RH_COMUNICACIONES>> Add(RH_COMUNICACIONES entity)
        {
            ResultDto<RH_COMUNICACIONES> result = new ResultDto<RH_COMUNICACIONES>(null);
            try
            {



                await _context.RH_COMUNICACIONES.AddAsync(entity);
                await _context.SaveChangesAsync();


                result.Data = entity;
                result.IsValid = true;
                result.Message = "";
                return result;


            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }



        }

        public async Task<ResultDto<RH_COMUNICACIONES>> Update(RH_COMUNICACIONES entity)
        {
            ResultDto<RH_COMUNICACIONES> result = new ResultDto<RH_COMUNICACIONES>(null);

            try
            {
                RH_COMUNICACIONES entityUpdate = await GetByCodigo(entity.CODIGO_COMUNICACION);
                if (entityUpdate != null)
                {


                    _context.RH_COMUNICACIONES.Update(entity);
                    await _context.SaveChangesAsync();
                    result.Data = entity;
                    result.IsValid = true;
                    result.Message = "";

                }
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }






        }

        public async Task<string> Delete(int codigoComunicacion)
        {

            try
            {
                RH_COMUNICACIONES entity = await GetByCodigo(codigoComunicacion);
                if (entity != null)
                {
                    _context.RH_COMUNICACIONES.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.RH_COMUNICACIONES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_COMUNICACION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_COMUNICACION + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

    }
}

