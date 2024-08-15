using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatTitulosRepository : ICatTitulosRepository
    {
        private readonly DataContextCat _context;

        public CatTitulosRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_TITULOS>> GetAll()
        {
            try
            {
                var result = await _context.CAT_TITULOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CAT_TITULOS> GetByCodigo(int tituloId)
        {
            try
            {
                var result = await _context.CAT_TITULOS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).FirstOrDefaultAsync();

                return (CAT_TITULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<CAT_TITULOS> GetByCodigoString(string codigoTitulo)
        {
            try
            {
                var result = await _context.CAT_TITULOS.DefaultIfEmpty().Where(e => e.CODIGO_TITULO.Trim() == codigoTitulo).FirstOrDefaultAsync();

                return (CAT_TITULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_TITULOS>> Add(CAT_TITULOS entity)
        {
            ResultDto<CAT_TITULOS> result = new ResultDto<CAT_TITULOS>(null);
            try
            {



                await _context.CAT_TITULOS.AddAsync(entity);
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

        public async Task<ResultDto<CAT_TITULOS>> Update(CAT_TITULOS entity)
        {
            ResultDto<CAT_TITULOS> result = new ResultDto<CAT_TITULOS>(null);

            try
            {
                CAT_TITULOS entityUpdate = await GetByCodigo(entity.TITULO_ID);
                if (entityUpdate != null)
                {


                    _context.CAT_TITULOS.Update(entity);
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
                var last = await _context.CAT_TITULOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.TITULO_ID)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.TITULO_ID + 1;
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
