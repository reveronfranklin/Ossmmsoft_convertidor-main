using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDescriptivasRepository : ICatDescriptivasRepository
    {
        private readonly DataContextCat _context;

        public CatDescriptivasRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_DESCRIPTIVAS>> GetAll()
        {
            try
            {
                var result = await _context.CAT_DESCRIPTIVAS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<CAT_DESCRIPTIVAS>> GetByTitulo(int tituloId)
        {
            try
            {
                var result = await _context.CAT_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).ToListAsync();

                return (List<CAT_DESCRIPTIVAS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<CAT_DESCRIPTIVAS>> GetByFKID(int descripcionIdFk)
        {
            try
            {

                var result = await _context.CAT_DESCRIPTIVAS.DefaultIfEmpty()
                    .Where(x => x.DESCRIPCION_FK_ID == descripcionIdFk)
                    .ToListAsync();
                return (List<CAT_DESCRIPTIVAS>)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }

        public async Task<CAT_DESCRIPTIVAS> GetByCodigo(int descripcionId)
        {
            try
            {

                var result = await _context.CAT_DESCRIPTIVAS.DefaultIfEmpty()
                    .Where(x => x.DESCRIPCION_ID == descripcionId)
                    .FirstOrDefaultAsync();
                return (CAT_DESCRIPTIVAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

        }

        public async Task<CAT_DESCRIPTIVAS> GetByCodigoDescriptivaTexto(string codigo)
        {
            try
            {
                var result = await _context.CAT_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.CODIGO == codigo).FirstOrDefaultAsync();

                return (CAT_DESCRIPTIVAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<bool> GetByIdAndTitulo(int tituloId, int id)
        {
            try
            {
                var descriptiva = await _context.CAT_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId && e.DESCRIPCION_ID == id).FirstOrDefaultAsync();
                var result = false;
                if (descriptiva != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return false;
            }

        }

        public async Task<ResultDto<CAT_DESCRIPTIVAS>> Add(CAT_DESCRIPTIVAS entity)
        {
            ResultDto<CAT_DESCRIPTIVAS> result = new ResultDto<CAT_DESCRIPTIVAS>(null);
            try
            {



                await _context.CAT_DESCRIPTIVAS.AddAsync(entity);
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

        public async Task<ResultDto<CAT_DESCRIPTIVAS>> Update(CAT_DESCRIPTIVAS entity)
        {
            ResultDto<CAT_DESCRIPTIVAS> result = new ResultDto<CAT_DESCRIPTIVAS>(null);

            try
            {
                CAT_DESCRIPTIVAS entityUpdate = await GetByCodigo(entity.DESCRIPCION_ID);
                if (entityUpdate != null)
                {


                    _context.CAT_DESCRIPTIVAS.Update(entity);
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

        public async Task<string> Delete(int descripcionId)
        {

            try
            {
                CAT_DESCRIPTIVAS entity = await GetByCodigo(descripcionId);
                if (entity != null)
                {
                    _context.CAT_DESCRIPTIVAS.Remove(entity);
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
                var last = await _context.CAT_DESCRIPTIVAS.DefaultIfEmpty()
                    .OrderByDescending(x => x.DESCRIPCION_ID)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.DESCRIPCION_ID + 1;
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
