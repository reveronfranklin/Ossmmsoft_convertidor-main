using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository
{
    public class ConceptosRetencionesRepository: IConceptosRetencionesRepository
    {

        private readonly DestinoDataContext _context;

        public ConceptosRetencionesRepository(DestinoDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ConceptosRetenciones entity)
        {
            try
            {
                _context.ConceptosRetenciones.Add(entity);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }
        public async Task<bool> Add(List<ConceptosRetenciones> entities)
        {
            try
            {
                await _context.ConceptosRetenciones.AddRangeAsync(entities);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task<List<ConceptosRetenciones>> GetAll()
        {

            try
            {
                var conceptosRetenciones = await _context.ConceptosRetenciones.ToListAsync();
                return conceptosRetenciones;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                throw;
            }
           
        }
        public async Task<ConceptosRetenciones> GetByKey(long codigoConcepto,string titulo)
        {
            return await _context.ConceptosRetenciones.Where(x => x.CODIGO_CONCEPTO == codigoConcepto && x.Titulo == titulo).FirstOrDefaultAsync();
        }
        public async Task<ConceptosRetenciones> GetByConcepto(long codigoConcepto)
        {
            return await _context.ConceptosRetenciones.Where(x => x.CODIGO_CONCEPTO == codigoConcepto ).FirstOrDefaultAsync();
        }
        public async Task DeleteAll()
        {
            List<ConceptosRetenciones> entities = await GetAll();

            if (entities != null)
            {
                _context.ConceptosRetenciones.RemoveRange(entities);
                _context.SaveChanges();
            }


        }

    }
}
