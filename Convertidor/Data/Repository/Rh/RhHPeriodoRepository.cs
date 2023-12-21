using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhHPeriodoRepository: IRhHPeriodoRepository
    {
		
        private readonly DataContext _context;

        public RhHPeriodoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_H_PERIODOS>> GetAll(PeriodoFilterDto filter)
        {
            

            try
            {
                if (filter.Year<=0  )
                {
                    var lastPeriodo = await _context.RH_H_PERIODOS.DefaultIfEmpty().OrderByDescending(p=>p.FECHA_NOMINA).FirstOrDefaultAsync();
                    if (lastPeriodo != null)
                    {
                        filter.Year = lastPeriodo.FECHA_NOMINA.Year;
                    }
                    else {
                        filter.Year = DateTime.Now.Year;
                    }

                }

                List<RH_H_PERIODOS> result = new List<RH_H_PERIODOS>();
                if (filter.Year>0 && filter.CodigoTipoNomina > 0)
                {
                    result = await _context.RH_H_PERIODOS.DefaultIfEmpty().Where(p=> p.FECHA_NOMINA.Year==filter.Year && p.CODIGO_TIPO_NOMINA==filter.CodigoTipoNomina).ToListAsync();
                }
                if (filter.Year > 0 && filter.CodigoTipoNomina <= 0)
                {
                    result = await _context.RH_H_PERIODOS.DefaultIfEmpty().Where(p => p.FECHA_NOMINA.Year == filter.Year ).ToListAsync();
                }


                return (List<RH_H_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RH_H_PERIODOS>> GetByYear(int  ano)
        {
            try
            {

                var result = await _context.RH_H_PERIODOS.DefaultIfEmpty().Where(p => p.FECHA_NOMINA.Year == ano).OrderByDescending(p=>p.FECHA_NOMINA).ToListAsync();
                return (List<RH_H_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_H_PERIODOS>> GetByTipoNomina(int tipoNomina)
        {
            try
            {

                var result = await _context.RH_H_PERIODOS.DefaultIfEmpty().Where(p=> p.CODIGO_TIPO_NOMINA== tipoNomina).ToListAsync();
                return (List<RH_H_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<RH_H_PERIODOS> GetByCodigo(int codigoPeriodo)
        {
            try
            {
                var result = await _context.RH_H_PERIODOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_H_PERIODO == codigoPeriodo)
                    .OrderBy(x => x.FECHA_NOMINA).FirstOrDefaultAsync();

                return (RH_H_PERIODOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ResultDto<RH_H_PERIODOS>> Add(RH_H_PERIODOS entity)
        {
            ResultDto<RH_H_PERIODOS> result = new ResultDto<RH_H_PERIODOS>(null);
            try
            {



                await _context.RH_H_PERIODOS.AddAsync(entity);
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

        public async Task<ResultDto<RH_H_PERIODOS>> Update(RH_H_PERIODOS entity)
        {
            ResultDto<RH_H_PERIODOS> result = new ResultDto<RH_H_PERIODOS>(null);

            try
            {
                RH_H_PERIODOS entityUpdate = await GetByCodigo(entity.CODIGO_H_PERIODO);
                if (entityUpdate != null)
                {


                    _context.RH_H_PERIODOS.Update(entity);
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

        public async Task<string> Delete(int codigoHPeriodo)
        {

            try
            {
                RH_H_PERIODOS entity = await GetByCodigo(codigoHPeriodo);
                if (entity != null)
                {
                    _context.RH_H_PERIODOS.Remove(entity);
                    _context.SaveChanges();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }



        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.RH_H_PERIODOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_H_PERIODO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_H_PERIODO + 1;
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


