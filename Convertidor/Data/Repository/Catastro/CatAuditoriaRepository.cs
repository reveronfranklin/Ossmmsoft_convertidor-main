using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatAuditoriaRepository : ICatAuditoriaRepository
    {
        private readonly DataContextCat _context;

        public CatAuditoriaRepository(DataContextCat context) 
        {
           _context = context;
        }

        public async Task<List<CAT_AUDITORIA>> GetAll()
        {
            try
            {
                var result = await _context.CAT_AUDITORIA.DefaultIfEmpty().ToListAsync();

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
