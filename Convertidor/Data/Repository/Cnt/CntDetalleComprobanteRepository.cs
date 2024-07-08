using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntDetalleComprobanteRepository : ICntDetalleComprobanteRepository
    {
        private readonly DataContextCnt _context;

        public CntDetalleComprobanteRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_DETALLE_COMPROBANTE>> GetAll()
        {
            try
            {
                var result = await _context.CNT_DETALLE_COMPROBANTE.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<CNT_DETALLE_COMPROBANTE>> GetByCodigoComprobante(int codigoComprobante)
        {
            try
            {
                var result = await _context.CNT_DETALLE_COMPROBANTE.DefaultIfEmpty().Where(x => x.CODIGO_COMPROBANTE == codigoComprobante).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_DETALLE_COMPROBANTE>> Add(CNT_DETALLE_COMPROBANTE entity)
        {

            ResultDto<CNT_DETALLE_COMPROBANTE> result = new ResultDto<CNT_DETALLE_COMPROBANTE>(null);
            try
            {
                await _context.CNT_DETALLE_COMPROBANTE.AddAsync(entity);
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

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.CNT_DETALLE_COMPROBANTE.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DETALLE_COMPROBANTE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DETALLE_COMPROBANTE + 1;
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
