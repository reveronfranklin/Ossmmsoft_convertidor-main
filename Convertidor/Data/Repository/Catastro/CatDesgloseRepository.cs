using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDesgloseRepository : ICatDesgloseRepository
    {
        private readonly DataContextCat _context;

        public CatDesgloseRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_DESGLOSE>> GetAll()
        {
            try
            {
                var result = await _context.CAT_DESGLOSE.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CAT_DESGLOSE> GetByCodigo(int codigoDesglose)
        {
            try
            {

                var result = await _context.CAT_DESGLOSE.DefaultIfEmpty()
                    .Where(x => x.CODIGO_DESGLOSE == codigoDesglose)
                    .FirstOrDefaultAsync();
                return (CAT_DESGLOSE)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_DESGLOSE>> Add(CAT_DESGLOSE entity)
        {
            ResultDto<CAT_DESGLOSE> result = new ResultDto<CAT_DESGLOSE>(null);
            try
            {



                await _context.CAT_DESGLOSE.AddAsync(entity);
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

        public async Task<ResultDto<CAT_DESGLOSE>> Update(CAT_DESGLOSE entity)
        {
            ResultDto<CAT_DESGLOSE> result = new ResultDto<CAT_DESGLOSE>(null);

            try
            {
                CAT_DESGLOSE entityUpdate = await GetByCodigo(entity.CODIGO_DESGLOSE);
                if (entityUpdate != null)
                {


                    _context.CAT_DESGLOSE.Update(entity);
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
                var last = await _context.CAT_DESGLOSE.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DESGLOSE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DESGLOSE + 1;
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
