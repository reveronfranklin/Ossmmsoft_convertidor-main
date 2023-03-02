using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository
{
    public class HistoricoPersonalCargoRepository: IHistoricoPersonalCargoRepository
    {
        private readonly DestinoDataContext _context;

        public HistoricoPersonalCargoRepository(DestinoDataContext context)
        {
            _context = context;
        }


        public async Task<bool> Add(HistoricoPersonalCargo entity)
        {
            try
            {
                _context.HistoricoPersonalCargo.Add(entity);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task<bool> Add(List<HistoricoPersonalCargo> entities)
        {
            try
            {
                await _context.HistoricoPersonalCargo.AddRangeAsync(entities);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }

        public async Task<List<HistoricoPersonalCargo>> GetByLastDay(int days)
        {
            try
            {


                var fecha = DateTime.UtcNow.AddDays(days * -1);
                var result = await _context.HistoricoPersonalCargo.DefaultIfEmpty().Where(d => d.FECHA_NOMINA >= fecha).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }



        public async Task<HistoricoPersonalCargo?> GetByKey(long codigoEmpresa, long codigoPersona, long codigoTipoNomina, long codigoPeriodo)
        {


            return await _context.HistoricoPersonalCargo.Where(x => x.CODIGO_EMPRESA == codigoEmpresa && x.CODIGO_TIPO_NOMINA == codigoTipoNomina && x.CODIGO_PERIODO == codigoPeriodo && x.CODIGO_PERSONA == codigoPersona).FirstOrDefaultAsync();
        }



        public async Task Delete(int codigoEmpresa, int codigoPersona, int codigoTipoNomina, int codigoPeriodo)
        {
            HistoricoPersonalCargo? entity = await GetByKey(codigoEmpresa, codigoPersona, codigoTipoNomina, codigoPeriodo);
            if (entity != null)
            {
                _context.HistoricoPersonalCargo.Remove(entity);
                _context.SaveChanges();
            }


        }

        public async Task DeletePorDias(int days)
        {
            List<HistoricoPersonalCargo> entities = await GetByLastDay(days);

            if (entities != null)
            {
                _context.HistoricoPersonalCargo.RemoveRange(entities);
                _context.SaveChanges();
            }


        }


    }
}
