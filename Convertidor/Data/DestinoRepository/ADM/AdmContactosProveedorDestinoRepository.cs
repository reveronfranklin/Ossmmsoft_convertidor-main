using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.DestinoInterfaces.ADM;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.DestinoRepository.ADM
{
    public class AdmContactosProveedorDestinoRepository: IAdmContactosProveedorDestinoRepository
    {
        private readonly DestinoDataContext _context;
        public AdmContactosProveedorDestinoRepository(DestinoDataContext context)
        {
            _context = context;
        }

      

    
        
    
        public async Task<ResultDto<bool>>Add(List<ADM_CONTACTO_PROVEEDOR> entities) 
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try 
            {
                await _context.ADM_CONTACTO_PROVEEDOR.AddRangeAsync(entities);
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

    
        public async Task<string>Delete(int codigoProveedor) 
        {
            try
            {

               /* var contactos = await _context.ADM_CONTACTO_PROVEEDOR.Where(x => x.CODIGO_PROVEEDOR == codigoProveedor)
                    .DefaultIfEmpty()
                    .ToListAsync();
                if (contactos.Count > 0)
                {
                    _context.ADM_CONTACTO_PROVEEDOR.RemoveRange(contactos);
                    await _context.SaveChangesAsync();
                }*/
              
                FormattableString xquerySaldo = $"DELETE FROM public.\"ADM_CONTACTO_PROVEEDOR\" WHERE  \"CODIGO_PROVEEDOR\" = {codigoProveedor}";
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
