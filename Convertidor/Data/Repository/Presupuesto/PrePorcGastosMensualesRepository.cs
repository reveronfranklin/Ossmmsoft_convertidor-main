
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PrePorcGastosMensualesRepository : IPrePorcGastosMensualesRepository
    {
        private readonly DataContextPre _context;

        public PrePorcGastosMensualesRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_PORC_GASTOS_MENSUALES> GetByCodigo(int codigoPorGastosMes)
        {
            try
            {
                var result = await _context.PRE_PORC_GASTOS_MENSUALES.DefaultIfEmpty().Where(e => e.CODIGO_POR_GASTOS_MES == codigoPorGastosMes).FirstOrDefaultAsync();

                return (PRE_PORC_GASTOS_MENSUALES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_PORC_GASTOS_MENSUALES>> GetAll()
        {
            try
            {
                var result = await _context.PRE_PORC_GASTOS_MENSUALES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<PRE_PORC_GASTOS_MENSUALES>> Add(PRE_PORC_GASTOS_MENSUALES entity)
        {
            ResultDto<PRE_PORC_GASTOS_MENSUALES> result = new ResultDto<PRE_PORC_GASTOS_MENSUALES>(null);
            try
            {



                await _context.PRE_PORC_GASTOS_MENSUALES.AddAsync(entity);
                await _context.SaveChangesAsync();


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

        public async Task<ResultDto<PRE_PORC_GASTOS_MENSUALES>> Update(PRE_PORC_GASTOS_MENSUALES entity)
        {
            ResultDto<PRE_PORC_GASTOS_MENSUALES> result = new ResultDto<PRE_PORC_GASTOS_MENSUALES>(null);

            try
            {
                PRE_PORC_GASTOS_MENSUALES entityUpdate = await GetByCodigo(entity.CODIGO_POR_GASTOS_MES);
                if (entityUpdate != null)
                {


                    _context.PRE_PORC_GASTOS_MENSUALES.Update(entity);
                    await _context.SaveChangesAsync();
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

        public async Task<string> Delete(int codigoPorGastosMes)
        {

            try
            {
                PRE_PORC_GASTOS_MENSUALES entity = await GetByCodigo(codigoPorGastosMes);
                if (entity != null)
                {
                    _context.PRE_PORC_GASTOS_MENSUALES.Remove(entity);
                    await _context.SaveChangesAsync();
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
                var last = await _context.PRE_PORC_GASTOS_MENSUALES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_POR_GASTOS_MES)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_POR_GASTOS_MES + 1;
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
