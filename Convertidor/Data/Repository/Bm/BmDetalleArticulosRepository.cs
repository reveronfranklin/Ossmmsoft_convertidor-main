using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
    public class BmDetalleArticulosRepository: IBmDetalleArticulosRepository
    {
		
        private readonly DataContextBm _context;

        public BmDetalleArticulosRepository(DataContextBm context)
        {
            _context = context;
        }
        public async Task<BM_DETALLE_ARTICULOS> GetByCodigoDetalleArticulo(int codigoDetalleArticulo)
        {
            try
            {
                var result = await _context.BM_DETALLE_ARTICULOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DETALLE_ARTICULO == codigoDetalleArticulo).FirstOrDefaultAsync();
        
                return (BM_DETALLE_ARTICULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<BM_DETALLE_ARTICULOS> GetByCodigoArticulo(int codigoArticulo)
        {
            try
            {
                var result = await _context.BM_DETALLE_ARTICULOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_ARTICULO == codigoArticulo).FirstOrDefaultAsync();

                return (BM_DETALLE_ARTICULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
    
        public async Task<List<BM_DETALLE_ARTICULOS>> GetAll()
        {
            try
            {
                var result = await _context.BM_DETALLE_ARTICULOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<BM_DETALLE_ARTICULOS>> Add(BM_DETALLE_ARTICULOS entity)
        {
            ResultDto<BM_DETALLE_ARTICULOS> result = new ResultDto<BM_DETALLE_ARTICULOS>(null);
            try
            {



                await _context.BM_DETALLE_ARTICULOS.AddAsync(entity);
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

        public async Task<ResultDto<BM_DETALLE_ARTICULOS>> Update(BM_DETALLE_ARTICULOS entity)
        {
            ResultDto<BM_DETALLE_ARTICULOS> result = new ResultDto<BM_DETALLE_ARTICULOS>(null);

            try
            {
                BM_DETALLE_ARTICULOS entityUpdate = await GetByCodigoDetalleArticulo(entity.CODIGO_DETALLE_ARTICULO);
                if (entityUpdate != null)
                {


                    _context.BM_DETALLE_ARTICULOS.Update(entity);
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

        public async Task<string> Delete(int codigoDetalleArticulo)
        {

            try
            {
                BM_DETALLE_ARTICULOS entity = await GetByCodigoDetalleArticulo(codigoDetalleArticulo);
                if (entity != null)
                {
                    _context.BM_DETALLE_ARTICULOS.Remove(entity);
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
                var last = await _context.BM_DETALLE_ARTICULOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DETALLE_ARTICULO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DETALLE_ARTICULO + 1;
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


