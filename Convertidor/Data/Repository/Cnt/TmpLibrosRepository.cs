using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class TmpLibrosRepository : ITmpLibrosRepository
    {
        private readonly DataContextCnt _context;

        public TmpLibrosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<TMP_LIBROS>> GetAll()
        {
            try
            {
                var result = await _context.TMP_LIBROS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<TMP_LIBROS>> Add(TMP_LIBROS entity)
        {

            ResultDto<TMP_LIBROS> result = new ResultDto<TMP_LIBROS>(null);
            try
            {
                await _context.TMP_LIBROS.AddAsync(entity);
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

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.TMP_LIBROS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_LIBRO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_LIBRO + 1;
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
