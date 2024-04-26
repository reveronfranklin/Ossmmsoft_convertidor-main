
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreNivelesPucRepository : IPreNivelesPucRepository
    {
        private readonly DataContextPre _context;

        public PreNivelesPucRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_NIVELES_PUC> GetByCodigo(int codigogRUPO)
        {
            try
            {
                var result = await _context.PRE_NIVELES_PUC.DefaultIfEmpty().Where(e => e.CODIGO_GRUPO == codigogRUPO).FirstOrDefaultAsync();

                return (PRE_NIVELES_PUC)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_NIVELES_PUC>> GetAll()
        {
            try
            {
                var result = await _context.PRE_NIVELES_PUC.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<PRE_NIVELES_PUC>> Add(PRE_NIVELES_PUC entity)
        {
            ResultDto<PRE_NIVELES_PUC> result = new ResultDto<PRE_NIVELES_PUC>(null);
            try
            {



                await _context.PRE_NIVELES_PUC.AddAsync(entity);
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

        public async Task<ResultDto<PRE_NIVELES_PUC>> Update(PRE_NIVELES_PUC entity)
        {
            ResultDto<PRE_NIVELES_PUC> result = new ResultDto<PRE_NIVELES_PUC>(null);

            try
            {
                PRE_NIVELES_PUC entityUpdate = await GetByCodigo(entity.CODIGO_GRUPO);
                if (entityUpdate != null)
                {


                    _context.PRE_NIVELES_PUC.Update(entity);
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

        public async Task<string> Delete(int codigoGrupo)
        {

            try
            {
                PRE_NIVELES_PUC entity = await GetByCodigo(codigoGrupo);
                if (entity != null)
                {
                    _context.PRE_NIVELES_PUC.Remove(entity);
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
                var last = await _context.PRE_NIVELES_PUC.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_GRUPO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_GRUPO + 1;
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
