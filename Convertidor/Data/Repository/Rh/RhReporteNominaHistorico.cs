using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhReporteNominaHistoricoRepository : IRhReporteNominaHistoricoRepository
    {
		
        private readonly DataContext _context;

        public RhReporteNominaHistoricoRepository(DataContext context)
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
        public async Task<List<RH_V_REPORTE_NOMINA_HISTORICO>> GetByPeriodoTipoNominaPersona(int codigoPeriodo,int tipoNomina,int codigoPersona)
        {
            try
            {
                var result = await _context.RH_V_REPORTE_NOMINA_HISTORICO.DefaultIfEmpty()
                    .Where(e => e.CODIGO_TIPO_NOMINA==tipoNomina && e.CODIGO_PERIODO==codigoPeriodo && e.CODIGO_PERSONA==codigoPersona).ToListAsync();
        
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

