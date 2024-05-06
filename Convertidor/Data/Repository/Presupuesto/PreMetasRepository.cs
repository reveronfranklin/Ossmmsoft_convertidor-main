using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreMetasRepository : IPreMetasRepository
    {
        private readonly DataContextPre _context;

        public PreMetasRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_METAS> GetByCodigo(int codigoMeta)
        {
            try
            {
                var result = await _context.PRE_METAS.DefaultIfEmpty().Where(e => e.CODIGO_META == codigoMeta).FirstOrDefaultAsync();

                return (PRE_METAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_METAS>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {
                var result = await _context.PRE_METAS.Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto).DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<PRE_METAS>> Add(PRE_METAS entity)
        {
            ResultDto<PRE_METAS> result = new ResultDto<PRE_METAS>(null);
            try
            {



                await _context.PRE_METAS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_METAS>> Update(PRE_METAS entity)
        {
            ResultDto<PRE_METAS> result = new ResultDto<PRE_METAS>(null);

            try
            {
                PRE_METAS entityUpdate = await GetByCodigo(entity.CODIGO_META);
                if (entityUpdate != null)
                {


                    _context.PRE_METAS.Update(entity);
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

        public async Task<string> Delete(int codigoMeta)
        {

            try
            {
                PRE_METAS entity = await GetByCodigo(codigoMeta);
                if (entity != null)
                {
                    _context.PRE_METAS.Remove(entity);
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
                var last = await _context.PRE_METAS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_META)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_META + 1;
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
