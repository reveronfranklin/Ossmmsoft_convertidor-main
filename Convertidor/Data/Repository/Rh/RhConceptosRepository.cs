using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
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
        public async Task<RH_CONCEPTOS> GetByCodigo(int codigoConcepto)
        {
            try
            {

                var result = await _context.RH_CONCEPTOS.DefaultIfEmpty().Where(tn => tn.CODIGO_CONCEPTO == codigoConcepto).FirstOrDefaultAsync();
                return (RH_CONCEPTOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_CONCEPTOS> GetCodigoString(string codigo)
        {
            try
            {

                var result = await _context.RH_CONCEPTOS.DefaultIfEmpty().Where(tn => tn.CODIGO == codigo).FirstOrDefaultAsync();
                return (RH_CONCEPTOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<RH_CONCEPTOS> GetByExtra1(string extra1)
        {
            try
            {

                var result = await _context.RH_CONCEPTOS.DefaultIfEmpty().Where(tn => tn.EXTRA1 == extra1).FirstOrDefaultAsync();
                return (RH_CONCEPTOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<RH_CONCEPTOS> GetByCodigoTipoNomina(int codigoConcepto,int codigoTipoNomina)
        {
            try
            {

                var result = await _context.RH_CONCEPTOS.DefaultIfEmpty().Where(tn => tn.CODIGO_TIPO_NOMINA == codigoTipoNomina && tn.CODIGO_CONCEPTO == codigoConcepto).FirstOrDefaultAsync();
                return (RH_CONCEPTOS)result;
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

        public async Task<ResultDto<RH_CONCEPTOS>> Add(RH_CONCEPTOS entity)
        {
            ResultDto<RH_CONCEPTOS> result = new ResultDto<RH_CONCEPTOS>(null);
            try
            {

                await _context.RH_CONCEPTOS.AddAsync(entity);
                _context.SaveChanges();


                result.Data = entity;
                result.IsValid = true;
                result.Message = "";
                return result;


            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }

        public async Task<ResultDto<RH_CONCEPTOS>> Update(RH_CONCEPTOS entity)
        {
            ResultDto<RH_CONCEPTOS> result = new ResultDto<RH_CONCEPTOS>(null);

            try
            {
                RH_CONCEPTOS entityUpdate = await GetByCodigo(entity.CODIGO_CONCEPTO);
                if (entityUpdate != null)
                {


                    _context.RH_CONCEPTOS.Update(entity);
                    _context.SaveChanges();
                    result.Data = entity;
                    result.IsValid = true;
                    result.Message = "";

                }
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }

        //public async Task<string> Delete(int CodigoConcepto)
        //{

        //    try
        //    {
        //        RH_CONCEPTOS entity = await GetByCodigo(CodigoConcepto);
        //        if (entity != null)
        //        {
        //            _context.RH_CONCEPTOS.Remove(entity);
        //            _context.SaveChanges();
        //        }
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //}

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.RH_CONCEPTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CONCEPTO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CONCEPTO + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }

        }

    }
}


    


