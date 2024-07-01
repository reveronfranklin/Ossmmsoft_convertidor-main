using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntTmpConciliacionRepository : ICntTmpConciliacionRepository
    {
        private readonly DataContextCnt _context;

        public CntTmpConciliacionRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_TMP_CONCILIACION>> GetAll()
        {
            try
            {
                var result = await _context.CNT_TMP_CONCILIACION.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_TMP_CONCILIACION>> Add(CNT_TMP_CONCILIACION entity)
        {

            ResultDto<CNT_TMP_CONCILIACION> result = new ResultDto<CNT_TMP_CONCILIACION>(null);
            try
            {
                await _context.CNT_TMP_CONCILIACION.AddAsync(entity);
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
                var last = await _context.CNT_TMP_CONCILIACION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_TMP_CONCILIACION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_TMP_CONCILIACION + 1;
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
