using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntAuxiliaresPucRepository : ICntAuxiliaresPucRepository
    {
        private readonly DataContextCnt _context;

        public CntAuxiliaresPucRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_AUXILIARES_PUC>> GetAll()
        {
            try
            {
                var result = await _context.CNT_AUXILIARES_PUC.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_AUXILIARES_PUC> GetByCodigo(int codigoAuxiliarPuc)
        {
            try
            {
                var result = await _context.CNT_AUXILIARES_PUC.DefaultIfEmpty().Where(x => x.CODIGO_AUXILIAR_PUC == codigoAuxiliarPuc).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_AUXILIARES_PUC>> Add(CNT_AUXILIARES_PUC entity)
        {

            ResultDto<CNT_AUXILIARES_PUC> result = new ResultDto<CNT_AUXILIARES_PUC>(null);
            try
            {
                await _context.CNT_AUXILIARES_PUC.AddAsync(entity);
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

        public async Task<ResultDto<CNT_AUXILIARES_PUC>> Update(CNT_AUXILIARES_PUC entity)
        {
            ResultDto<CNT_AUXILIARES_PUC> result = new ResultDto<CNT_AUXILIARES_PUC>(null);

            try
            {
                CNT_AUXILIARES_PUC entityUpdate = await GetByCodigo(entity.CODIGO_AUXILIAR_PUC);
                if (entityUpdate != null)
                {
                    _context.CNT_AUXILIARES_PUC.Update(entity);
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
                var last = await _context.CNT_AUXILIARES_PUC.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_AUXILIAR_PUC)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_AUXILIAR_PUC + 1;
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
