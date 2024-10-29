using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.DestinoInterfaces.ADM;
using Convertidor.Data.DestinoInterfaces.PRE;
using Convertidor.Data.EntitiesDestino.PRE;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.DestinoRepository.PRE
{
    public class PreVSaldoDestinoRepository: IPreVSaldoDestinoRepository
    {
        private readonly DestinoDataContext _context;
        public PreVSaldoDestinoRepository(DestinoDataContext context)
        {
            _context = context;
        }

      

    
        
    
        public async Task<ResultDto<bool>>Add(PRE_V_SALDOS entities) 
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try 
            {
                await _context.PRE_V_SALDOS.AddAsync(entities);
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

    
        public async Task<string>Delete(int codigoPresupuesto,int codigoSaldo) 
        {
            try
            {

                var saldo = await _context.PRE_V_SALDOS.Where(x => x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.CODIGO_SALDO == codigoSaldo)
                    .DefaultIfEmpty()
                    .FirstOrDefaultAsync();
                if (saldo!=null)
                {
                    _context.PRE_V_SALDOS.Remove(saldo);
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
