using Convertidor.Data.Entities;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository
{
    public class RH_HISTORICO_PERSONAL_CARGORepository: IRH_HISTORICO_PERSONAL_CARGORepository
    {

        private readonly DataContext _context;

        public RH_HISTORICO_PERSONAL_CARGORepository(DataContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<RH_HISTORICO_PERSONAL_CARGO>> GetByLastDay(int days)
        {
            try
            {
                var fecha = DateTime.Now.AddDays(days * -1);
                var result = await _context.RH_HISTORICO_PERSONAL_CARGO.DefaultIfEmpty().Where(h => h.FECHA_NOMINA >= fecha).ToListAsync();
                return (IEnumerable<RH_HISTORICO_PERSONAL_CARGO>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

      

    }
}
