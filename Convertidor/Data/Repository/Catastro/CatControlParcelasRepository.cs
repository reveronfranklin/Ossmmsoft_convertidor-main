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
    }
}
