using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhHRetencionesIncesRepository : IRhHRetencionesIncesRepository
    {
		
        private readonly DataContext _context;

        public RhHRetencionesIncesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_H_RETENCIONES_INCES>> GetByProcesoId(int procesoId)
        {
            try
            {
                var result = await _context.RH_H_RETENCIONES_INCES.DefaultIfEmpty().Where(e => e.PROCESO_ID == procesoId).ToListAsync();
        
                return (List<RH_H_RETENCIONES_INCES>)result; 
            }
            catch (Exception ex)
            {
                //var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_H_RETENCIONES_INCES>> GetByTipoNominaFechaDesdeFachaHasta(int tipoNomina,string fechaDesde,string fechaHasta)
        {
            try
            {
                DateTime desde = Convert.ToDateTime(fechaDesde);
                DateTime hasta = Convert.ToDateTime(fechaHasta);
                var result = await _context.RH_H_RETENCIONES_INCES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_TIPO_NOMINA == tipoNomina && e.FECHA_DESDE==desde  && e.FECHA_HASTA==hasta).ToListAsync();

                return (List<RH_H_RETENCIONES_INCES>)result;
            }
            catch (Exception ex)
            {
                //var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.RH_H_RETENCIONES_INCES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_RETENCION_APORTE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_RETENCION_APORTE + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }


        public async Task<ResultDto<RH_H_RETENCIONES_INCES>> Add(RH_H_RETENCIONES_INCES entity)
        {

            ResultDto<RH_H_RETENCIONES_INCES> result = new ResultDto<RH_H_RETENCIONES_INCES>(null);
            try
            {

                await _context.RH_H_RETENCIONES_INCES.AddAsync(entity);
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

        public async Task<string> Delete(int procesoId)
        {

            try
            {
                var entity = await GetByProcesoId(procesoId);
                if (entity.Count>0)
                {
                    _context.RH_H_RETENCIONES_INCES.RemoveRange(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }

    }
}

