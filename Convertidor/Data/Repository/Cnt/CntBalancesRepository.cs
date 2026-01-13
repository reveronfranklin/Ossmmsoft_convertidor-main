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

        public async Task<CNT_BALANCES> GetByCodigo(int codigoBalance)
        {
            try
            {
                var result = await _context.CNT_BALANCES.DefaultIfEmpty().Where(x => x.CODIGO_BALANCE == codigoBalance).FirstOrDefaultAsync();

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

        public async Task<ResultDto<CNT_BALANCES>> Update(CNT_BALANCES entity)
        {
            ResultDto<CNT_BALANCES> result = new ResultDto<CNT_BALANCES>(null);

            try
            {
                CNT_BALANCES entityUpdate = await GetByCodigo(entity.CODIGO_BALANCE);
                if (entityUpdate != null)
                {
                    _context.CNT_BALANCES.Update(entity);
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

        public async Task<string> Delete(int codigoBalance)
        {
            try
            {
                CNT_BALANCES entity = await GetByCodigo(codigoBalance);
                if (entity != null)
                {
                    _context.CNT_BALANCES.Remove(entity);
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
