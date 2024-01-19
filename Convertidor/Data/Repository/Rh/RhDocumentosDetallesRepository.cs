using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhDocumentosDetallesRepository : IRhDocumentosDetallesRepository
    {
		
        private readonly DataContext _context;

        public RhDocumentosDetallesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<RH_DOCUMENTOS_DETALLES> GetByCodigo(int codigoDocumentoDetalle)
        {
            try
            {
                var result = await _context.RH_DOCUMENTOS_DETALLES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DOCUMENTO_DETALLE == codigoDocumentoDetalle)
                    .OrderBy(x=>x.FECHA_INS).FirstOrDefaultAsync();

                return (RH_DOCUMENTOS_DETALLES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_DOCUMENTOS_DETALLES>> GetByCodigoDocumento(int codigoDocumento)
        {
            try
            {
                var result = await _context.RH_DOCUMENTOS_DETALLES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DOCUMENTO == codigoDocumento)
                    .OrderBy(x => x.FECHA_INS).ToListAsync();

                return (List<RH_DOCUMENTOS_DETALLES>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<RH_DOCUMENTOS_DETALLES>> Add(RH_DOCUMENTOS_DETALLES entity)
        {
            ResultDto<RH_DOCUMENTOS_DETALLES> result = new ResultDto<RH_DOCUMENTOS_DETALLES>(null);
            try
            {

                await _context.RH_DOCUMENTOS_DETALLES.AddAsync(entity);
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

        public async Task<ResultDto<RH_DOCUMENTOS_DETALLES>> Update(RH_DOCUMENTOS_DETALLES entity)
        {
            ResultDto<RH_DOCUMENTOS_DETALLES> result = new ResultDto<RH_DOCUMENTOS_DETALLES>(null);

            try
            {
                RH_DOCUMENTOS_DETALLES entityUpdate = await GetByCodigo(entity.CODIGO_DOCUMENTO_DETALLE);
                if (entityUpdate != null)
                {


                    _context.RH_DOCUMENTOS_DETALLES.Update(entity);
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

        public async Task<string> Delete(int codigoDocumentoDetalle)
        {

            try
            {
                RH_DOCUMENTOS_DETALLES entity = await GetByCodigo(codigoDocumentoDetalle);
                if (entity != null)
                {
                    _context.RH_DOCUMENTOS_DETALLES.Remove(entity);
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
                var last = await _context.RH_DOCUMENTOS_DETALLES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DOCUMENTO_DETALLE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DOCUMENTO_DETALLE+ 1;
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

