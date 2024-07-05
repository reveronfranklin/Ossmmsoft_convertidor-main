using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntPeriodosRepository : ICntPeriodosRepository
    {
        private readonly DataContextCnt _context;

        public CntPeriodosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_PERIODOS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_PERIODOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_PERIODOS> GetByCodigo(int codigoPeriodo)
        {
            try
            {
                var result = await _context.CNT_PERIODOS.DefaultIfEmpty().Where(x => x.CODIGO_PERIODO == codigoPeriodo).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_PERIODOS>> Add(CNT_PERIODOS entity)
        {

            ResultDto<CNT_PERIODOS> result = new ResultDto<CNT_PERIODOS>(null);
            try
            {
                await _context.CNT_PERIODOS.AddAsync(entity);
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


        public async Task<ResultDto<CNT_PERIODOS>> Update(CNT_PERIODOS entity)
        {
            ResultDto<CNT_PERIODOS> result = new ResultDto<CNT_PERIODOS>(null);

            try
            {
                CNT_PERIODOS entityUpdate = await GetByCodigo(entity.CODIGO_PERIODO);
                if (entityUpdate != null)
                {
                    _context.CNT_PERIODOS.Update(entity);
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
                var last = await _context.CNT_PERIODOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PERIODO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PERIODO + 1;
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
