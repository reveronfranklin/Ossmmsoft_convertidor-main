using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatAforosInmueblesRepository : ICatAforosInmueblesRepository
    {
        private readonly DataContextCat _context;

        public CatAforosInmueblesRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_AFOROS_INMUEBLES>> GetAll()
        {
            try
            {
                var result = await _context.CAT_AFOROS_INMUEBLES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
    }
}
