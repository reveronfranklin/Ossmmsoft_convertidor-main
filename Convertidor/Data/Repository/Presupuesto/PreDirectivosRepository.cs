using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class PreDirectivosRepository: IPreDirectivosRepository
    {
		
        private readonly DataContextPre _context;

        public PreDirectivosRepository(DataContextPre context)
        {
            _context = context;
        }
      
        public async Task<PRE_DIRECTIVOS> GetByCodigo(int codigoDirectivo)
        {
            try
            {
                var result = await _context.PRE_DIRECTIVOS.DefaultIfEmpty().Where(e => e.CODIGO_DIRECTIVO == codigoDirectivo).FirstOrDefaultAsync();

                return (PRE_DIRECTIVOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

    
        public async Task<List<PRE_DIRECTIVOS>> GetAll()
        {
            try
            {
                var result = await _context.PRE_DIRECTIVOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

       

        public async Task<ResultDto<PRE_DIRECTIVOS>> Add(PRE_DIRECTIVOS entity)
        {
            ResultDto<PRE_DIRECTIVOS> result = new ResultDto<PRE_DIRECTIVOS>(null);
            try
            {



                await _context.PRE_DIRECTIVOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_DIRECTIVOS>> Update(PRE_DIRECTIVOS entity)
        {
            ResultDto<PRE_DIRECTIVOS> result = new ResultDto<PRE_DIRECTIVOS>(null);

            try
            {
                PRE_DIRECTIVOS entityUpdate = await GetByCodigo(entity.CODIGO_DIRECTIVO);
                if (entityUpdate != null)
                {


                    _context.PRE_DIRECTIVOS.Update(entity);
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

        public async Task<string> Delete(int codigoDirectivo)
        {

            try
            {
                PRE_DIRECTIVOS entity = await GetByCodigo(codigoDirectivo);
                if (entity != null)
                {
                    _context.PRE_DIRECTIVOS.Remove(entity);
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
                var last = await _context.PRE_DIRECTIVOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DIRECTIVO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DIRECTIVO + 1;
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

