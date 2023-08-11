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

       

        public async Task<List<RH_CONCEPTOS>> GetConceptosByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta)
        {
            try
            {

                    List<RH_V_HISTORICO_MOVIMIENTOS> historico = new List<RH_V_HISTORICO_MOVIMIENTOS>();
                if (codigoPersona > 0)
                {
                    historico = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().Where(h => h.CODIGO_PERSONA == codigoPersona && h.FECHA_NOMINA_MOV >= desde && h.FECHA_NOMINA_MOV <= hasta).OrderByDescending(h => h.FECHA_NOMINA_MOV).ThenBy(h => h.CEDULA).ToListAsync();

                }
                else
                {
                    historico = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().Where(h =>  h.FECHA_NOMINA_MOV >= desde && h.FECHA_NOMINA_MOV <= hasta).OrderByDescending(h => h.FECHA_NOMINA_MOV).ThenBy(h => h.CEDULA).ToListAsync();

                }

                var resumen = from s in historico
                              group s by new { CodigoTipoNomina=s.CODIGO_TIPO_NOMINA,Codigo = s.CODIGO, Denominacion = s.DENOMINACION } into g
                              select new
                              {
                                  g.Key.CodigoTipoNomina,
                                  g.Key.Codigo,
                                  g.Key.Denominacion,

                              };

                List<RH_CONCEPTOS> result = new List<RH_CONCEPTOS>();
                foreach (var item in resumen)
                {
                 
                    var concepto = await _context.RH_CONCEPTOS.DefaultIfEmpty().Where(tn => tn.CODIGO_TIPO_NOMINA==item.CodigoTipoNomina && tn.CODIGO == item.Codigo).FirstOrDefaultAsync();
                    if (concepto != null && result.Where(x=>x.CODIGO == item.Codigo && x.CODIGO_TIPO_NOMINA==item.CodigoTipoNomina).FirstOrDefault() ==null )
                    {
                        result.Add(concepto);
                    }
                }
               

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

