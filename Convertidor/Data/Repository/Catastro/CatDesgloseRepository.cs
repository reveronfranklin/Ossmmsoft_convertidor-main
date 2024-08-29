using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDesgloseRepository : ICatDesgloseRepository
    {
        private readonly DataContextCat _context;

        public CatDesgloseRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_DESGLOSE>> GetAll()
        {
            try
            {
                var result = await _context.CAT_DESGLOSE.DefaultIfEmpty().ToListAsync();

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
