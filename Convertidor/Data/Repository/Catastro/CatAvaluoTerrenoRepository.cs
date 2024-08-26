using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatAvaluoTerrenoRepository : ICatAvaluoTerrenoRepository
    {
        private readonly DataContextCat _context;

        public CatAvaluoTerrenoRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_AVALUO_TERRENO>> GetAll()
        {
            try
            {
                var result = await _context.CAT_AVALUO_TERRENO.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<CAT_AVALUO_TERRENO> GetByCodigo(int codigoAvaluoTerreno)
        {
            try
            {

                var result = await _context.CAT_AVALUO_TERRENO.DefaultIfEmpty()
                    .Where(x => x.CODIGO_AVALUO_TERRENO == codigoAvaluoTerreno)
                    .FirstOrDefaultAsync();
                return (CAT_AVALUO_TERRENO)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_AVALUO_TERRENO>> Update(CAT_AVALUO_TERRENO entity)
        {
            ResultDto<CAT_AVALUO_TERRENO> result = new ResultDto<CAT_AVALUO_TERRENO>(null);

            try
            {
                CAT_AVALUO_TERRENO entityUpdate = await GetByCodigo(entity.CODIGO_AVALUO_TERRENO);
                if (entityUpdate != null)
                {


                    _context.CAT_AVALUO_TERRENO.Update(entity);
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

        public async Task<string> Delete(int codigoAvaluoTerreno)
        {

            try
            {
                CAT_AVALUO_TERRENO entity = await GetByCodigo(codigoAvaluoTerreno);
                if (entity != null)
                {
                    _context.CAT_AVALUO_TERRENO.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
        public async Task<ResultDto<CAT_AVALUO_TERRENO>> Add(CAT_AVALUO_TERRENO entity)
        {
            ResultDto<CAT_AVALUO_TERRENO> result = new ResultDto<CAT_AVALUO_TERRENO>(null);
            try
            {



                await _context.CAT_AVALUO_TERRENO.AddAsync(entity);
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
                var last = await _context.CAT_AVALUO_TERRENO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_AVALUO_TERRENO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_AVALUO_TERRENO + 1;
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
