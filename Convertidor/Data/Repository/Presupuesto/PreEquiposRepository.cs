using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreEquiposRepository : IPreEquiposRepository
    {
        private readonly DataContextPre _context;

        public PreEquiposRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_EQUIPOS> GetByCodigo (int codigoEquipo) 
        {
            try
            {
                var result = await _context.PRE_EQUIPOS.DefaultIfEmpty().Where(x => x.CODIGO_EQUIPO == codigoEquipo).FirstOrDefaultAsync();
                return (PRE_EQUIPOS)result;
            }
            catch (Exception ex) 
            {
                var res = ex.Message;
                return null;
            }
        }

        public async Task<List<PRE_EQUIPOS>> GetAll() 
        {
            try
            {
                var result = await _context.PRE_EQUIPOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }
        }

        public async Task<ResultDto<PRE_EQUIPOS>> Add(PRE_EQUIPOS entity)
        {
            ResultDto<PRE_EQUIPOS> result = new ResultDto<PRE_EQUIPOS>(null);
            try
            {



                await _context.PRE_EQUIPOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_EQUIPOS>> Update(PRE_EQUIPOS entity)
        {
            ResultDto<PRE_EQUIPOS> result = new ResultDto<PRE_EQUIPOS>(null);

            try
            {
                PRE_EQUIPOS entityUpdate = await GetByCodigo(entity.CODIGO_EQUIPO);
                if (entityUpdate != null)
                {


                    _context.PRE_EQUIPOS.Update(entity);
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

        public async Task<string> Delete(int codigoEquipo)
        {

            try
            {
                PRE_EQUIPOS entity = await GetByCodigo(codigoEquipo);
                if (entity != null)
                {
                    _context.PRE_EQUIPOS.Remove(entity);
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
                var last = await _context.PRE_EQUIPOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_EQUIPO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_EQUIPO + 1;
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
