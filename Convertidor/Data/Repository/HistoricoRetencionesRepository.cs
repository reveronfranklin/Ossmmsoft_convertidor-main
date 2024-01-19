using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository
{
    public class HistoricoRetencionesRepository: IHistoricoRetencionesRepository
    {

        private readonly DestinoDataContext _context;

        public HistoricoRetencionesRepository(DestinoDataContext context)
        {
            _context = context;
        }

     
        public async Task<bool> Add(HistoricoRetenciones entity)
        {
            try
            {
                _context.HistoricoRetenciones.Add(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task<bool> Add(List<HistoricoRetenciones> entities)
        {
            try
            {
                await _context.HistoricoRetenciones.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task<List<HistoricoRetenciones>> GetByLastDay(int days)
        {
            try
            {


                var fecha = DateTime.UtcNow.AddDays(days * -1);
                var result = await _context.HistoricoRetenciones.DefaultIfEmpty().Where(d => d.FECHA_INS >= fecha).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }



        public async Task<HistoricoRetenciones> GetByKey(long key)
        {
            return await _context.HistoricoRetenciones.Where(x => x.CODIGO_HISTORICO_NOMINA == key).FirstOrDefaultAsync();
        }



        public async Task Delete(long id)
        {
            HistoricoRetenciones entity = await GetByKey(id);
            if (entity != null)
            {
                _context.HistoricoRetenciones.Remove(entity);
                await _context.SaveChangesAsync();
            }


        }

        public async Task DeletePorDias(int days)
        {
            List<HistoricoRetenciones> entities = await GetByLastDay(days);

            if (entities != null)
            {
                _context.HistoricoRetenciones.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }


        }

    }
}
