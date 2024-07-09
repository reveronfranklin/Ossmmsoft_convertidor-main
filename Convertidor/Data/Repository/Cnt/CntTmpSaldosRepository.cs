using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntTmpSaldosRepository : ICntTmpSaldosRepository
    {
        private readonly DataContextCnt _context;

        public CntTmpSaldosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_TMP_SALDOS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_TMP_SALDOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<CNT_TMP_SALDOS> GetByCodigo(int codigoTmpSaldo)
        {
            try
            {
                var result = await _context.CNT_TMP_SALDOS.DefaultIfEmpty().Where(x => x.CODIGO_TMP_SALDO == codigoTmpSaldo).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_TMP_SALDOS>> Add(CNT_TMP_SALDOS entity)
        {

            ResultDto<CNT_TMP_SALDOS> result = new ResultDto<CNT_TMP_SALDOS>(null);
            try
            {
                await _context.CNT_TMP_SALDOS.AddAsync(entity);
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
        public async Task<ResultDto<CNT_TMP_SALDOS>> Update(CNT_TMP_SALDOS entity)
        {
            ResultDto<CNT_TMP_SALDOS> result = new ResultDto<CNT_TMP_SALDOS>(null);

            try
            {
                CNT_TMP_SALDOS entityUpdate = await GetByCodigo(entity.CODIGO_TMP_SALDO);
                if (entityUpdate != null)
                {
                    _context.CNT_TMP_SALDOS.Update(entity);
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
                var last = await _context.CNT_TMP_SALDOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_TMP_SALDO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_TMP_SALDO + 1;
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
