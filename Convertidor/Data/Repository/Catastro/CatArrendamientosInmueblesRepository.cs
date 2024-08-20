using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatArrendamientosInmueblesRepository : ICatArrendamientosInmueblesRepository
    {
        private readonly DataContextCat _context;

        public CatArrendamientosInmueblesRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_ARRENDAMIENTOS_INMUEBLES>> GetAll()
        {
            try
            {
                var result = await _context.CAT_ARRENDAMIENTOS_INMUEBLES.DefaultIfEmpty().ToListAsync();

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
