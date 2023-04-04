using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhEducacionRepository: IRhEducacionRepository
    {
		
        private readonly DataContext _context;

        public RhEducacionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_EDUCACION>> GetAll()
        {
            try
            {

                var result = await _context.RH_EDUCACION.DefaultIfEmpty().ToListAsync();
                return (List<RH_EDUCACION>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_EDUCACION>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {

                var result = await _context.RH_EDUCACION.DefaultIfEmpty().Where(e=>e.CODIGO_PERSONA==codigoPersona).ToListAsync();
                return (List<RH_EDUCACION>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

    }
}

