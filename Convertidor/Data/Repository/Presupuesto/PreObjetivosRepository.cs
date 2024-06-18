
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreObjetivosRepository : IPreObjetivosRepository
    {
        private readonly DataContextPre _context;

        public PreObjetivosRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_OBJETIVOS> GetByCodigo(int codigoObjetivo)
        {
            try
            {
                var result = await _context.PRE_OBJETIVOS.DefaultIfEmpty().Where(e => e.CODIGO_OBJETIVO == codigoObjetivo).FirstOrDefaultAsync();

                return (PRE_OBJETIVOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_OBJETIVOS>> GetAll()
        {
            try
            {
                var result = await _context.PRE_OBJETIVOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<PRE_OBJETIVOS>> Add(PRE_OBJETIVOS entity)
        {
            ResultDto<PRE_OBJETIVOS> result = new ResultDto<PRE_OBJETIVOS>(null);
            try
            {



                await _context.PRE_OBJETIVOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_OBJETIVOS>> Update(PRE_OBJETIVOS entity)
        {
            ResultDto<PRE_OBJETIVOS> result = new ResultDto<PRE_OBJETIVOS>(null);

            try
            {
                PRE_OBJETIVOS entityUpdate = await GetByCodigo(entity.CODIGO_OBJETIVO);
                if (entityUpdate != null)
                {


                    _context.PRE_OBJETIVOS.Update(entity);
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

        public async Task<string> Delete(int codigoObjetivo)
        {

            try
            {
                PRE_OBJETIVOS entity = await GetByCodigo(codigoObjetivo);
                if (entity != null)
                {
                    _context.PRE_OBJETIVOS.Remove(entity);
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
                var last = await _context.PRE_OBJETIVOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_OBJETIVO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_OBJETIVO + 1;
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
