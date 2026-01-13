using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntBancoArchivoControlRepository :ICntBancoArchivoControlRepository
    {
        private readonly DataContextCnt _context;

        public CntBancoArchivoControlRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_BANCO_ARCHIVO_CONTROL>> GetAll()
        {
            try
            {
                var result = await _context.CNT_BANCO_ARCHIVO_CONTROL.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<CNT_BANCO_ARCHIVO_CONTROL> GetByCodigo(int codigoBancoArchivoControl)
        {
            try
            {
                var result = await _context.CNT_BANCO_ARCHIVO_CONTROL
                    .Where(e => e.CODIGO_BANCO_ARCHIVO_CONTROL == codigoBancoArchivoControl).FirstOrDefaultAsync();

                return (CNT_BANCO_ARCHIVO_CONTROL)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<ResultDto<CNT_BANCO_ARCHIVO_CONTROL>> Add(CNT_BANCO_ARCHIVO_CONTROL entity)
        {

            ResultDto<CNT_BANCO_ARCHIVO_CONTROL> result = new ResultDto<CNT_BANCO_ARCHIVO_CONTROL>(null);
            try
            {
                await _context.CNT_BANCO_ARCHIVO_CONTROL.AddAsync(entity);
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

        public async Task<ResultDto<CNT_BANCO_ARCHIVO_CONTROL>> Update(CNT_BANCO_ARCHIVO_CONTROL entity)
        {
            ResultDto<CNT_BANCO_ARCHIVO_CONTROL> result = new ResultDto<CNT_BANCO_ARCHIVO_CONTROL>(null);

            try
            {
                CNT_BANCO_ARCHIVO_CONTROL entityUpdate = await GetByCodigo(entity.CODIGO_BANCO_ARCHIVO_CONTROL);
                if (entityUpdate != null)
                {
                    _context.CNT_BANCO_ARCHIVO_CONTROL.Update(entity);
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

        public async Task<string> Delete(int codigoBancoArchivoControl)
        {
            try
            {
                CNT_BANCO_ARCHIVO_CONTROL entity = await GetByCodigo(codigoBancoArchivoControl);
                if (entity != null)
                {
                    _context.CNT_BANCO_ARCHIVO_CONTROL.Remove(entity);
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
                var last = await _context.CNT_BANCO_ARCHIVO_CONTROL.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_BANCO_ARCHIVO_CONTROL)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_BANCO_ARCHIVO_CONTROL + 1;
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
