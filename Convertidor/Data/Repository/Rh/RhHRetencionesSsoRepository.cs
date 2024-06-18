using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhHRetencionesSsoRepository : IRhHRetencionesSsoRepository
    {
		
        private readonly DataContext _context;

        public RhHRetencionesSsoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_H_RETENCIONES_SSO>> GetByProcesoId(int procesoId)
        {
            try
            {
                var result = await _context.RH_H_RETENCIONES_SSO.DefaultIfEmpty().Where(e => e.PROCESO_ID == procesoId).ToListAsync();
        
                return (List<RH_H_RETENCIONES_SSO>)result; 
            }
            catch (Exception ex)
            {
                //var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_H_RETENCIONES_SSO>> GetByTipoNominaFechaDesdeFachaHasta(int tipoNomina,string fechaDesde,string fechaHasta)
        {
            try
            {

                DateTime desde = Convert.ToDateTime(fechaDesde);
                DateTime hasta = Convert.ToDateTime(fechaHasta);
                var result = await _context.RH_H_RETENCIONES_SSO.DefaultIfEmpty()
                    .Where(e => e.CODIGO_TIPO_NOMINA == tipoNomina && e.FECHA_DESDE == desde && e.FECHA_HASTA == hasta).ToListAsync();


                return (List<RH_H_RETENCIONES_SSO>)result;
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
                var last = await _context.RH_H_RETENCIONES_SSO.DefaultIfEmpty()
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


        public async Task<ResultDto<RH_H_RETENCIONES_SSO>> Add(RH_H_RETENCIONES_SSO entity)
        {

            ResultDto<RH_H_RETENCIONES_SSO> result = new ResultDto<RH_H_RETENCIONES_SSO>(null);
            try
            {

                await _context.RH_H_RETENCIONES_SSO.AddAsync(entity);
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
                    _context.RH_H_RETENCIONES_SSO.RemoveRange(entity);
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

