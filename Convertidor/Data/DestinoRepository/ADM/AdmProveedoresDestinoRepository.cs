using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.DestinoInterfaces.ADM;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.DestinoRepository.ADM
{
    public class AdmProveedoresDestinoRepository: IAdmProveedoresDestinoRepository
    {
        private readonly DestinoDataContext _context;
        public AdmProveedoresDestinoRepository(DestinoDataContext context)
        {
            _context = context;
        }

      

    
        
    
        public async Task<ResultDto<bool>>Add(ADM_PROVEEDORES entities) 
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try 
            {
                await _context.ADM_PROVEEDORES.AddAsync(entities);
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

                /*var proveedor = await _context.ADM_PROVEEDORES.Where(x => x.CODIGO_PROVEEDOR == codigoProveedor)
                    .DefaultIfEmpty()
                    .FirstOrDefaultAsync();
                if (proveedor!=null)
                {
                    _context.ADM_PROVEEDORES.Remove(proveedor);
                    await _context.SaveChangesAsync();
                }*/
              
                
                FormattableString xquerySaldo = $"DELETE FROM public.\"ADM_PROVEEDORES\" WHERE  \"CODIGO_PROVEEDOR\" = {codigoProveedor}";
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
