using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDocumentosLegalesRepository : ICatDocumentosLegalesRepository
    {
        private readonly DataContextCat _context;

        public CatDocumentosLegalesRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_DOCUMENTOS_LEGALES>> GetAll()
        {
            try
            {
                var result = await _context.CAT_DOCUMENTOS_LEGALES.DefaultIfEmpty().ToListAsync();

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
