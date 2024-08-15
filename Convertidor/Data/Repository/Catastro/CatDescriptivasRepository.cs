using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDescriptivasRepository : ICatDescriptivasRepository
    {
        private readonly DataContextCat _context;

        public CatDescriptivasRepository(DataContextCat context)
        {
            _context = context;
        }

   
        public async Task<List<CAT_DESCRIPTIVAS>> GetByTitulo(int tituloId)
        {
            try
            {
                var result = await _context.CAT_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).ToListAsync();

                return (List<CAT_DESCRIPTIVAS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


    }
}
