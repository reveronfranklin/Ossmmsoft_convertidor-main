
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository
{
    public class HistoricoNominaRepository : IHistoricoNominaRepository
    {

      
        private readonly DestinoDataContext _context;

        public HistoricoNominaRepository(DestinoDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistoricoNomina>> Get()
        {
            try
            {
                var result = await _context.HistoricoNomina.DefaultIfEmpty().Where(h => h.CODIGO_PERSONA == 1388 && h.CODIGO_PERIODO == 3803).ToListAsync();
                return (IEnumerable<HistoricoNomina>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<bool> Add(HistoricoNomina entity)
        {
            try
            {
                _context.HistoricoNomina.Add(entity);
                _context.SaveChanges();
               
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task<bool> Add(List<HistoricoNomina> entities)
        {
            try
            {
                await _context.HistoricoNomina.AddRangeAsync(entities);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task<List<HistoricoNomina>> GetByLastDay(int days)
        {
            try
            {

               
                var fecha = DateTime.UtcNow.AddDays(days*-1);
                var result = await _context.HistoricoNomina.DefaultIfEmpty().Where(d=> d.FECHA_NOMINA>= fecha).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }
        }


        public async Task<List<HistoricoNomina>> GetByPeriodo(int codigoPeriodo,int tipoNomina)
        {
            try
            {



                var result = await _context.HistoricoNomina.DefaultIfEmpty().Where(d => d.CODIGO_PERIODO==codigoPeriodo && d.CODIGO_TIPO_NOMINA == tipoNomina ).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<List<HistoricoNomina>> GetByLastDayWithRelation(int days)
        {
            try
            {


                var fecha = DateTime.UtcNow.AddDays(days * -1);
                var result = await _context.HistoricoNomina.DefaultIfEmpty().Include(a => a.Codigo).ThenInclude(b=>b.IndiceCategoriaPrograma).Where(d => d.FECHA_NOMINA >= fecha).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }
        }


        public async Task<HistoricoNomina> GetByKey(long key)
        {
            return await _context.HistoricoNomina.Where(x => x.CODIGO_HISTORICO_NOMINA == key).FirstOrDefaultAsync();
        }

        

        public async Task Delete(long id)
        {
            HistoricoNomina entity = await GetByKey(id);
            if (entity != null)
            {
                _context.HistoricoNomina.Remove(entity);
                _context.SaveChanges();
            }
            

        }

        public async Task DeletePorDias(int days)
        {
           List<HistoricoNomina> entities =await GetByLastDay(days);
            
            if (entities != null)
            {
                _context.HistoricoNomina.RemoveRange(entities);
                _context.SaveChanges();
            }


        }


    }
}
