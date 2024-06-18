using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhProcesoDetalleRepository : IRhProcesoDetalleRepository
    {

        private readonly DataContext _context;

        public RhProcesoDetalleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<RH_PROCESOS_DETALLES> GetByCodigo(int codigoProcesoDetalle)
        {
            try
            {
                var result = await _context.RH_PROCESOS_DETALLES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DETALLE_PROCESO == codigoProcesoDetalle).FirstOrDefaultAsync();

                return (RH_PROCESOS_DETALLES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_PROCESOS_DETALLES>> GetByCodigoProceso(int codigoProceso)
        {
            try
            {
                var result = await _context.RH_PROCESOS_DETALLES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_PROCESO == codigoProceso).ToListAsync();

                return (List<RH_PROCESOS_DETALLES>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_PROCESOS_DETALLES>> GetByCodigoProcesoTipoNomina(int codigoProceso, int codigoTipoNomina)
        {
            try
            {
                var result = await _context.RH_PROCESOS_DETALLES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_PROCESO == codigoProceso && e.CODIGO_TIPO_NOMINA==codigoTipoNomina).ToListAsync();

                return (List<RH_PROCESOS_DETALLES>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RH_PROCESOS_DETALLES>> GetAll()
        {
            try
            {
                var result = await _context.RH_PROCESOS_DETALLES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<ResultDto<RH_PROCESOS_DETALLES>> Add(RH_PROCESOS_DETALLES entity)
        {
            ResultDto<RH_PROCESOS_DETALLES> result = new ResultDto<RH_PROCESOS_DETALLES>(null);
            try
            {



                await _context.RH_PROCESOS_DETALLES.AddAsync(entity);
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

        public async Task<ResultDto<RH_PROCESOS_DETALLES>> Update(RH_PROCESOS_DETALLES entity)
        {
            ResultDto<RH_PROCESOS_DETALLES> result = new ResultDto<RH_PROCESOS_DETALLES>(null);

            try
            {
                RH_PROCESOS_DETALLES entityUpdate = await GetByCodigo(entity.CODIGO_PROCESO);
                if (entityUpdate != null)
                {


                    _context.RH_PROCESOS_DETALLES.Update(entity);
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

        public async Task<string> Delete(int codigoAdministrativo)
        {

            try
            {
                RH_PROCESOS_DETALLES entity = await GetByCodigo(codigoAdministrativo);
                if (entity != null)
                {
                    _context.RH_PROCESOS_DETALLES.Remove(entity);
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
                var last = await _context.RH_PROCESOS_DETALLES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DETALLE_PROCESO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DETALLE_PROCESO + 1;
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


