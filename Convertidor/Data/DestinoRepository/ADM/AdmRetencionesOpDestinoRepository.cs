using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.DestinoInterfaces.ADM;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.DestinoRepository.ADM
{
    public class AdmRetencionesOpDestinoRepository: IAdmRetencionesOpDestinoRepository
    {
        private readonly DestinoDataContext _context;
        public AdmRetencionesOpDestinoRepository(DestinoDataContext context)
        {
            _context = context;
        }

      

    
        
    
        public async Task<ResultDto<bool>>Add(List<ADM_RETENCIONES_OP> entities) 
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try 
            {
                await _context.ADM_RETENCIONES_OP.AddRangeAsync(entities);
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

                var result = await _context.ADM_RETENCIONES_OP.Where(x => x.CODIGO_ORDEN_PAGO == codigoOrdenPago).AnyAsync();
                if (result==true)
                {
                    var listPuc = await _context.ADM_RETENCIONES_OP.Where(x => x.CODIGO_ORDEN_PAGO == codigoOrdenPago)
                   
                        .ToListAsync();
                    _context.ADM_RETENCIONES_OP.RemoveRange(listPuc);
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
