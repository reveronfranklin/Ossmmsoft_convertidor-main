using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhVReciboPagoRepository: IRhVReciboPagoRepository
    {
        private readonly DataContext _context;

        public RhVReciboPagoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<RH_V_RECIBO_PAGO>> GetAll()
        {
            try
            {

                var result = await _context.RH_V_RECIBO_PAGO.DefaultIfEmpty().ToListAsync();
                return (List<RH_V_RECIBO_PAGO>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<RH_V_RECIBO_PAGO> GetByCodigoTipoNomina(int codigoTipoNomina, int codigoPersona)
        {
            try
            {

                var result = await _context.RH_V_RECIBO_PAGO.DefaultIfEmpty()
                .Where(c=>c.CODIGO_TIPO_NOMINA == codigoTipoNomina && c.CODIGO_PERSONA == codigoPersona)
                .OrderBy(x=>x.CODIGO_PERIODO).FirstOrDefaultAsync();
                return (RH_V_RECIBO_PAGO)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_V_RECIBO_PAGO>> GeTByFilter(int codigoPeriodo, int codigoTipoNomina, int codigoPersona)
        {
            try
            {

                var result = await _context.RH_V_RECIBO_PAGO.DefaultIfEmpty()
                    .Where(tn => tn.CODIGO_PERIODO == codigoPeriodo && tn.CODIGO_TIPO_NOMINA == codigoTipoNomina && tn.CODIGO_PERSONA == codigoPersona)
                    .OrderBy(x => x.CODIGO_TIPO_NOMINA).ToListAsync();
                return (List<RH_V_RECIBO_PAGO>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
    }
}
