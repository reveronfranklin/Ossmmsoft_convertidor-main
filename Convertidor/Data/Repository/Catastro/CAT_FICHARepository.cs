using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
	public class CAT_FICHARepository: ICAT_FICHARepository
    {
		

        private readonly DataContextCat _context;

        public CAT_FICHARepository(DataContextCat context)
        {
            _context = context;
        }

     


        public async Task<List<CAT_FICHA>> Get()
        {
            try
            {
                var result = await _context.CAT_FICHA.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<bool> Add(CAT_FICHA entity)
        {
            try
            {
                _context.CAT_FICHA.Add(entity);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task Update(CAT_FICHA entity)
        {
            CAT_FICHA entityExist = await GetByKey(entity.CODIGO_FICHA);
            if (entityExist != null)
            {
                _context.CAT_FICHA.Update(entity);
                _context.SaveChanges();
            }

        }


        public async Task<int> GetNext()
        {
            try
            {

                int result = 0;


                var catFicha = await _context.CAT_FICHA.DefaultIfEmpty().OrderByDescending(c=> c.CODIGO_FICHA).FirstOrDefaultAsync();
                if (catFicha!=null ) {
                    result = catFicha.CODIGO_FICHA + 1;

                }
                else
                {
                    result = 1;
                }
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return 0;
            }
        }


      



        public async Task<CAT_FICHA> GetByKey(int codigoFicha)
        {
            var result = await _context.CAT_FICHA.Where(x => x.CODIGO_FICHA == codigoFicha).FirstOrDefaultAsync();
            return result;
        }



        public async Task Delete(int id)
        {
            CAT_FICHA entity = await GetByKey(id);
            if (entity != null)
            {
                _context.CAT_FICHA.Remove(entity);
                _context.SaveChanges();
            }


        }

      
    }
}

