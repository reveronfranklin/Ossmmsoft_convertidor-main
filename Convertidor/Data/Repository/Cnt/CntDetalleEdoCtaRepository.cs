using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntDetalleEdoCtaRepository : ICntDetalleEdoCtaRepository
    {
        private readonly DataContextCnt _context;

        public CntDetalleEdoCtaRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_DETALLE_EDO_CTA>> GetAll() 
        {
            try 
            {
            
                var result = await _context.CNT_DETALLE_EDO_CTA.DefaultIfEmpty().ToListAsync();
                return result;

            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;

            }
        
        }

        public async Task<CNT_DETALLE_EDO_CTA> GetByCodigo(int codigoDetalleEdoCuenta)
        {
            try
            {
                var result = await _context.CNT_DETALLE_EDO_CTA.DefaultIfEmpty().Where(x => x.CODIGO_DETALLE_EDO_CTA == codigoDetalleEdoCuenta).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<CNT_DETALLE_EDO_CTA>> GetByCodigoEstadoCuenta(int codigoEstadoCuenta)
        {
            try
            {
                var result = await _context.CNT_DETALLE_EDO_CTA.DefaultIfEmpty().Where(x => x.CODIGO_ESTADO_CUENTA == codigoEstadoCuenta).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_DETALLE_EDO_CTA>> Add(CNT_DETALLE_EDO_CTA entity)
        {

            ResultDto<CNT_DETALLE_EDO_CTA> result = new ResultDto<CNT_DETALLE_EDO_CTA>(null);
            try
            {
                await _context.CNT_DETALLE_EDO_CTA.AddAsync(entity);
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

        public async Task<ResultDto<CNT_DETALLE_EDO_CTA>> Update(CNT_DETALLE_EDO_CTA entity)
        {
            ResultDto<CNT_DETALLE_EDO_CTA> result = new ResultDto<CNT_DETALLE_EDO_CTA>(null);

            try
            {
                CNT_DETALLE_EDO_CTA entityUpdate = await GetByCodigo(entity.CODIGO_DETALLE_EDO_CTA);
                if (entityUpdate != null)
                {
                    _context.CNT_DETALLE_EDO_CTA.Update(entity);
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
                var last = await _context.CNT_DETALLE_EDO_CTA.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DETALLE_EDO_CTA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DETALLE_EDO_CTA + 1;
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
