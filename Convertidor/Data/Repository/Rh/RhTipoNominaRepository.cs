using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhTipoNominaRepository: IRhTipoNominaRepository
    {
		
        private readonly DataContext _context;

        public RhTipoNominaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_TIPOS_NOMINA>> GetAll()
        {
            try
            {

                var result = await _context.RH_TIPOS_NOMINA.DefaultIfEmpty().Where(t=>t.DESCRIPCION!= null).ToListAsync();
                return (List<RH_TIPOS_NOMINA>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

    }
}

