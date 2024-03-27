using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreAsignacionesDetalleRepository : IPreAsignacionesDetalleRepository
    {


        private readonly DataContextPre _context;

        public PreAsignacionesDetalleRepository(DataContextPre context)
        {
            _context = context;
        }


        public async Task<PRE_ASIGNACIONES_DETALLE> GetByCodigo(int codigo)
        {
            try
            {
                var result = await _context.PRE_ASIGNACIONES_DETALLE.DefaultIfEmpty()
                    .Where(e => e.CODIGO_ASIGNACION_DETALLE == codigo).FirstOrDefaultAsync();

                return (PRE_ASIGNACIONES_DETALLE)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<PRE_ASIGNACIONES_DETALLE>> GetAll()
        {
            try
            {
                var result = await _context.PRE_ASIGNACIONES_DETALLE.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<PRE_ASIGNACIONES_DETALLE>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {
                var result = await _context.PRE_ASIGNACIONES_DETALLE.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<PRE_ASIGNACIONES_DETALLE>> GetAllByAsignacion(int codigoAsignacion)
        {
            try
            {
                var result = await _context.PRE_ASIGNACIONES_DETALLE.DefaultIfEmpty()
                    .Where(x => x.CODIGO_ASIGNACION == codigoAsignacion).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<PRE_ASIGNACIONES_DETALLE>> Add(PRE_ASIGNACIONES_DETALLE entity)
        {
            ResultDto<PRE_ASIGNACIONES_DETALLE> result = new ResultDto<PRE_ASIGNACIONES_DETALLE>(null);
            try
            {



                await _context.PRE_ASIGNACIONES_DETALLE.AddAsync(entity);
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

        public async Task<ResultDto<PRE_ASIGNACIONES_DETALLE>> Update(PRE_ASIGNACIONES_DETALLE entity)
        {
            ResultDto<PRE_ASIGNACIONES_DETALLE> result = new ResultDto<PRE_ASIGNACIONES_DETALLE>(null);

            try
            {
                PRE_ASIGNACIONES_DETALLE entityUpdate = await GetByCodigo(entity.CODIGO_ASIGNACION_DETALLE);
                if (entityUpdate != null)
                {
                    _context.PRE_ASIGNACIONES_DETALLE.Update(entity);
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

        public async Task<string> Delete(int codigoAsignacionDetalle)
        {

            try
            {
                PRE_ASIGNACIONES_DETALLE entity = await GetByCodigo(codigoAsignacionDetalle);
                if (entity != null)
                {
                    _context.PRE_ASIGNACIONES_DETALLE.Remove(entity);
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
                var last = await _context.PRE_ASIGNACIONES_DETALLE.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ASIGNACION_DETALLE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ASIGNACION_DETALLE + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }
        
        public async Task<bool> AsignacionExiste(int codigoAsignacion)
        {

            bool result;
            try
            {

                var asignaciones = await _context.PRE_ASIGNACIONES_DETALLE.DefaultIfEmpty()
                    .Where(x => x.CODIGO_ASIGNACION == codigoAsignacion).FirstOrDefaultAsync();
                if (asignaciones != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }


                return result;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

    }
}

