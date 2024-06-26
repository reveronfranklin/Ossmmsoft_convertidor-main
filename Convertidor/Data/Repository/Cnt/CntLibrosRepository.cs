using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntLibrosRepository : ICntLibrosRepository
    {
        private readonly DataContextCnt _context;

        public CntLibrosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_LIBROS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_LIBROS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_LIBROS>> Add(CNT_LIBROS entity)
        {

            ResultDto<CNT_LIBROS> result = new ResultDto<CNT_LIBROS>(null);
            try
            {
                await _context.CNT_LIBROS.AddAsync(entity);
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
                var last = await _context.CNT_LIBROS.DefaultIfEmpty()
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
