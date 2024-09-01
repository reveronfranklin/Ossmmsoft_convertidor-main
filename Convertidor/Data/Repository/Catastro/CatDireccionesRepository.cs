using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDireccionesRepository : ICatDireccionesRepository
    {
        private readonly DataContextCat _context;

        public CatDireccionesRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_DIRECCIONES>> GetAll()
        {
            try
            {
                var result = await _context.CAT_DIRECCIONES.DefaultIfEmpty().ToListAsync();

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
