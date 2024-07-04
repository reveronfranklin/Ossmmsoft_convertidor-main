using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntAuxiliaresRepository : ICntAuxiliaresRepository
    {
        private readonly DataContextCnt _context;

        public CntAuxiliaresRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_AUXILIARES>> GetAll()
        {
            try
            {
                var result = await _context.CNT_AUXILIARES.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_AUXILIARES> GetByCodigo(int codigoAuxiliar)
        {
            try
            {
                var result = await _context.CNT_AUXILIARES.DefaultIfEmpty().Where(x => x.CODIGO_AUXILIAR == codigoAuxiliar).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<CNT_AUXILIARES>> GetByCodigoMayor(int codigoMayor)
        {
            try
            {
                var result = await _context.CNT_AUXILIARES.DefaultIfEmpty().Where(x => x.CODIGO_MAYOR == codigoMayor).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_AUXILIARES>> Add(CNT_AUXILIARES entity)
        {

            ResultDto<CNT_AUXILIARES> result = new ResultDto<CNT_AUXILIARES>(null);
            try
            {
                await _context.CNT_AUXILIARES.AddAsync(entity);
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

        public async Task<ResultDto<CNT_AUXILIARES>> Update(CNT_AUXILIARES entity)
        {
            ResultDto<CNT_AUXILIARES> result = new ResultDto<CNT_AUXILIARES>(null);

            try
            {
                CNT_AUXILIARES entityUpdate = await GetByCodigo(entity.CODIGO_AUXILIAR);
                if (entityUpdate != null)
                {
                    _context.CNT_AUXILIARES.Update(entity);
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

        public async Task<string> Delete(int codigoAuxiliar)
        {
            try
            {
                CNT_AUXILIARES entity = await GetByCodigo(codigoAuxiliar);
                if (entity != null)
                {
                    _context.CNT_AUXILIARES.Remove(entity);
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
                var last = await _context.CNT_AUXILIARES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_AUXILIAR)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_AUXILIAR + 1;
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
