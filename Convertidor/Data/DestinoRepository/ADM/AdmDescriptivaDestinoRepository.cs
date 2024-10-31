using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.DestinoInterfaces.ADM;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.DestinoRepository.ADM
{
    public class AdmDescriptivaDestinoRepository: IAdmDescriptivaDestinoRepository
    {
        private readonly DestinoDataContext _context;
        public AdmDescriptivaDestinoRepository(DestinoDataContext context)
        {
            _context = context;
        }

      

    
        
    
        public async Task<ResultDto<bool>>Add(ADM_DESCRIPTIVAS entities) 
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try 
            {
                await _context.ADM_DESCRIPTIVAS.AddAsync(entities);
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

    
        public async Task<string>Delete(int idDescriptiva) 
        {
            try
            {

               /* var proveedor = await _context.ADM_DESCRIPTIVAS.Where(x => x.DESCRIPCION_ID == idDescriptiva)
                    .DefaultIfEmpty()
                    .FirstOrDefaultAsync();
                if (proveedor!=null)
                {
                    _context.ADM_DESCRIPTIVAS.Remove(proveedor);
                    await _context.SaveChangesAsync();
                }*/
              
                
                FormattableString xquerySaldo = $"DELETE FROM public.\"ADM_DESCRIPTIVAS\" WHERE  \"DESCRIPCION_ID\" = {idDescriptiva}";
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
