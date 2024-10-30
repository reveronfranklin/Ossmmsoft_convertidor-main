using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.DestinoInterfaces.ADM;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.DestinoRepository.ADM
{
    public class AdmOrdenPagoDestinoRepository: IAdmOrdenPagoDestinoRepository
    {
        private readonly DestinoDataContext _context;
        public AdmOrdenPagoDestinoRepository(DestinoDataContext context)
        {
            _context = context;
        }

        public async Task<ADM_ORDEN_PAGO> GetCodigoOrdenPago(int codigoOrdenPago)
        {
            try
            {
                var result = await _context.ADM_ORDEN_PAGO
                    .Where(e => e.CODIGO_ORDEN_PAGO == codigoOrdenPago)
                    .DefaultIfEmpty()
                    .FirstOrDefaultAsync();

                return (ADM_ORDEN_PAGO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

    
        
    
        public async Task<ResultDto<ADM_ORDEN_PAGO>>Add(ADM_ORDEN_PAGO entity) 
        {

            ResultDto<ADM_ORDEN_PAGO> result = new ResultDto<ADM_ORDEN_PAGO>(null);
            try 
            {
                await _context.ADM_ORDEN_PAGO.AddAsync(entity);
                await _context.SaveChangesAsync();


                result.Data = entity;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
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
                ADM_ORDEN_PAGO entity = await GetCodigoOrdenPago(codigoOrdenPago);
                if (entity != null)
                {
                    _context.ADM_ORDEN_PAGO.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
     
        
     
    }
}
