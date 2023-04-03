using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
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

    }
}

