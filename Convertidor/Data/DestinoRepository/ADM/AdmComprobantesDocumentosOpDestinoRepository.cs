using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.DestinoInterfaces.ADM;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.DestinoRepository.ADM
{
    public class AdmComprobantesDocumentosOpDestinoRepository: IAdmComprobantesDocumentosOpDestinoRepository
    {
        private readonly DestinoDataContext _context;
        public AdmComprobantesDocumentosOpDestinoRepository(DestinoDataContext context)
        {
            _context = context;
        }

      

    
        
    
        public async Task<ResultDto<bool>>Add(List<ADM_COMPROBANTES_DOCUMENTOS_OP> entities) 
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try 
            {
                await _context.ADM_COMPROBANTES_DOCUMENTOS_OP.AddRangeAsync(entities);
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

           
                FormattableString xquerySaldo = $"DELETE FROM public.\"ADM_COMPROBANTES_DOCUMENTOS_OP\" WHERE  \"CODIGO_ORDEN_PAGO\" = {codigoOrdenPago}";
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
