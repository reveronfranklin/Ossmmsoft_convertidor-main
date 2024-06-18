using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhReporteFirmaRepository : IRhReporteFirmaRepository
    {
		
        private readonly DataContext _context;

        public RhReporteFirmaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_V_REPORTE_NOMINA_FIRMA>> GetAll()
        {
            try
            {
                var result = await _context.RH_V_REPORTE_NOMINA_FIRMA.DefaultIfEmpty().ToListAsync();
        
                return (List<RH_V_REPORTE_NOMINA_FIRMA>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
  
        
    }
}

