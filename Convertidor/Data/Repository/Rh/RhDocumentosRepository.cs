using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhDocumentosRepository : IRhDocumentosRepository
    {
		
        private readonly DataContext _context;

        public RhDocumentosRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_DOCUMENTOS>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_DOCUMENTOS.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona).ToListAsync();
        
                return (List<RH_DOCUMENTOS>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        //public async Task<RH_DOCUMENTOS> GetPrimerMovimientoCodigoPersona(int codigoPersona)
        //{
        //    try
        //    {
        //        var result = await _context.RH_DOCUMENTOS.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona)
        //                    .OrderBy(x=>x.FECHA_INGRESO).FirstOrDefaultAsync();

        //        return (RH_DOCUMENTOS)result;
        //    }
        //    catch (Exception ex)
        //    {
        //        var res = ex.InnerException.Message;
        //        return null;
        //    }

        //}

        public async Task<RH_DOCUMENTOS> GetByCodigo(int codigoDocumento)
        {
            try
            {
                var result = await _context.RH_DOCUMENTOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DOCUMENTO == codigoDocumento)
                    .OrderBy(x=>x.FECHA_INS).FirstOrDefaultAsync();

                return (RH_DOCUMENTOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<RH_DOCUMENTOS>> Add(RH_DOCUMENTOS entity)
        {
            ResultDto<RH_DOCUMENTOS> result = new ResultDto<RH_DOCUMENTOS>(null);
            try
            {



                await _context.RH_DOCUMENTOS.AddAsync(entity);
                _context.SaveChanges();


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

        public async Task<ResultDto<RH_DOCUMENTOS>> Update(RH_DOCUMENTOS entity)
        {
            ResultDto<RH_DOCUMENTOS> result = new ResultDto<RH_DOCUMENTOS>(null);

            try
            {
                RH_DOCUMENTOS entityUpdate = await GetByCodigo(entity.CODIGO_DOCUMENTO);
                if (entityUpdate != null)
                {


                    _context.RH_DOCUMENTOS.Update(entity);
                    _context.SaveChanges();
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

        public async Task<string> Delete(int codigoDocumento)
        {

            try
            {
                RH_DOCUMENTOS entity = await GetByCodigo(codigoDocumento);
                if (entity != null)
                {
                    _context.RH_DOCUMENTOS.Remove(entity);
                    _context.SaveChanges();
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
                var last = await _context.RH_DOCUMENTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DOCUMENTO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DOCUMENTO + 1;
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

