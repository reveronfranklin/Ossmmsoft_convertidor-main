using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntMayoresRepository : ICntMayoresRepository
    {
        private readonly DataContextCnt _context;

        public CntMayoresRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_MAYORES>> GetAll()
        {
            try
            {
                var result = await _context.CNT_MAYORES.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_MAYORES> GetByCodigo(int codigoMayor)
        {
            try
            {
                var result = await _context.CNT_MAYORES.DefaultIfEmpty().Where(x => x.CODIGO_MAYOR == codigoMayor).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_MAYORES>> Add(CNT_MAYORES entity)
        {

            ResultDto<CNT_MAYORES> result = new ResultDto<CNT_MAYORES>(null);
            try
            {
                await _context.CNT_MAYORES.AddAsync(entity);
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


        public async Task<ResultDto<CNT_MAYORES>> Update(CNT_MAYORES entity)
        {
            ResultDto<CNT_MAYORES> result = new ResultDto<CNT_MAYORES>(null);

            try
            {
                CNT_MAYORES entityUpdate = await GetByCodigo(entity.CODIGO_MAYOR);
                if (entityUpdate != null)
                {
                    _context.CNT_MAYORES.Update(entity);
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
                var last = await _context.CNT_MAYORES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_MAYOR)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_MAYOR + 1;
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
