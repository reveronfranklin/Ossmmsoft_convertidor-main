using System.Globalization;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmDocumentosOpRepository : IAdmDocumentosOpRepository
    {
        private readonly DataContextAdm _context;
        public AdmDocumentosOpRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_DOCUMENTOS_OP> GetCodigoDocumentoOp(int codigoDocumentoOp)
        {
            try
            {
                var result = await _context.ADM_DOCUMENTOS_OP
                    .Where(e => e.CODIGO_DOCUMENTO_OP == codigoDocumentoOp).FirstOrDefaultAsync();

                return (ADM_DOCUMENTOS_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        
        public async Task<List<ADM_DOCUMENTOS_OP>> GetByCodigoOrdenPago(int codigoOrdenPago)
        {
            try
            {
                var result = await _context.ADM_DOCUMENTOS_OP
                    .Where(e => e.CODIGO_ORDEN_PAGO == codigoOrdenPago).ToListAsync();

                return (List<ADM_DOCUMENTOS_OP>)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_DOCUMENTOS_OP>> GetAll()
        {
            try
            {
                var result = await _context.ADM_DOCUMENTOS_OP.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_DOCUMENTOS_OP>> Add(ADM_DOCUMENTOS_OP entity)
        {

            ResultDto<ADM_DOCUMENTOS_OP> result = new ResultDto<ADM_DOCUMENTOS_OP>(null);
            try
            {
                await _context.ADM_DOCUMENTOS_OP.AddAsync(entity);
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

        public async Task<ResultDto<ADM_DOCUMENTOS_OP>> Update(ADM_DOCUMENTOS_OP entity)
        {
            ResultDto<ADM_DOCUMENTOS_OP> result = new ResultDto<ADM_DOCUMENTOS_OP>(null);

            try
            {
                ADM_DOCUMENTOS_OP entityUpdate = await GetCodigoDocumentoOp(entity.CODIGO_DOCUMENTO_OP);
                if (entityUpdate != null)
                {
                    _context.ADM_DOCUMENTOS_OP.Update(entity);
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
        
        
        public async Task<string> UpdateMontoRetenido(int codigoDocumentoOp,decimal monto)
        {


            string montoString = monto.ToString(CultureInfo.InvariantCulture);
            try
            {
            
                
                FormattableString xqueryDiario = $"UPDATE  ADM.ADM_DOCUMENTOS_OP SET MONTO_RETENIDO = {montoString} WHERE CODIGO_DOCUMENTO_OP= {codigoDocumentoOp};";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        
      
        
      
        
        public async Task<string> Delete(int codigoDocumentoOp)
        {
            try
            {
                ADM_DOCUMENTOS_OP entity = await GetCodigoDocumentoOp (codigoDocumentoOp);
                if (entity != null)
                {
                    _context.ADM_DOCUMENTOS_OP.Remove(entity);
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
                var last = await _context.ADM_DOCUMENTOS_OP.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DOCUMENTO_OP)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DOCUMENTO_OP + 1;
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
