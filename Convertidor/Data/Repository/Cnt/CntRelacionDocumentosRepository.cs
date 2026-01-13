using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntRelacionDocumentosRepository : ICntRelacionDocumentosRepository
    {
        private readonly DataContextCnt _context;

        public CntRelacionDocumentosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_RELACION_DOCUMENTOS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_RELACION_DOCUMENTOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CNT_RELACION_DOCUMENTOS> GetByCodigo(int codigoRelacionDocumento)
        {
            try
            {
                var result = await _context.CNT_RELACION_DOCUMENTOS.DefaultIfEmpty()
                                           .Where(x => x.CODIGO_RELACION_DOCUMENTO == codigoRelacionDocumento).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_RELACION_DOCUMENTOS>> Add(CNT_RELACION_DOCUMENTOS entity)
        {

            ResultDto<CNT_RELACION_DOCUMENTOS> result = new ResultDto<CNT_RELACION_DOCUMENTOS>(null);
            try
            {
                await _context.CNT_RELACION_DOCUMENTOS.AddAsync(entity);
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

        public async Task<ResultDto<CNT_RELACION_DOCUMENTOS>> Update(CNT_RELACION_DOCUMENTOS entity)
        {
            ResultDto<CNT_RELACION_DOCUMENTOS> result = new ResultDto<CNT_RELACION_DOCUMENTOS>(null);

            try
            {
                CNT_RELACION_DOCUMENTOS entityUpdate = await GetByCodigo(entity.CODIGO_RELACION_DOCUMENTO);
                if (entityUpdate != null)
                {
                    _context.CNT_RELACION_DOCUMENTOS.Update(entity);
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

        public async Task<string> Delete(int codigoRelaciondocumento)
        {
            try
            {
                CNT_RELACION_DOCUMENTOS entity = await GetByCodigo(codigoRelaciondocumento);
                if (entity != null)
                {
                    _context.CNT_RELACION_DOCUMENTOS.Remove(entity);
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
                var last = await _context.CNT_RELACION_DOCUMENTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_RELACION_DOCUMENTO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_RELACION_DOCUMENTO + 1;
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
