using System;
using Convertidor.Data.Entities.Presupuesto;
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

                var result = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().ToListAsync() ;
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

                var result = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().Where(h=>h.CODIGO_PERSONA== codigoPersona).OrderByDescending(h=>h.FECHA_NOMINA_MOV).ThenBy(h=>h.CEDULA).ToListAsync();
                return (List<RH_V_HISTORICO_MOVIMIENTOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<RH_V_HISTORICO_MOVIMIENTOS> GetPrimerMovimientoByCodigoPersona(int codigoPersona)
        {
            try
            {

                var result = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().Where(h => h.CODIGO_PERSONA == codigoPersona).OrderBy(h => h.FECHA_NOMINA_MOV).FirstOrDefaultAsync();
                return (RH_V_HISTORICO_MOVIMIENTOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }



        public async Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetByTipoNominaPeriodo(int tipoNomina,int codigoPeriodo)
        {
            try
            {

                var result = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().Where(h => h.CODIGO_TIPO_NOMINA == tipoNomina && h.CODIGO_PERIODO==codigoPeriodo).OrderByDescending(h => h.FECHA_NOMINA_MOV).ToListAsync();
                return (List<RH_V_HISTORICO_MOVIMIENTOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetByFechaNomina(DateTime desde, DateTime hasta)
        {
            try
            {

                var result = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().Where(h => h.FECHA_NOMINA_MOV >= desde && h.FECHA_NOMINA_MOV <= hasta)
                    .ToListAsync();
                return (List<RH_V_HISTORICO_MOVIMIENTOS>)result;
            }
            catch (Exception ex)
            {
              var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetByFechaNominaPersona(DateTime desde, DateTime hasta,int idPersona)
        {
            try
            {

                var result = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().Where(h => h.FECHA_NOMINA_MOV >= desde && h.FECHA_NOMINA_MOV <= hasta && h.CODIGO_PERSONA== idPersona)
                    .ToListAsync();
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

