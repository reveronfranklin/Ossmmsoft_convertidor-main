using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_V_MTR_DENOMINACION_PUCRepository: IPRE_V_MTR_DENOMINACION_PUCRepository
    {
	
        private readonly DataContextPre _context;

        public PRE_V_MTR_DENOMINACION_PUCRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<List<PRE_V_MTR_DENOMINACION_PUC>> GetAll()
        {
            try
            {

                var result = await _context.PRE_V_MTR_DENOMINACION_PUC.DefaultIfEmpty().ToListAsync();
                return (List<PRE_V_MTR_DENOMINACION_PUC>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_V_MTR_DENOMINACION_PUC>> GetByCodigoPresupuesto(int codigoPresupuesto)
        {
            try
            {

                var puc = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty().Where(x=>x.CODIGO_PRESUPUESTO== codigoPresupuesto).ToListAsync();
                
                       var lista = from s in puc
                            group s by new
                            {
                                CodigoPresupuesto = s.CODIGO_PRESUPUESTO,
                                CodigoPuc = s.CODIGO_PUC,
                                CodigoGrupo = s.CODIGO_GRUPO,
                                CodigoNivel1 = s.CODIGO_NIVEL1,
                                CodigoNivel2 = s.CODIGO_NIVEL2,
                                CodigoNivel3 = s.CODIGO_NIVEL3,
                                CodigoNivel4 = s.CODIGO_NIVEL4,
                                CodigoNivel5 = s.CODIGO_NIVEL5,
                                Denominacion=s.DENOMINACION
                              

                            } into g
                            select new PRE_V_MTR_DENOMINACION_PUC()
                            {

                                CODIGO_PRESUPUESTO = (int)g.Key.CodigoPresupuesto,
                                CODIGO_PUC = g.Key.CodigoPuc,
                                CODIGO_PUC_CONCAT = $"{g.Key.CodigoGrupo}.{g.Key.CodigoNivel1}.{g.Key.CodigoNivel2}.{g.Key.CodigoNivel3}.{g.Key.CodigoNivel4}.{g.Key.CodigoNivel5}",
                                DENOMINACION_PUC = g.Key.Denominacion 
                               

                            };
                       var result = lista.ToList();       
                
                //var result = await _context.PRE_V_MTR_DENOMINACION_PUC.DefaultIfEmpty().Where(x=>x.CODIGO_PRESUPUESTO== codigoPresupuesto).ToListAsync();
                return (List<PRE_V_MTR_DENOMINACION_PUC>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


    }
}

