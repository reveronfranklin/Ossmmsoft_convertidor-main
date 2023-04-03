using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhPersonasRepository: IRhPersonasRepository
    {
		
        private readonly DataContext _context;

        public RhPersonasRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_PERSONAS>> GetAll()
        {
            try
            {

                var result = await _context.RH_PERSONAS.DefaultIfEmpty().ToListAsync();
                return (List<RH_PERSONAS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_PERSONAS> GetCedula(int cedula)
        {
            try
            {

                var result = await _context.RH_PERSONAS.DefaultIfEmpty().Where(p=> p.CEDULA==cedula).FirstOrDefaultAsync();
                return (RH_PERSONAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_PERSONAS> GetCodogoPersona(int codigoPersona)
        {
            try
            {

                var result = await _context.RH_PERSONAS.DefaultIfEmpty().Where(p => p.CODIGO_PERSONA == codigoPersona).FirstOrDefaultAsync();
                return (RH_PERSONAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


    }
}

