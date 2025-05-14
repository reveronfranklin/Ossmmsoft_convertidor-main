using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmBeneficiariosPagosRepository : IAdmBeneficiariosPagosRepository
    {
        private readonly DataContextAdm _context;
        public AdmBeneficiariosPagosRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_BENEFICIARIOS_CH> GetCodigoBeneficiarioPago(int codigoBeneficiarioPago)
        {
            try
            {
                var result = await _context.ADM_BENEFICIARIOS_CH
                    .Where(e => e.CODIGO_BENEFICIARIO_CH == codigoBeneficiarioPago).DefaultIfEmpty().FirstOrDefaultAsync();

                return (ADM_BENEFICIARIOS_CH)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

       
        public async Task<List<ADM_BENEFICIARIOS_CH>> GetAll()
        {
            try
            {
                var result = await _context.ADM_BENEFICIARIOS_CH.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ADM_BENEFICIARIOS_CH> GetByPago(int codigoPago)
        {
            try
            {
                var result = await _context.ADM_BENEFICIARIOS_CH.Where(x=>x.CODIGO_CHEQUE==codigoPago).DefaultIfEmpty().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
        

        
        public async Task<ResultDto<ADM_BENEFICIARIOS_CH>> Add(ADM_BENEFICIARIOS_CH entity)
        {

            ResultDto<ADM_BENEFICIARIOS_CH> result = new ResultDto<ADM_BENEFICIARIOS_CH>(null);
            try
            {
                await _context.ADM_BENEFICIARIOS_CH.AddAsync(entity);
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

        public async Task<ResultDto<ADM_BENEFICIARIOS_CH>> Update(ADM_BENEFICIARIOS_CH entity)
        {
            ResultDto<ADM_BENEFICIARIOS_CH> result = new ResultDto<ADM_BENEFICIARIOS_CH>(null);

            try
            {
                ADM_BENEFICIARIOS_CH entityUpdate = await GetCodigoBeneficiarioPago(entity.CODIGO_BENEFICIARIO_CH);
                if (entityUpdate != null)
                {
                    _context.ADM_BENEFICIARIOS_CH.Update(entity);
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
        public async Task<string> Delete(int codigoBeneficiarioPago)
        {
            try
            {
                ADM_BENEFICIARIOS_CH entity = await GetCodigoBeneficiarioPago (codigoBeneficiarioPago);
                if (entity != null)
                {
                    _context.ADM_BENEFICIARIOS_CH.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public async Task<string> DeleteByCodigoPago(int codigoPago)
        {
            try
            {
                var entities = await GetCodigoBeneficiarioPago (codigoPago);
                if (entities != null)
                {
                    _context.ADM_BENEFICIARIOS_CH.RemoveRange(entities);
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
                var last = await _context.ADM_BENEFICIARIOS_CH.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_BENEFICIARIO_CH)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_BENEFICIARIO_CH + 1;
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
