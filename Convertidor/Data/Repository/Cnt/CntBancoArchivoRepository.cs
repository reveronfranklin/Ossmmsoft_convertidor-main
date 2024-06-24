using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntBancoArchivoRepository : ICntBancoArchivoRepository
    {
        private readonly DataContextCnt _context;

        public CntBancoArchivoRepository(DataContextCnt context)
        {
            _context = context;
        }


        public async Task<List<CNT_BANCO_ARCHIVO>> GetAll() 
        {
            try
            {
                var result = await _context.CNT_BANCO_ARCHIVO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<CNT_BANCO_ARCHIVO> GetByCodigo(int codigoBancoArchivo)
        {
            try
            {
                var result = await _context.CNT_BANCO_ARCHIVO
                    .Where(e => e.CODIGO_BANCO_ARCHIVO == codigoBancoArchivo).FirstOrDefaultAsync();

                return (CNT_BANCO_ARCHIVO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_BANCO_ARCHIVO>> Add(CNT_BANCO_ARCHIVO entity)
        {

            ResultDto<CNT_BANCO_ARCHIVO> result = new ResultDto<CNT_BANCO_ARCHIVO>(null);
            try
            {
                await _context.CNT_BANCO_ARCHIVO.AddAsync(entity);
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

        public async Task<ResultDto<CNT_BANCO_ARCHIVO>> Update(CNT_BANCO_ARCHIVO entity)
        {
            ResultDto<CNT_BANCO_ARCHIVO> result = new ResultDto<CNT_BANCO_ARCHIVO>(null);

            try
            {
                CNT_BANCO_ARCHIVO entityUpdate = await GetByCodigo(entity.CODIGO_BANCO_ARCHIVO);
                if (entityUpdate != null)
                {
                    _context.CNT_BANCO_ARCHIVO.Update(entity);
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

        public async Task<string> Delete(int codigoBancoArchivo)
        {
            try
            {
                CNT_BANCO_ARCHIVO entity = await GetByCodigo(codigoBancoArchivo);
                if (entity != null)
                {
                    _context.CNT_BANCO_ARCHIVO.Remove(entity);
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
                var last = await _context.CNT_BANCO_ARCHIVO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_BANCO_ARCHIVO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_BANCO_ARCHIVO + 1;
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
