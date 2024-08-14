using Convertidor.Data.Entities.Catastro;
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
    }
}
