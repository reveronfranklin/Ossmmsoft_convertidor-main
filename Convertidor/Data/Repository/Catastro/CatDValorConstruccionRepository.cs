using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDValorConstruccionRepository : ICatDValorConstruccionRepository
    {
        private readonly DataContextCat _context;

        public CatDValorConstruccionRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_D_VALOR_CONSTRUCCION>> GetAll()
        {
            try
            {
                var result = await _context.CAT_D_VALOR_CONSTRUCCION.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CAT_D_VALOR_CONSTRUCCION> GetByCodigo(int codigoParcela)
        {
            try
            {

                var result = await _context.CAT_D_VALOR_CONSTRUCCION.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PARCELA == codigoParcela)
                    .FirstOrDefaultAsync();
                return (CAT_D_VALOR_CONSTRUCCION)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_D_VALOR_CONSTRUCCION>> Add(CAT_D_VALOR_CONSTRUCCION entity)
        {
            ResultDto<CAT_D_VALOR_CONSTRUCCION> result = new ResultDto<CAT_D_VALOR_CONSTRUCCION>(null);
            try
            {



                await _context.CAT_D_VALOR_CONSTRUCCION.AddAsync(entity);
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

        public async Task<ResultDto<CAT_D_VALOR_CONSTRUCCION>> Update(CAT_D_VALOR_CONSTRUCCION entity)
        {
            ResultDto<CAT_D_VALOR_CONSTRUCCION> result = new ResultDto<CAT_D_VALOR_CONSTRUCCION>(null);

            try
            {
                CAT_D_VALOR_CONSTRUCCION entityUpdate = await GetByCodigo(entity.CODIGO_PARCELA);
                if (entityUpdate != null)
                {


                    _context.CAT_D_VALOR_CONSTRUCCION.Update(entity);
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

        public async Task<string> Delete(int codigoParcela)
        {

            try
            {
                CAT_D_VALOR_CONSTRUCCION entity = await GetByCodigo(codigoParcela);
                if (entity != null)
                {
                    _context.CAT_D_VALOR_CONSTRUCCION.Remove(entity);
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
                var last = await _context.CAT_D_VALOR_CONSTRUCCION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PARCELA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PARCELA + 1;
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
