using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntReversoConciliacionRepository : ICntReversoConciliacionRepository
    {
        private readonly DataContextCnt _context;

        public CntReversoConciliacionRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_REVERSO_CONCILIACION>> GetAll()
        {
            try
            {
                var result = await _context.CNT_REVERSO_CONCILIACION.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_REVERSO_CONCILIACION> GetByCodigo(int codigoHistConciliacion)
        {
            try
            {
                var result = await _context.CNT_REVERSO_CONCILIACION.DefaultIfEmpty().Where(x => x.CODIGO_HIST_CONCILIACION == codigoHistConciliacion).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<CNT_REVERSO_CONCILIACION>> GetByCodigoConciliacion(int codigoConciliacion)
        {
            try
            {
                var result = await _context.CNT_REVERSO_CONCILIACION.DefaultIfEmpty().Where(x => x.CODIGO_HIST_CONCILIACION == codigoConciliacion).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_REVERSO_CONCILIACION>> Add(CNT_REVERSO_CONCILIACION entity)
        {

            ResultDto<CNT_REVERSO_CONCILIACION> result = new ResultDto<CNT_REVERSO_CONCILIACION>(null);
            try
            {
                await _context.CNT_REVERSO_CONCILIACION.AddAsync(entity);
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

        public async Task<ResultDto<CNT_REVERSO_CONCILIACION>> Update(CNT_REVERSO_CONCILIACION entity)
        {
            ResultDto<CNT_REVERSO_CONCILIACION> result = new ResultDto<CNT_REVERSO_CONCILIACION>(null);

            try
            {
                CNT_REVERSO_CONCILIACION entityUpdate = await GetByCodigo(entity.CODIGO_HIST_CONCILIACION);
                if (entityUpdate != null)
                {
                    _context.CNT_REVERSO_CONCILIACION.Update(entity);
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

        public async Task<string> Delete(int codigoHistConciliacion)
        {
            try
            {
                CNT_REVERSO_CONCILIACION entity = await GetByCodigo(codigoHistConciliacion);
                if (entity != null)
                {
                    _context.CNT_REVERSO_CONCILIACION.Remove(entity);
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
                var last = await _context.CNT_REVERSO_CONCILIACION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_HIST_CONCILIACION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_HIST_CONCILIACION + 1;
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
