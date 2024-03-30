using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhReporteNominaTemporalRepository : IRhReporteNominaTemporalRepository
    {
		
        private readonly DataContext _context;

        public RhReporteNominaTemporalRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_V_REPORTE_NOMINA_TEMPORAL>> GetByPeriodoTipoNomina(int codigoPeriodo,int tipoNomina)
        {
            try
            {
                var result = await _context.RH_V_REPORTE_NOMINA_TEMPORAL.DefaultIfEmpty().Where(e => e.CODIGO_TIPO_NOMINA==tipoNomina && e.CODIGO_PERIODO==codigoPeriodo).ToListAsync();
        
                return (List<RH_V_REPORTE_NOMINA_TEMPORAL>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RH_V_REPORTE_NOMINA_TEMPORAL>> GetByPeriodoTipoNominaPersona(int codigoPeriodo,int tipoNomina,int codigoPersona)
        {
            try
            {
                var result = await _context.RH_V_REPORTE_NOMINA_TEMPORAL.DefaultIfEmpty()
                    .Where(e => e.CODIGO_TIPO_NOMINA==tipoNomina && e.CODIGO_PERIODO==codigoPeriodo && e.CODIGO_PERSONA==codigoPersona).ToListAsync();
        
                return (List<RH_V_REPORTE_NOMINA_TEMPORAL>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        
    }
}

