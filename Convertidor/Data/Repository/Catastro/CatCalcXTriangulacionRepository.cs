using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatCalcXTriangulacionRepository : ICatCalcXTriangulacionRepository
    {
        private readonly DataContextCat _context;

        public CatCalcXTriangulacionRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_CALC_X_TRIANGULACION>> GetAll()
        {
            try
            {
                var result = await _context.CAT_CALC_X_TRIANGULACION.DefaultIfEmpty().ToListAsync();

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
