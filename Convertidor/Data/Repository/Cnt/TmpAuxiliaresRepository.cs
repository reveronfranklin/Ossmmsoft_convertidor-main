using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class TmpAuxiliaresRepository : ITmpAuxiliaresRepository
    {
        private readonly DataContextCnt _context;

        public TmpAuxiliaresRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<TMP_AUXILIARES>> GetAll()
        {
            try
            {
                var result = await _context.TMP_AUXILIARES.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<TMP_AUXILIARES> GetByCodigo(int codigoAuxiliar)
        {
            try
            {
                var result = await _context.TMP_AUXILIARES.DefaultIfEmpty().Where(x => x.CODIGO_AUXILIAR == codigoAuxiliar).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<TMP_AUXILIARES>> Add(TMP_AUXILIARES entity)
        {

            ResultDto<TMP_AUXILIARES> result = new ResultDto<TMP_AUXILIARES>(null);
            try
            {
                await _context.TMP_AUXILIARES.AddAsync(entity);
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

        public async Task<ResultDto<TMP_AUXILIARES>> Update(TMP_AUXILIARES entity)
        {
            ResultDto<TMP_AUXILIARES> result = new ResultDto<TMP_AUXILIARES>(null);

            try
            {
                TMP_AUXILIARES entityUpdate = await GetByCodigo(entity.CODIGO_AUXILIAR);
                if (entityUpdate != null)
                {
                    _context.TMP_AUXILIARES.Update(entity);
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
                TMP_AUXILIARES entity = await GetByCodigo(codigoAuxiliar);
                if (entity != null)
                {
                    _context.TMP_AUXILIARES.Remove(entity);
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
                var last = await _context.TMP_AUXILIARES.DefaultIfEmpty()
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
