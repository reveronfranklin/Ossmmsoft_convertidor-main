using Convertidor.Data.Entities.Presupuesto;
using Microsoft.EntityFrameworkCore;
using Convertidor.Data.Interfaces.Presupuesto;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreEjecucionPresupuestariaRepository : IPreEjecucionPresupuestariaRepository
    {
        private readonly DataContextPre _context;

        public PreEjecucionPresupuestariaRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_EJECUCION_PRESUPUESTARIA> GetByCodigo(int codigoEjePresupuestaria) 
        {
            try 
            {
            
                var result = await _context.PRE_EJECUCION_PRESUPUESTARIA.DefaultIfEmpty().Where(e => e.CODIGO_EJE_PRESUPUESTARIA == codigoEjePresupuestaria).FirstOrDefaultAsync();
                return (PRE_EJECUCION_PRESUPUESTARIA)result;
                
            }

            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_EJECUCION_PRESUPUESTARIA>> GetAll()
        {
            try
            {

                var result = await _context.PRE_EJECUCION_PRESUPUESTARIA.DefaultIfEmpty().ToListAsync();
                return result;

            }

            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<PRE_EJECUCION_PRESUPUESTARIA>> Add(PRE_EJECUCION_PRESUPUESTARIA entity)
        {
            ResultDto<PRE_EJECUCION_PRESUPUESTARIA> result = new ResultDto<PRE_EJECUCION_PRESUPUESTARIA>(null);
            try
            {



                await _context.PRE_EJECUCION_PRESUPUESTARIA.AddAsync(entity);
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

        public async Task<ResultDto<PRE_EJECUCION_PRESUPUESTARIA>> Update(PRE_EJECUCION_PRESUPUESTARIA entity)
        {
            ResultDto<PRE_EJECUCION_PRESUPUESTARIA> result = new ResultDto<PRE_EJECUCION_PRESUPUESTARIA>(null);

            try
            {
                PRE_EJECUCION_PRESUPUESTARIA entityUpdate = await GetByCodigo(entity.CODIGO_EJE_PRESUPUESTARIA);
                if (entityUpdate != null)
                {


                    _context.PRE_EJECUCION_PRESUPUESTARIA.Update(entity);
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

        public async Task<string> Delete(int codigoEjePresupuestaria)
        {

            try
            {
                PRE_EJECUCION_PRESUPUESTARIA entity = await GetByCodigo(codigoEjePresupuestaria);
                if (entity != null)
                {
                    _context.PRE_EJECUCION_PRESUPUESTARIA.Remove(entity);
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
                var last = await _context.PRE_EJECUCION_PRESUPUESTARIA.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_EJE_PRESUPUESTARIA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_EJE_PRESUPUESTARIA + 1;
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
