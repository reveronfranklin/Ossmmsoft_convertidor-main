using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntDetalleEdoCtaRepository : ICntDetalleEdoCtaRepository
    {
        private readonly DataContextCnt context;

        public CntDetalleEdoCtaRepository(DataContextCnt context)
        {
            this.context = context;
        }

        public async Task<List<CNT_DETALLE_EDO_CTA>> GetAll() 
        {
            try 
            {
            
                var result = await context.CNT_DETALLE_EDO_CTA.DefaultIfEmpty().ToListAsync();
                return result;

            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;

            }
        
        }
    }
}
