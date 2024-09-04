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
    }
}
