using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.DestinoInterfaces.ADM;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.DestinoRepository.ADM
{
    public class AdmPucOrdenPagoDestinoRepository: IAdmPucOrdenPagoDestinoRepository
    {
        private readonly DestinoDataContext _context;
        public AdmPucOrdenPagoDestinoRepository(DestinoDataContext context)
        {
            _context = context;
        }

        public async Task<ADM_PUC_ORDEN_PAGO> GetByCodigoPucOrdenPago(int codigoPucOrdenPago)
        {
            try
            {
                var pucOrdenPago =
                    await _context.ADM_PUC_ORDEN_PAGO.Where(x => x.CODIGO_ORDEN_PAGO == codigoPucOrdenPago).FirstOrDefaultAsync();
                return pucOrdenPago;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    
        
    
        public async Task<ResultDto<bool>>Add(List<ADM_PUC_ORDEN_PAGO> entities) 
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try 
            {
                
                await  _context.ADM_PUC_ORDEN_PAGO.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                


                result.Data = true;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.IsValid = false;
                if (ex.InnerException != null)
                {
                    result.Message = ex.InnerException.Message;
                }
                else
                {
                    result.Message = ex.Message;
                }
                
                return result;
            }
        }

    
        public async Task<string>Delete(int codigoOrdenPago) 
        {
            try
            {

                /*var listPuc = await _context.ADM_PUC_ORDEN_PAGO.Where(x => x.CODIGO_ORDEN_PAGO == codigoOrdenPago)
                  
                    .ToListAsync();
                if (listPuc!=null && listPuc.Count > 0)
                {
                    _context.ADM_PUC_ORDEN_PAGO.RemoveRange(listPuc);
                    await _context.SaveChangesAsync();
                }*/
              
                
                FormattableString xquerySaldo = $"DELETE FROM public.\"ADM_PUC_ORDEN_PAGO\" WHERE  \"CODIGO_ORDEN_PAGO\" = {codigoOrdenPago}";
                var result = await _context.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);

             
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
     
        
     
    }
}
