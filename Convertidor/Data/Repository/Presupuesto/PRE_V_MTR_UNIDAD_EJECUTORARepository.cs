using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_V_MTR_UNIDAD_EJECUTORARepository : IPRE_V_MTR_UNIDAD_EJECUTORARepository
    {
	
        private readonly DataContextPre _context;

        public PRE_V_MTR_UNIDAD_EJECUTORARepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<List<PRE_V_MTR_UNIDAD_EJECUTORA>> GetAll()
        {
            try
            {

                var result = await _context.PRE_V_MTR_UNIDAD_EJECUTORA.DefaultIfEmpty().ToListAsync();
                return (List<PRE_V_MTR_UNIDAD_EJECUTORA>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_V_MTR_UNIDAD_EJECUTORA>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {
                var puc = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty().Where( x=>x.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();
                var lista = from s in puc
                    group s by new
                    {
                        CodigoPresupuesto = s.CODIGO_PRESUPUESTO,
                        CodigoIcp = s.CODIGO_ICP,
                        CodigoSector = s.CODIGO_SECTOR,
                        CodigoPrograma = s.CODIGO_PROGRAMA,
                        CodigoSubPrograma = s.CODIGO_SUBPROGRAMA,
                        CodigoProyecto = s.CODIGO_PROYECTO,
                        CodigoActividad = s.CODIGO_ACTIVIDAD,
                        UnidadEjecutora=s.UNIDAD_EJECUTORA

                    } into g
                    select new PRE_V_MTR_UNIDAD_EJECUTORA()
                    {
                        CODIGO_PRESUPUESTO = (int)g.Key.CodigoPresupuesto,
                        CODIGO_IPC = g.Key.CodigoIcp,
                        CODIGO_ICP_CONCAT = $"{g.Key.CodigoSector}-{g.Key.CodigoPrograma}-{g.Key.CodigoSubPrograma}-{g.Key.CodigoProyecto}-{g.Key.CodigoActividad}",
                        UNIDAD_EJECUTORA = g.Key.UnidadEjecutora 

                    };
                var result = lista.ToList();    
                
                //var result = await _context.PRE_V_MTR_UNIDAD_EJECUTORA.DefaultIfEmpty().Where( x=>x.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();
                return (List<PRE_V_MTR_UNIDAD_EJECUTORA>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        


    }
}

