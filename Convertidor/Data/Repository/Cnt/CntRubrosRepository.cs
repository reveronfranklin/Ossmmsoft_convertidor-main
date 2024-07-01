using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntRubrosRepository : ICntRubrosRepository
    {
        private readonly DataContextCnt _context;

        public CntRubrosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_RUBROS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_RUBROS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_RUBROS> GetByCodigo(int codigoRubro)
        {
            try
            {
                var result = await _context.CNT_RUBROS.DefaultIfEmpty().Where(x => x.CODIGO_RUBRO == codigoRubro).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_RUBROS>> Add(CNT_RUBROS entity)
        {

            ResultDto<CNT_RUBROS> result = new ResultDto<CNT_RUBROS>(null);
            try
            {
                await _context.CNT_RUBROS.AddAsync(entity);
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

        public async Task<ResultDto<CNT_RUBROS>> Update(CNT_RUBROS entity)
        {
            ResultDto<CNT_RUBROS> result = new ResultDto<CNT_RUBROS>(null);

            try
            {
                CNT_RUBROS entityUpdate = await GetByCodigo(entity.CODIGO_RUBRO);
                if (entityUpdate != null)
                {
                    _context.CNT_RUBROS.Update(entity);
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
                var last = await _context.CNT_RUBROS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_RUBRO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_RUBRO + 1;
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
