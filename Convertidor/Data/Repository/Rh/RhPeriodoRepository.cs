using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
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
        public async Task<List<RH_PERIODOS>> GetAll()
        {
            try
            {

                var result = await _context.RH_PERIODOS.DefaultIfEmpty().ToListAsync();
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

