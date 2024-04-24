using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreModificacionRepository : IPreModificacionRepository
    {
        private readonly DataContextPre _context;

        public PreModificacionRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_MODIFICACION> GetByCodigo(int codigoModificacion)
        {
            try
            {
                var result = await _context.PRE_MODIFICACION.DefaultIfEmpty().Where(e => e.CODIGO_MODIFICACION == codigoModificacion).FirstOrDefaultAsync();

                return (PRE_MODIFICACION)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        
        public async Task<PRE_MODIFICACION> GetByCodigoSolicitud(int codigoSolicitud)
        {
            try
            {
                var result = await _context.PRE_MODIFICACION.DefaultIfEmpty().Where(e => e.CODIGO_SOL_MODIFICACION == codigoSolicitud).FirstOrDefaultAsync();

                return (PRE_MODIFICACION)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_MODIFICACION>> GetAll()
        {
            try
            {
                var result = await _context.PRE_MODIFICACION.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<PRE_MODIFICACION>> Add(PRE_MODIFICACION entity)
        {
            ResultDto<PRE_MODIFICACION> result = new ResultDto<PRE_MODIFICACION>(null);
            try
            {



                await _context.PRE_MODIFICACION.AddAsync(entity);
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

        public async Task<ResultDto<PRE_MODIFICACION>> Update(PRE_MODIFICACION entity)
        {
            ResultDto<PRE_MODIFICACION> result = new ResultDto<PRE_MODIFICACION>(null);

            try
            {
                PRE_MODIFICACION entityUpdate = await GetByCodigo(entity.CODIGO_MODIFICACION);
                if (entityUpdate != null)
                {


                    _context.PRE_MODIFICACION.Update(entity);
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

        public async Task<string> Delete(int codigoModificacion)
        {

            try
            {
                PRE_MODIFICACION entity = await GetByCodigo(codigoModificacion);
                if (entity != null)
                {
                    _context.PRE_MODIFICACION.Remove(entity);
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
                var last = await _context.PRE_MODIFICACION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_MODIFICACION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_MODIFICACION + 1;
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
