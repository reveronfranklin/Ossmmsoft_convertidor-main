using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhReporteNominaRepository : IRhReporteNominaHistoricoRepository
    {
		
        private readonly DataContext _context;

        public RhReporteNominaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_V_REPORTE_NOMINA_HISTORICO>> GetByPeriodoTipoNomina(int codigoPeriodo,int tipoNomina)
        {
            try
            {
                var result = await _context.RH_V_REPORTE_NOMINA_HISTORICO.DefaultIfEmpty().Where(e => e.CODIGO_TIPO_NOMINA==tipoNomina && e.CODIGO_PERIODO==codigoPeriodo).ToListAsync();
        
                return (List<RH_V_REPORTE_NOMINA_HISTORICO>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        
    }
}

