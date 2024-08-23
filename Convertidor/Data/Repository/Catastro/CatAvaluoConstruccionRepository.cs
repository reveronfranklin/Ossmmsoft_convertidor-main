using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatAvaluoConstruccionRepository : ICatAvaluoConstruccionRepository
    {
        private readonly DataContextCat _context;

        public CatAvaluoConstruccionRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_AVALUO_CONSTRUCCION>> GetAll()
        {
            try
            {
                var result = await _context.CAT_AVALUO_CONSTRUCCION.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CAT_AVALUO_CONSTRUCCION> GetByCodigo(int codigoAvaluoConstruccion)
        {
            try
            {

                var result = await _context.CAT_AVALUO_CONSTRUCCION.DefaultIfEmpty()
                    .Where(x => x.CODIGO_AVALUO_CONSTRUCCION == codigoAvaluoConstruccion)
                    .FirstOrDefaultAsync();
                return (CAT_AVALUO_CONSTRUCCION)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_AVALUO_CONSTRUCCION>> Add(CAT_AVALUO_CONSTRUCCION entity)
        {
            ResultDto<CAT_AVALUO_CONSTRUCCION> result = new ResultDto<CAT_AVALUO_CONSTRUCCION>(null);
            try
            {



                await _context.CAT_AVALUO_CONSTRUCCION.AddAsync(entity);
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

        public async Task<ResultDto<CAT_AVALUO_CONSTRUCCION>> Update(CAT_AVALUO_CONSTRUCCION entity)
        {
            ResultDto<CAT_AVALUO_CONSTRUCCION> result = new ResultDto<CAT_AVALUO_CONSTRUCCION>(null);

            try
            {
                CAT_AVALUO_CONSTRUCCION entityUpdate = await GetByCodigo(entity.CODIGO_AVALUO_CONSTRUCCION);
                if (entityUpdate != null)
                {


                    _context.CAT_AVALUO_CONSTRUCCION.Update(entity);
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

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.CAT_AVALUO_CONSTRUCCION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_AVALUO_CONSTRUCCION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_AVALUO_CONSTRUCCION + 1;
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
