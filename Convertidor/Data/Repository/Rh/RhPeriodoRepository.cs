using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhPeriodoRepository: IRhPeriodoRepository
    {
		
        private readonly DataContext _context;

        public RhPeriodoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_PERIODOS>> GetAll(PeriodoFilterDto filter)
        {
            

            try
            {
                if (filter.Year<=0  )
                {
                    var lastPeriodo = await _context.RH_PERIODOS.DefaultIfEmpty().OrderByDescending(p=>p.FECHA_NOMINA).FirstOrDefaultAsync();
                    if (lastPeriodo != null)
                    {
                        filter.Year = lastPeriodo.FECHA_NOMINA.Year;
                    }
                    else {
                        filter.Year = DateTime.Now.Year;
                    }

                }

                List<RH_PERIODOS> result = new List<RH_PERIODOS>();
                if (filter.Year>0 && filter.CodigoTipoNomina > 0)
                {
                    result = await _context.RH_PERIODOS.DefaultIfEmpty().Where(p=> p.FECHA_NOMINA.Year==filter.Year && p.CODIGO_TIPO_NOMINA==filter.CodigoTipoNomina).ToListAsync();
                }
                if (filter.Year > 0 && filter.CodigoTipoNomina <= 0)
                {
                    result = await _context.RH_PERIODOS.DefaultIfEmpty().Where(p => p.FECHA_NOMINA.Year == filter.Year ).ToListAsync();
                }


                return (List<RH_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RH_PERIODOS>> GetByYear(int  ano)
        {
            try
            {

                var result = await _context.RH_PERIODOS.DefaultIfEmpty().Where(p => p.FECHA_NOMINA.Year == ano).OrderByDescending(p=>p.FECHA_NOMINA).ToListAsync();
                return (List<RH_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_PERIODOS>> GetByTipoNomina(int tipoNomina)
        {
            try
            {

                var result = await _context.RH_PERIODOS.DefaultIfEmpty().Where(p=> p.CODIGO_TIPO_NOMINA== tipoNomina).ToListAsync();
                return (List<RH_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<RH_PERIODOS> GetByCodigo(int codigoPeriodo)
        {
            try
            {
                var result = await _context.RH_PERIODOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_PERIODO == codigoPeriodo)
                    .OrderBy(x => x.FECHA_NOMINA).FirstOrDefaultAsync();

                return (RH_PERIODOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ResultDto<RH_PERIODOS>> Add(RH_PERIODOS entity)
        {
            ResultDto<RH_PERIODOS> result = new ResultDto<RH_PERIODOS>(null);
            try
            {



                await _context.RH_PERIODOS.AddAsync(entity);
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

        public async Task<ResultDto<RH_PERIODOS>> Update(RH_PERIODOS entity)
        {
            ResultDto<RH_PERIODOS> result = new ResultDto<RH_PERIODOS>(null);

            try
            {
                RH_PERIODOS entityUpdate = await GetByCodigo(entity.CODIGO_PERIODO);
                if (entityUpdate != null)
                {


                    _context.RH_PERIODOS.Update(entity);
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

        public async Task<string> Delete(int codigoPeriodo)
        {

            try
            {
                RH_PERIODOS entity = await GetByCodigo(codigoPeriodo);
                if (entity != null)
                {
                    _context.RH_PERIODOS.Remove(entity);
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
                var last = await _context.RH_PERIODOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PERIODO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PERIODO + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }


        public async Task<RH_PERIODOS> GetPeriodoAbierto(int codigoTipoNomina, string tipoNomina)
        {
            try
            {
                
                var result = await _context.RH_PERIODOS.DefaultIfEmpty()
                 .Where(e => e.CODIGO_TIPO_NOMINA == codigoTipoNomina && e.TIPO_NOMINA == tipoNomina && e.FECHA_CIERRE==null)
                        .OrderByDescending(x => x.FECHA_NOMINA).FirstOrDefaultAsync();
                return (RH_PERIODOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
    }
}


