using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmImpuestosDocumentosOpRepository : IAdmImpuestosDocumentosOpRepository
    {
        private readonly DataContextAdm _context;
        public AdmImpuestosDocumentosOpRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_IMPUESTOS_DOCUMENTOS_OP> GetCodigoImpuestoDocumentoOp(int codigoImpuestoDocumentoOp)
        {
            try
            {
                var result = await _context.ADM_IMPUESTOS_DOCUMENTOS_OP
                    .Where(e => e.CODIGO_IMPUESTO_DOCUMENTO_OP == codigoImpuestoDocumentoOp).FirstOrDefaultAsync();

                return (ADM_IMPUESTOS_DOCUMENTOS_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<ADM_IMPUESTOS_DOCUMENTOS_OP> GetByDocumentoCodigoRetencionTipoRetencion(int codigoDocumentoOp,int codigoRetencion,int tipoRetencionId)
        {
            try
            {
                var result = await _context.ADM_IMPUESTOS_DOCUMENTOS_OP
                    .Where(e => e.CODIGO_DOCUMENTO_OP == codigoDocumentoOp && e.CODIGO_RETENCION==codigoRetencion && e.TIPO_RETENCION_ID==tipoRetencionId).FirstOrDefaultAsync();

                return (ADM_IMPUESTOS_DOCUMENTOS_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_IMPUESTOS_DOCUMENTOS_OP>> GetByDocumento(int codigoDocumento)
        {
            try
            {
                var result = await _context.ADM_IMPUESTOS_DOCUMENTOS_OP.DefaultIfEmpty().Where(x=>x.CODIGO_DOCUMENTO_OP==codigoDocumento).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
      
        
        
        public async Task<List<ADM_IMPUESTOS_DOCUMENTOS_OP>> GetAll()
        {
            try
            {
                var result = await _context.ADM_IMPUESTOS_DOCUMENTOS_OP.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_IMPUESTOS_DOCUMENTOS_OP>> Add(ADM_IMPUESTOS_DOCUMENTOS_OP entity)
        {

            ResultDto<ADM_IMPUESTOS_DOCUMENTOS_OP> result = new ResultDto<ADM_IMPUESTOS_DOCUMENTOS_OP>(null);
            try
            {
                await _context.ADM_IMPUESTOS_DOCUMENTOS_OP.AddAsync(entity);
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

        public async Task<ResultDto<ADM_IMPUESTOS_DOCUMENTOS_OP>> Update(ADM_IMPUESTOS_DOCUMENTOS_OP entity)
        {
            ResultDto<ADM_IMPUESTOS_DOCUMENTOS_OP> result = new ResultDto<ADM_IMPUESTOS_DOCUMENTOS_OP>(null);

            try
            {
                ADM_IMPUESTOS_DOCUMENTOS_OP entityUpdate = await GetCodigoImpuestoDocumentoOp(entity.CODIGO_IMPUESTO_DOCUMENTO_OP);
                if (entityUpdate != null)
                {
                    _context.ADM_IMPUESTOS_DOCUMENTOS_OP.Update(entity);
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
        public async Task<string> Delete(int codigoImpuestoDocumentoOp)
        {
            try
            {
                ADM_IMPUESTOS_DOCUMENTOS_OP entity = await GetCodigoImpuestoDocumentoOp (codigoImpuestoDocumentoOp);
                if (entity != null)
                {
                    _context.ADM_IMPUESTOS_DOCUMENTOS_OP.Remove(entity);
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
                var last = await _context.ADM_IMPUESTOS_DOCUMENTOS_OP.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_IMPUESTO_DOCUMENTO_OP)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_IMPUESTO_DOCUMENTO_OP + 1;
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
