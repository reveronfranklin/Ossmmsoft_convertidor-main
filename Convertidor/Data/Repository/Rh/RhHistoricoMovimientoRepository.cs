using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhHistoricoMovimientoRepository: IRhHistoricoMovimientoRepository
    {
		

        private readonly DataContext _context;

        public RhHistoricoMovimientoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetAll()
        {
            try
            {

                var result = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().ToListAsync();
                return (List<RH_V_HISTORICO_MOVIMIENTOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {

                var result = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().Where(h=>h.CODIGO_PERSONA== codigoPersona).OrderByDescending(h=>h.FECHA_NOMINA_MOV).ToListAsync();
                return (List<RH_V_HISTORICO_MOVIMIENTOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }



    }
}

