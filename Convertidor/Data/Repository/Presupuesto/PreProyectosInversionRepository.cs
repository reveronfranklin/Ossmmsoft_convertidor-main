using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreProyectosInversionRepository: IPreProyectosInversionRepository
    {
        private readonly DataContextPre _context;

        public PreProyectosInversionRepository(DataContextPre context)
        {
            _context = context;
        }

       public async Task<PRE_PROYECTOS_INVERSION> GetByCodigo(int codigoProyectoInv) 
       {
            try
            {
                var result = await _context.PRE_PROYECTOS_INVERSION.DefaultIfEmpty().Where(e => e.CODIGO_PROYECTO_INV == codigoProyectoInv).FirstOrDefaultAsync();
                return (PRE_PROYECTOS_INVERSION)result;
            }
            catch (Exception ex) 
            {
                var res = ex.Message;
                return null;
            }
       }

       public async Task<List<PRE_PROYECTOS_INVERSION>> GetAll() 
       {
            try 
            {
                var result = await _context.PRE_PROYECTOS_INVERSION.DefaultIfEmpty().ToListAsync();
                return result;
            }
            
            catch (Exception ex) 
            {
                var res = ex.Message;
                return null;
            }
       }

        public async Task<ResultDto<PRE_PROYECTOS_INVERSION>> Add(PRE_PROYECTOS_INVERSION entity)
        {
            ResultDto<PRE_PROYECTOS_INVERSION> result = new ResultDto<PRE_PROYECTOS_INVERSION>(null);
            try
            {



                await _context.PRE_PROYECTOS_INVERSION.AddAsync(entity);
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

        public async Task<ResultDto<PRE_PROYECTOS_INVERSION>> Update(PRE_PROYECTOS_INVERSION entity)
        {
            ResultDto<PRE_PROYECTOS_INVERSION> result = new ResultDto<PRE_PROYECTOS_INVERSION>(null);

            try
            {
                PRE_PROYECTOS_INVERSION entityUpdate = await GetByCodigo(entity.CODIGO_PROYECTO_INV);
                if (entityUpdate != null)
                {


                    _context.PRE_PROYECTOS_INVERSION.Update(entity);
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

        public async Task<string> Delete(int codigoProyectoInv)
        {

            try
            {
                PRE_PROYECTOS_INVERSION entity = await GetByCodigo(codigoProyectoInv);
                if (entity != null)
                {
                    _context.PRE_PROYECTOS_INVERSION.Remove(entity);
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
                var last = await _context.PRE_PROYECTOS_INVERSION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PROYECTO_INV)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PROYECTO_INV + 1;
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
