using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class TmpDetalleComprobanteRepository : ITmpDetalleComprobanteRepository
    {
        private readonly DataContextCnt _context;

        public TmpDetalleComprobanteRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<TMP_DETALLE_COMPROBANTE>> GetAll()
        {
            try
            {
                var result = await _context.TMP_DETALLE_COMPROBANTE.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<TMP_DETALLE_COMPROBANTE> GetByCodigo(int codigoDetalleComprobante)
        {
            try
            {
                var result = await _context.TMP_DETALLE_COMPROBANTE.DefaultIfEmpty().Where(x => x.CODIGO_DETALLE_COMPROBANTE == codigoDetalleComprobante).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ResultDto<TMP_DETALLE_COMPROBANTE>> Add(TMP_DETALLE_COMPROBANTE entity)
        {

            ResultDto<TMP_DETALLE_COMPROBANTE> result = new ResultDto<TMP_DETALLE_COMPROBANTE>(null);
            try
            {
                await _context.TMP_DETALLE_COMPROBANTE.AddAsync(entity);
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

        public async Task<string> Delete(int codigoDetalleComprobante)
        {
            try
            {
                TMP_DETALLE_COMPROBANTE entity = await GetByCodigo(codigoDetalleComprobante);
                if (entity != null)
                {
                    _context.TMP_DETALLE_COMPROBANTE.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<ResultDto<TMP_DETALLE_COMPROBANTE>> Update(TMP_DETALLE_COMPROBANTE entity)
        {
            ResultDto<TMP_DETALLE_COMPROBANTE> result = new ResultDto<TMP_DETALLE_COMPROBANTE>(null);

            try
            {
                TMP_DETALLE_COMPROBANTE entityUpdate = await GetByCodigo(entity.CODIGO_DETALLE_COMPROBANTE);
                if (entityUpdate != null)
                {
                    _context.TMP_DETALLE_COMPROBANTE.Update(entity);
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
                var last = await _context.TMP_DETALLE_COMPROBANTE.DefaultIfEmpty()
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
