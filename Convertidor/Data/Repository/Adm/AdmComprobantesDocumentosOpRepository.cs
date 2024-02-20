using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmComprobantesDocumentosOpRepository : IAdmComprobantesDocumentosOpRepository
    {
        private readonly DataContextAdm _context;
        public AdmComprobantesDocumentosOpRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_COMPROBANTES_DOCUMENTOS_OP> GetCodigoComprobanteDocOp(int codigoComprobanteDocOp)
        {
            try
            {
                var result = await _context.ADM_COMPROBANTES_DOCUMENTOS_OP
                    .Where(e => e.CODIGO_COMPROBANTE_DOC_OP == codigoComprobanteDocOp).FirstOrDefaultAsync();

                return (ADM_COMPROBANTES_DOCUMENTOS_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_COMPROBANTES_DOCUMENTOS_OP>> GetAll()
        {
            try
            {
                var result = await _context.ADM_COMPROBANTES_DOCUMENTOS_OP.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_COMPROBANTES_DOCUMENTOS_OP>> Add(ADM_COMPROBANTES_DOCUMENTOS_OP entity)
        {

            ResultDto<ADM_COMPROBANTES_DOCUMENTOS_OP> result = new ResultDto<ADM_COMPROBANTES_DOCUMENTOS_OP>(null);
            try
            {
                await _context.ADM_COMPROBANTES_DOCUMENTOS_OP.AddAsync(entity);
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

        public async Task<ResultDto<ADM_COMPROBANTES_DOCUMENTOS_OP>> Update(ADM_COMPROBANTES_DOCUMENTOS_OP entity)
        {
            ResultDto<ADM_COMPROBANTES_DOCUMENTOS_OP> result = new ResultDto<ADM_COMPROBANTES_DOCUMENTOS_OP>(null);

            try
            {
                ADM_COMPROBANTES_DOCUMENTOS_OP entityUpdate = await GetCodigoComprobanteDocOp(entity.CODIGO_COMPROBANTE_DOC_OP);
                if (entityUpdate != null)
                {
                    _context.ADM_COMPROBANTES_DOCUMENTOS_OP.Update(entity);
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
        public async Task<string> Delete(int codigoComprobanteDocOp)
        {
            try
            {
                ADM_COMPROBANTES_DOCUMENTOS_OP entity = await GetCodigoComprobanteDocOp (codigoComprobanteDocOp);
                if (entity != null)
                {
                    _context.ADM_COMPROBANTES_DOCUMENTOS_OP.Remove(entity);
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
                var last = await _context.ADM_COMPROBANTES_DOCUMENTOS_OP.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_COMPROBANTE_DOC_OP)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_COMPROBANTE_DOC_OP + 1;
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
