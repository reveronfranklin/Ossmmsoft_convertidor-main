using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhConceptosRepository: IRhConceptosRepository
    {
		
        private readonly DataContext _context;

        public RhConceptosRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_CONCEPTOS>> GetAll()
        {
            try
            {

                var result = await _context.RH_CONCEPTOS.DefaultIfEmpty().ToListAsync();
                return (List<RH_CONCEPTOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RH_CONCEPTOS>> GeTByTipoNomina(int codTipoNomina)
        {
            try
            {

                var result = await _context.RH_CONCEPTOS.DefaultIfEmpty().Where(tn=>tn.CODIGO_TIPO_NOMINA== codTipoNomina).ToListAsync();
                return (List<RH_CONCEPTOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

    }
}

