using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_ASIGNACIONESRepository : IPRE_ASIGNACIONESRepository
    {
	

        private readonly DataContextPre _context;

        public PRE_ASIGNACIONESRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<bool> PresupuestoExiste(int codPresupuesto)
        {

            bool result;
            try
            {

                var asignaciones = await _context.PRE_ASIGNACIONES.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == codPresupuesto).FirstOrDefaultAsync();
                if (asignaciones != null)
                {
                    result = true;
                }
                else {
                    result = false;
                }


                return result;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

    }
}

