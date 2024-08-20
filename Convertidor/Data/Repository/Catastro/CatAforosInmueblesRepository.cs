using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatAforosInmueblesRepository : ICatAforosInmueblesRepository
    {
        private readonly DataContextCat _context;

        public CatAforosInmueblesRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_AFOROS_INMUEBLES>> GetAll()
        {
            try
            {
                var result = await _context.CAT_AFOROS_INMUEBLES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CAT_AFOROS_INMUEBLES> GetByCodigo(int codigoAforoInmueble)
        {
            try
            {

                var result = await _context.CAT_AFOROS_INMUEBLES.DefaultIfEmpty()
                    .Where(x => x.CODIGO_AFORO_INMUEBLE == codigoAforoInmueble)
                    .FirstOrDefaultAsync();
                return (CAT_AFOROS_INMUEBLES)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_AFOROS_INMUEBLES>> Add(CAT_AFOROS_INMUEBLES entity)
        {
            ResultDto<CAT_AFOROS_INMUEBLES> result = new ResultDto<CAT_AFOROS_INMUEBLES>(null);
            try
            {



                await _context.CAT_AFOROS_INMUEBLES.AddAsync(entity);
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

        public async Task<ResultDto<CAT_AFOROS_INMUEBLES>> Update(CAT_AFOROS_INMUEBLES entity)
        {
            ResultDto<CAT_AFOROS_INMUEBLES> result = new ResultDto<CAT_AFOROS_INMUEBLES>(null);

            try
            {
                CAT_AFOROS_INMUEBLES entityUpdate = await GetByCodigo(entity.CODIGO_AFORO_INMUEBLE);
                if (entityUpdate != null)
                {


                    _context.CAT_AFOROS_INMUEBLES.Update(entity);
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

        public async Task<string> Delete(int codigoAforoInmueble)
        {

            try
            {
                CAT_AFOROS_INMUEBLES entity = await GetByCodigo(codigoAforoInmueble);
                if (entity != null)
                {
                    _context.CAT_AFOROS_INMUEBLES.Remove(entity);
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
                var last = await _context.CAT_AFOROS_INMUEBLES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_AFORO_INMUEBLE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_AFORO_INMUEBLE + 1;
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
