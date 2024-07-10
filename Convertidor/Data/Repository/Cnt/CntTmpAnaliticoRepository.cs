using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntTmpAnaliticoRepository : ICntTmpAnaliticoRepository
    {
        private readonly DataContextCnt _context;

        public CntTmpAnaliticoRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_TMP_ANALITICO>> GetAll()
        {
            try
            {
                var result = await _context.CNT_TMP_ANALITICO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_TMP_ANALITICO>> Add(CNT_TMP_ANALITICO entity)
        {

            ResultDto<CNT_TMP_ANALITICO> result = new ResultDto<CNT_TMP_ANALITICO>(null);
            try
            {
                await _context.CNT_TMP_ANALITICO.AddAsync(entity);
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
                var last = await _context.CNT_TMP_ANALITICO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_TMP_ANALITICO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_TMP_ANALITICO + 1;
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
