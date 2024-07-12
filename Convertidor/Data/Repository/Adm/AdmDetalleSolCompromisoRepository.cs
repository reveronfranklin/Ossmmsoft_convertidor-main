using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmDetalleSolCompromisoRepository : IAdmDetalleSolCompromisoRepository
    {
        private readonly DataContextAdm _context;

        public AdmDetalleSolCompromisoRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<List<ADM_DETALLE_SOL_COMPROMISO>> GetAll()
        {
            try
            {
                var result = await _context.ADM_DETALLE_SOL_COMPROMISO.DefaultIfEmpty().ToListAsync();
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
