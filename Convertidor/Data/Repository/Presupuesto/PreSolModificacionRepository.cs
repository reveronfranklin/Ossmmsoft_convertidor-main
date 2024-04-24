using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class PreSolModificacionRepository: IPreSolModificacionRepository
    {
		
        private readonly DataContextPre _context;

        public PreSolModificacionRepository(DataContextPre context)
        {
            _context = context;
        }
      
        public async Task<PRE_SOL_MODIFICACION> GetByCodigo(int codigoSolModificacion)
        {
            try
            {
                var result = await _context.PRE_SOL_MODIFICACION.DefaultIfEmpty().Where(e => e.CODIGO_SOL_MODIFICACION == codigoSolModificacion).FirstOrDefaultAsync();

                return (PRE_SOL_MODIFICACION)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

    
        public async Task<List<PRE_SOL_MODIFICACION>> GetAll()
        {
            try
            {
                var result = await _context.PRE_SOL_MODIFICACION.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_SOL_MODIFICACION>> GetByPresupuesto(int codigoPresupuesto)
        {
            try
            {
                var result = await _context.PRE_SOL_MODIFICACION.DefaultIfEmpty().Where(x=> x.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<PRE_SOL_MODIFICACION>> Add(PRE_SOL_MODIFICACION entity)
        {
            ResultDto<PRE_SOL_MODIFICACION> result = new ResultDto<PRE_SOL_MODIFICACION>(null);
            try
            {



                await _context.PRE_SOL_MODIFICACION.AddAsync(entity);
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

        public async Task<ResultDto<PRE_SOL_MODIFICACION>> Update(PRE_SOL_MODIFICACION entity)
        {
            ResultDto<PRE_SOL_MODIFICACION> result = new ResultDto<PRE_SOL_MODIFICACION>(null);

            try
            {
                PRE_SOL_MODIFICACION entityUpdate = await GetByCodigo(entity.CODIGO_SOL_MODIFICACION);
                if (entityUpdate != null)
                {


                    _context.PRE_SOL_MODIFICACION.Update(entity);
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

        public async Task<string> Delete(int codigoSolModificacion)
        {

            try
            {
                PRE_SOL_MODIFICACION entity = await GetByCodigo(codigoSolModificacion);
                if (entity != null)
                {
                    _context.PRE_SOL_MODIFICACION.Remove(entity);
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
                var last = await _context.PRE_SOL_MODIFICACION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_SOL_MODIFICACION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_SOL_MODIFICACION + 1;
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

