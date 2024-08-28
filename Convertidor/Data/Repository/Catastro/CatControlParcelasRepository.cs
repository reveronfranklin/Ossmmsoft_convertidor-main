using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatControlParcelasRepository : ICatControlParcelasRepository
    {
        private readonly DataContextCat _context;

        public CatControlParcelasRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_CONTROL_PARCELAS>> GetAll()
        {
            try
            {
                var result = await _context.CAT_CONTROL_PARCELAS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_CONTROL_PARCELAS>> Add(CAT_CONTROL_PARCELAS entity)
        {
            ResultDto<CAT_CONTROL_PARCELAS> result = new ResultDto<CAT_CONTROL_PARCELAS>(null);
            try
            {



                await _context.CAT_CONTROL_PARCELAS.AddAsync(entity);
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
                var last = await _context.CAT_CONTROL_PARCELAS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CONTROL_PARCELA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CONTROL_PARCELA + 1;
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
