using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreEscalaRepository: IPreEscalaRepository
    {
        private readonly DataContextPre _context;

        public PreEscalaRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_ESCALA> GetByCodigo(int codigoEscala) 
        {
            try 
            {
                var result = await _context.PRE_ESCALA.DefaultIfEmpty().Where(e => e.CODIGO_ESCALA == codigoEscala).FirstOrDefaultAsync();
                return (PRE_ESCALA)result;
            
            }

            catch (Exception ex) 
            {
                var res = ex.Message;
                return null;
            }
        
        }

        public async Task<List<PRE_ESCALA>> GetAll()
        {
            try
            {
                var result = await _context.PRE_ESCALA.DefaultIfEmpty().ToListAsync();
                return result;

            }

            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<PRE_ESCALA>> Add(PRE_ESCALA entity)
        {
            ResultDto<PRE_ESCALA> result = new ResultDto<PRE_ESCALA>(null);
            try
            {



                await _context.PRE_ESCALA.AddAsync(entity);
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

        public async Task<ResultDto<PRE_ESCALA>> Update(PRE_ESCALA entity)
        {
            ResultDto<PRE_ESCALA> result = new ResultDto<PRE_ESCALA>(null);

            try
            {
                PRE_ESCALA entityUpdate = await GetByCodigo(entity.CODIGO_ESCALA);
                if (entityUpdate != null)
                {


                    _context.PRE_ESCALA.Update(entity);
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

        public async Task<string> Delete(int codigoEscala)
        {

            try
            {
                PRE_ESCALA entity = await GetByCodigo(codigoEscala);
                if (entity != null)
                {
                    _context.PRE_ESCALA.Remove(entity);
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
                var last = await _context.PRE_ESCALA.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ESCALA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ESCALA + 1;
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
