using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatAvaluoConstruccionRepository : ICatAvaluoConstruccionRepository
    {
        private readonly DataContextCat _context;

        public CatAvaluoConstruccionRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_AVALUO_CONSTRUCCION>> GetAll()
        {
            try
            {
                var result = await _context.CAT_AVALUO_CONSTRUCCION.DefaultIfEmpty().ToListAsync();

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
