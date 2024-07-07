using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntSaldosRepository : ICntSaldosRepository
    {
        private readonly DataContextCnt _context;

        public CntSaldosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_SALDOS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_SALDOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_SALDOS> GetByCodigo(int codigoSaldo)
        {
            try
            {
                var result = await _context.CNT_SALDOS.DefaultIfEmpty().Where(x => x.CODIGO_SALDO == codigoSaldo).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_SALDOS>> Add(CNT_SALDOS entity)
        {

            ResultDto<CNT_SALDOS> result = new ResultDto<CNT_SALDOS>(null);
            try
            {
                await _context.CNT_SALDOS.AddAsync(entity);
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

        public async Task<ResultDto<CNT_SALDOS>> Update(CNT_SALDOS entity)
        {
            ResultDto<CNT_SALDOS> result = new ResultDto<CNT_SALDOS>(null);

            try
            {
                CNT_SALDOS entityUpdate = await GetByCodigo(entity.CODIGO_SALDO);
                if (entityUpdate != null)
                {
                    _context.CNT_SALDOS.Update(entity);
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

        public async Task<string> Delete(int codigoSaldo)
        {
            try
            {
                CNT_SALDOS entity = await GetByCodigo(codigoSaldo);
                if (entity != null)
                {
                    _context.CNT_SALDOS.Remove(entity);
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
                var last = await _context.CNT_SALDOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_SALDO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_SALDO + 1;
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
