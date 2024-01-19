using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository
{
    public class IndiceCategoriaProgramaRepository: IIndiceCategoriaProgramaRepository
    {

        private readonly DestinoDataContext _context;

        public IndiceCategoriaProgramaRepository(DestinoDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(IndiceCategoriaPrograma entity)
        {
            try
            {
                _context.IndiceCategoriaPrograma.Add(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task<bool> Add(List<IndiceCategoriaPrograma> entities)
        {
            try
            {
                await _context.IndiceCategoriaPrograma.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task<List<IndiceCategoriaPrograma>> GetByLastDay(int days)
        {
            try
            {


                var fecha = DateTime.UtcNow.AddDays(days * -1);
                var result = await _context.IndiceCategoriaPrograma.DefaultIfEmpty().Where(d => d.FECHA_INS >= fecha || d.FECHA_UPD>= fecha).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }
        public async Task<List<IndiceCategoriaPrograma>> GetAll()
        {
            try
            {


               
                var result = await _context.IndiceCategoriaPrograma.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }



        public async Task<IndiceCategoriaPrograma> GetByKey(long key)
        {
            return await _context.IndiceCategoriaPrograma.Where(x => x.CODIGO_ICP == key).FirstOrDefaultAsync();
        }



        public async Task Delete(long id)
        {
            IndiceCategoriaPrograma entity = await GetByKey(id);
            if (entity != null)
            {
                _context.IndiceCategoriaPrograma.Remove(entity);
                await _context.SaveChangesAsync();
            }


        }

        public async Task DeletePorDias(int days)
        {
            List<IndiceCategoriaPrograma> entities = await GetByLastDay(days);

            if (entities != null)
            {
                _context.IndiceCategoriaPrograma.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }


        }
        public async Task DeleteAll()
        {
            List<IndiceCategoriaPrograma> entities = await GetAll();

            if (entities != null && entities.Count>1)
            {
                _context.IndiceCategoriaPrograma.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }


        }


    }
}
