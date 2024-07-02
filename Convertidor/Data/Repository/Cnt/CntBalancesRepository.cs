using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntBalancesRepository : ICntBalancesRepository
    {
        private readonly DataContextCnt _context;

        public CntBalancesRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_BALANCES>> GetAll()
        {
            try
            {
                var result = await _context.CNT_BALANCES.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_BALANCES>> Add(CNT_BALANCES entity)
        {

            ResultDto<CNT_BALANCES> result = new ResultDto<CNT_BALANCES>(null);
            try
            {
                await _context.CNT_BALANCES.AddAsync(entity);
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
                var last = await _context.CNT_BALANCES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_BALANCE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_BALANCE + 1;
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
