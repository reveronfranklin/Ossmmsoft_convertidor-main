using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatAuditoriaRepository : ICatAuditoriaRepository
    {
        private readonly DataContextCat _context;

        public CatAuditoriaRepository(DataContextCat context) 
        {
           _context = context;
        }

        public async Task<List<CAT_AUDITORIA>> GetAll()
        {
            try
            {
                var result = await _context.CAT_AUDITORIA.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CAT_AUDITORIA> GetByCodigo(int codigoArrendamientoInmueble)
        {
            try
            {

                var result = await _context.CAT_AUDITORIA.DefaultIfEmpty()
                    .Where(x => x.CODIGO_AUDITORIA == codigoArrendamientoInmueble)
                    .FirstOrDefaultAsync();
                return (CAT_AUDITORIA)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

        }



        public async Task<ResultDto<CAT_AUDITORIA>> Add(CAT_AUDITORIA entity)
        {
            ResultDto<CAT_AUDITORIA> result = new ResultDto<CAT_AUDITORIA>(null);
            try
            {



                await _context.CAT_AUDITORIA.AddAsync(entity);
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

        public async Task<ResultDto<CAT_AUDITORIA>> Update(CAT_AUDITORIA entity)
        {
            ResultDto<CAT_AUDITORIA> result = new ResultDto<CAT_AUDITORIA>(null);

            try
            {
                CAT_AUDITORIA entityUpdate = await GetByCodigo(entity.CODIGO_AUDITORIA);
                if (entityUpdate != null)
                {


                    _context.CAT_AUDITORIA.Update(entity);
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

        public async Task<string> Delete(int codigoAuditoria)
        {

            try
            {
                CAT_AUDITORIA entity = await GetByCodigo(codigoAuditoria);
                if (entity != null)
                {
                    _context.CAT_AUDITORIA.Remove(entity);
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
                var last = await _context.CAT_AUDITORIA.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_AUDITORIA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_AUDITORIA + 1;
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
