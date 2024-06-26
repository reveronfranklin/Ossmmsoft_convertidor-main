using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntEstadoCuentasRepository : ICntEstadoCuentasRepository
    {
        private readonly DataContextCnt _context;

        public CntEstadoCuentasRepository(DataContextCnt context)
        {
            _context = context;
        }


        public async Task<List<CNT_ESTADO_CUENTAS>> GetAll() 
        {
            try
            {
                var result = await _context.CNT_ESTADO_CUENTAS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_ESTADO_CUENTAS> GetByCodigo(int codigoEstadoCuenta)
        {
            try
            {
                var result = await _context.CNT_ESTADO_CUENTAS.DefaultIfEmpty().Where(x => x.CODIGO_ESTADO_CUENTA == codigoEstadoCuenta).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_ESTADO_CUENTAS>> Add(CNT_ESTADO_CUENTAS entity)
        {

            ResultDto<CNT_ESTADO_CUENTAS> result = new ResultDto<CNT_ESTADO_CUENTAS>(null);
            try
            {
                await _context.CNT_ESTADO_CUENTAS.AddAsync(entity);
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

        public async Task<ResultDto<CNT_ESTADO_CUENTAS>> Update(CNT_ESTADO_CUENTAS entity)
        {
            ResultDto<CNT_ESTADO_CUENTAS> result = new ResultDto<CNT_ESTADO_CUENTAS>(null);

            try
            {
                CNT_ESTADO_CUENTAS entityUpdate = await GetByCodigo(entity.CODIGO_ESTADO_CUENTA);
                if (entityUpdate != null)
                {
                    _context.CNT_ESTADO_CUENTAS.Update(entity);
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

        public async Task<string> Delete(int codigoEstadoCuenta)
        {
            try
            {
                CNT_ESTADO_CUENTAS entity = await GetByCodigo(codigoEstadoCuenta);
                if (entity != null)
                {
                    _context.CNT_ESTADO_CUENTAS.Remove(entity);
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
                var last = await _context.CNT_ESTADO_CUENTAS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ESTADO_CUENTA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ESTADO_CUENTA + 1;
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

