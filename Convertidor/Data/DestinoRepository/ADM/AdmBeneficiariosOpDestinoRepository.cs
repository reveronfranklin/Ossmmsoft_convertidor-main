﻿using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.DestinoInterfaces.ADM;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.DestinoRepository.ADM
{
    public class AdmBeneficiariosOpDestinoRepository: IAdmBeneficiariosOpDestinoRepository
    {
        private readonly DestinoDataContext _context;
        public AdmBeneficiariosOpDestinoRepository(DestinoDataContext context)
        {
            _context = context;
        }

      

    
        
    
        public async Task<ResultDto<bool>>Add(List<ADM_BENEFICIARIOS_OP> entities) 
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try 
            {
                await _context.ADM_BENEFICIARIOS_OP.AddRangeAsync(entities);
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

                /*var listPuc = await _context.ADM_BENEFICIARIOS_OP.Where(x => x.CODIGO_ORDEN_PAGO == codigoOrdenPago)
                    .DefaultIfEmpty()
                    .ToListAsync();
                if (listPuc.Count > 0)
                {
                    _context.ADM_BENEFICIARIOS_OP.RemoveRange(listPuc);
                    await _context.SaveChangesAsync();
                }*/
                FormattableString xquerySaldo = $"DELETE FROM public.\"ADM_BENEFICIARIOS_OP\" WHERE  \"CODIGO_ORDEN_PAGO\" = {codigoOrdenPago}";
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
