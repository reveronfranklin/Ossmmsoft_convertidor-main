
using Convertidor.Data.Entities;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository
{
    public class RH_HISTORICO_NOMINARepository: IRH_HISTORICO_NOMINARepository
    {

      
        private readonly DataContext _context;

        public RH_HISTORICO_NOMINARepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RH_HISTORICO_NOMINA>> Get()
        {
            try
            {
                var result = await _context.RH_HISTORICO_NOMINA.DefaultIfEmpty().Where(h => h.CODIGO_PERSONA == 1388 && h.CODIGO_PERIODO == 3803).ToListAsync();
                return (IEnumerable<RH_HISTORICO_NOMINA>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<IEnumerable<RH_HISTORICO_NOMINA>> GetByLastDay(int days)
        {
            try
            {
                var fecha = DateTime.Now.AddDays(days*-1);
                var result = await _context.RH_HISTORICO_NOMINA.DefaultIfEmpty().Where(h => h.FECHA_NOMINA >= fecha).ToListAsync();
                return (IEnumerable<RH_HISTORICO_NOMINA>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }


        public async Task<List<RH_HISTORICO_NOMINA>> GetByPeriodo(int codigoPeriodo, int tipoNomina)
        {
            try
            {



                var result = await _context.RH_HISTORICO_NOMINA.DefaultIfEmpty().Where(d => d.CODIGO_PERIODO == codigoPeriodo && d.CODIGO_TIPO_NOMINA == tipoNomina).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }
        }

    }
}
