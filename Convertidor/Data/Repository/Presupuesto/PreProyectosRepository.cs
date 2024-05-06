using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreProyectosRepository: IPreProyectosRepository
    {
        private readonly DataContextPre _context;

        public PreProyectosRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_PROYECTOS> GetByCodigo (int codigoProyecto) 
        {
            try 
            {
               var result = await _context.PRE_PROYECTOS.DefaultIfEmpty().Where(e => e.CODIGO_PROYECTO == codigoProyecto).FirstOrDefaultAsync();
               return (PRE_PROYECTOS)result;
            
            }
            
            catch (Exception ex) 
            {
                var res = ex.Message;
                return null;

            }
        }

        public async Task<List<PRE_PROYECTOS>> GetAll() 
        {
            try 
            {
               var result = await _context.PRE_PROYECTOS.DefaultIfEmpty().ToListAsync();
               return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<PRE_PROYECTOS>> Add(PRE_PROYECTOS entity)
        {
            ResultDto<PRE_PROYECTOS> result = new ResultDto<PRE_PROYECTOS>(null);
            try
            {



                await _context.PRE_PROYECTOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_PROYECTOS>> Update(PRE_PROYECTOS entity)
        {
            ResultDto<PRE_PROYECTOS> result = new ResultDto<PRE_PROYECTOS>(null);

            try
            {
                PRE_PROYECTOS entityUpdate = await GetByCodigo(entity.CODIGO_PROYECTO);
                if (entityUpdate != null)
                {


                    _context.PRE_PROYECTOS.Update(entity);
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

        public async Task<string> Delete(int codigoProyecto)
        {

            try
            {
                PRE_PROYECTOS entity = await GetByCodigo(codigoProyecto);
                if (entity != null)
                {
                    _context.PRE_PROYECTOS.Remove(entity);
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
                var last = await _context.PRE_PROYECTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PROYECTO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PROYECTO + 1;
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
