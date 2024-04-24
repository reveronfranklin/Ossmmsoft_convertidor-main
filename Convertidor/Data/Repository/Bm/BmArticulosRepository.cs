using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
    public class BmArticulosRepository: IBmArticulosRepository
    {
		
        private readonly DataContextBm _context;

        public BmArticulosRepository(DataContextBm context)
        {
            _context = context;
        }
        public async Task<BM_ARTICULOS> GetByCodigoArticulo(int codigoArticulo)
        {
            try
            {
                var result = await _context.BM_ARTICULOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_ARTICULO == codigoArticulo).FirstOrDefaultAsync();
        
                return (BM_ARTICULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<BM_ARTICULOS> GetByCodigo(string codigo)
        {
            try
            {
                var result = await _context.BM_ARTICULOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO == codigo).FirstOrDefaultAsync();

                return (BM_ARTICULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
       
        public async Task<List<BM_ARTICULOS>> GetAll()
        {
            try
            {
                var result = await _context.BM_ARTICULOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<BM_ARTICULOS>> Add(BM_ARTICULOS entity)
        {
            ResultDto<BM_ARTICULOS> result = new ResultDto<BM_ARTICULOS>(null);
            try
            {



                await _context.BM_ARTICULOS.AddAsync(entity);
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

        public async Task<ResultDto<BM_ARTICULOS>> Update(BM_ARTICULOS entity)
        {
            ResultDto<BM_ARTICULOS> result = new ResultDto<BM_ARTICULOS>(null);

            try
            {
                BM_ARTICULOS entityUpdate = await GetByCodigoArticulo(entity.CODIGO_ARTICULO);
                if (entityUpdate != null)
                {


                    _context.BM_ARTICULOS.Update(entity);
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

        public async Task<string> Delete(int codigoArticulo)
        {

            try
            {
                BM_ARTICULOS entity = await GetByCodigoArticulo(codigoArticulo);
                if (entity != null)
                {
                    _context.BM_ARTICULOS.Remove(entity);
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
                var last = await _context.BM_ARTICULOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ARTICULO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ARTICULO + 1;
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

