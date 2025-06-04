using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmBeneficiariosOpRepository : IAdmBeneficiariosOpRepository
    {
        private readonly DataContextAdm _context;
        public AdmBeneficiariosOpRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_BENEFICIARIOS_OP> GetCodigoBeneficiarioOp(int codigoBeneficiarioOp)
        {
            try
            {
                var result = await _context.ADM_BENEFICIARIOS_OP
                    .Where(e => e.CODIGO_BENEFICIARIO_OP == codigoBeneficiarioOp).FirstOrDefaultAsync();

                return (ADM_BENEFICIARIOS_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

       
        public async Task<List<ADM_BENEFICIARIOS_OP>> GetAll()
        {
            try
            {
                var result = await _context.ADM_BENEFICIARIOS_OP.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<List<ADM_BENEFICIARIOS_OP>> GetByOrdenPago(int codigoOrdenPago)
        {
            try
            {
                var result = await _context.ADM_BENEFICIARIOS_OP.Where(x=>x.CODIGO_ORDEN_PAGO==codigoOrdenPago).DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
        
        public async Task<ADM_BENEFICIARIOS_OP> GetByOrdenPagoProveedor(int codigoOrdenPago,int codigoProveedor)
        {
            try
            {
                var result = await _context.ADM_BENEFICIARIOS_OP.Where(x=>x.CODIGO_ORDEN_PAGO==codigoOrdenPago && x.CODIGO_PROVEEDOR==codigoProveedor).DefaultIfEmpty().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        
        public async Task<ResultDto<ADM_BENEFICIARIOS_OP>> Add(ADM_BENEFICIARIOS_OP entity)
        {

            ResultDto<ADM_BENEFICIARIOS_OP> result = new ResultDto<ADM_BENEFICIARIOS_OP>(null);
            try
            {
                await _context.ADM_BENEFICIARIOS_OP.AddAsync(entity);
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
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultDto<ADM_BENEFICIARIOS_OP>> Update(ADM_BENEFICIARIOS_OP entity)
        {
            ResultDto<ADM_BENEFICIARIOS_OP> result = new ResultDto<ADM_BENEFICIARIOS_OP>(null);

            try
            {
                ADM_BENEFICIARIOS_OP entityUpdate = await GetCodigoBeneficiarioOp(entity.CODIGO_BENEFICIARIO_OP);
                if (entityUpdate != null)
                {
                    _context.ADM_BENEFICIARIOS_OP.Update(entity);
                    await _context.SaveChangesAsync();
                    result.Data = entity;
                    result.IsValid = true;
                    result.Message = "";

                }
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
        }
        public async Task<string> Delete(int codigoBeneficiarioOp)
        {
            try
            {
                ADM_BENEFICIARIOS_OP entity = await GetCodigoBeneficiarioOp (codigoBeneficiarioOp);
                if (entity != null)
                {
                    _context.ADM_BENEFICIARIOS_OP.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public async Task<string>UpdateMontoPagado(int codigoBeneficiarioOp,decimal montoPagado) 
        {
            try
            {

           
                FormattableString xquerySaldo = $"UPDATE ADM_BENEFICIARIOS_OP SET MONTO_PAGADO={montoPagado} WHERE  CODIGO_BENEFICIARIO_OP = {codigoBeneficiarioOp}";
                var result = await _context.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);

             
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public async Task<string>UpdateMontoAnulado(int codigoOrdenPago) 
        {
            try
            {

           
                FormattableString xquerySaldo = $"UPDATE ADM_BENEFICIARIOS_OP SET MONTO_ANULADO=MONTO WHERE  CODIGO_ORDEN_PAGO = {codigoOrdenPago}";
                var result = await _context.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);

             
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        
        public async Task<string> DeleteByOrdenPago(int codigoOrdenPago)
        {
            try
            {
                var entities = await GetByOrdenPago (codigoOrdenPago);
                if (entities != null)
                {
                    _context.ADM_BENEFICIARIOS_OP.RemoveRange(entities);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.ADM_BENEFICIARIOS_OP.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_BENEFICIARIO_OP)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_BENEFICIARIO_OP + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }
    }
}
