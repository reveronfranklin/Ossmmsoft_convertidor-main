using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntDetalleLibroRepository : ICntDetalleLibroRepository
    {
        private readonly DataContextCnt _context;

        public CntDetalleLibroRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_DETALLE_LIBRO>> GetAll() 
        {
            try 
            {
            
                var result = await _context.CNT_DETALLE_LIBRO.DefaultIfEmpty().ToListAsync();
                return result;

            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;

            }
        
        }

        public async Task<List<CNT_DETALLE_LIBRO>> GetByCodigoLibro(int codigoLibro)
        {
            try
            {
                var result = await _context.CNT_DETALLE_LIBRO.DefaultIfEmpty().Where(x => x.CODIGO_LIBRO == codigoLibro).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_DETALLE_LIBRO> GetByCodigo(int codigoDetalleLibro)
        {
            try
            {
                var result = await _context.CNT_DETALLE_LIBRO.DefaultIfEmpty().Where(x => x.CODIGO_DETALLE_LIBRO == codigoDetalleLibro).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_DETALLE_LIBRO>> Add(CNT_DETALLE_LIBRO entity)
        {

            ResultDto<CNT_DETALLE_LIBRO> result = new ResultDto<CNT_DETALLE_LIBRO>(null);
            try
            {
                await _context.CNT_DETALLE_LIBRO.AddAsync(entity);
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

        public async Task<ResultDto<CNT_DETALLE_LIBRO>> Update(CNT_DETALLE_LIBRO entity)
        {
            ResultDto<CNT_DETALLE_LIBRO> result = new ResultDto<CNT_DETALLE_LIBRO>(null);

            try
            {
                CNT_DETALLE_LIBRO entityUpdate = await GetByCodigo(entity.CODIGO_DETALLE_LIBRO);
                if (entityUpdate != null)
                {
                    _context.CNT_DETALLE_LIBRO.Update(entity);
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

        public async Task<string> Delete(int codigoDetalleLibro)
        {
            try
            {
                CNT_DETALLE_LIBRO entity = await GetByCodigo(codigoDetalleLibro);
                if (entity != null)
                {
                    _context.CNT_DETALLE_LIBRO.Remove(entity);
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
                var last = await _context.CNT_DETALLE_LIBRO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DETALLE_LIBRO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DETALLE_LIBRO + 1;
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
