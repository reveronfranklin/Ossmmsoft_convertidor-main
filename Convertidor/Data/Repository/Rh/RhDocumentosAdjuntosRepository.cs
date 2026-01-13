using Convertidor.Data.Entities.Rh;

using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
    public class RhDocumentosAdjuntosRepository: IRhDocumentosAdjuntosRepository
    {
		
        private readonly DataContext _context;

        public RhDocumentosAdjuntosRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<RH_DOCUMENTOS_ADJUNTOS> GetByCodigo(int codigoDocumentoAdjunto)
        {
            try
            {
                var result = await _context.RH_DOCUMENTOS_ADJUNTOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DOCUMENTO_ADJUNTO == codigoDocumentoAdjunto).FirstOrDefaultAsync();
        
                return (RH_DOCUMENTOS_ADJUNTOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RH_DOCUMENTOS_ADJUNTOS>> GetByCodigoDocumento(int codigoDocumento)
        {
            try
            {
                var result = await _context.RH_DOCUMENTOS_ADJUNTOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DOCUMENTO == codigoDocumento).ToListAsync();
        
                return (List<RH_DOCUMENTOS_ADJUNTOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
       

      
       
        
        public async Task<RH_DOCUMENTOS_ADJUNTOS> GetByNumeroDocumentoAdjunto(int numeroDocumento,string adjunto)
        {
            try
            {
                var result = await _context.RH_DOCUMENTOS_ADJUNTOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DOCUMENTO == numeroDocumento && e.ADJUNTO == adjunto).FirstOrDefaultAsync();
        
                return (RH_DOCUMENTOS_ADJUNTOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<RH_DOCUMENTOS_ADJUNTOS>> Add(RH_DOCUMENTOS_ADJUNTOS entity)
        {
            ResultDto<RH_DOCUMENTOS_ADJUNTOS> result = new ResultDto<RH_DOCUMENTOS_ADJUNTOS>(null);
            try
            {



                await _context.RH_DOCUMENTOS_ADJUNTOS.AddAsync(entity);
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

        public async Task<ResultDto<RH_DOCUMENTOS_ADJUNTOS>> Update(RH_DOCUMENTOS_ADJUNTOS entity)
        {
            ResultDto<RH_DOCUMENTOS_ADJUNTOS> result = new ResultDto<RH_DOCUMENTOS_ADJUNTOS>(null);

            try
            {
                RH_DOCUMENTOS_ADJUNTOS entityUpdate = await GetByCodigo(entity.CODIGO_DOCUMENTO_ADJUNTO);
                if (entityUpdate != null)
                {


                    _context.RH_DOCUMENTOS_ADJUNTOS.Update(entity);
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

        public async Task<string> Delete(int codigoDocumentoAdjunto)
        {

            try
            {
                RH_DOCUMENTOS_ADJUNTOS entity = await GetByCodigo(codigoDocumentoAdjunto);
                if (entity != null)
                {
                    _context.RH_DOCUMENTOS_ADJUNTOS.Remove(entity);
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
                var last = await _context.RH_DOCUMENTOS_ADJUNTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DOCUMENTO_ADJUNTO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DOCUMENTO_ADJUNTO + 1;
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


