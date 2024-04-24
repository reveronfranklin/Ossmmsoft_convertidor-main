using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmPeriodicoOpRepository : IAdmPeriodicoOpRepository
    {
        private readonly DataContextAdm _context;
        public AdmPeriodicoOpRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_PERIODICO_OP> GetCodigoPeriodicoOp(int codigoPeriodicoOp)
        {
            try
            {
                var result = await _context.ADM_PERIODICO_OP
                    .Where(e => e.CODIGO_PERIODICO_OP == codigoPeriodicoOp).FirstOrDefaultAsync();

                return (ADM_PERIODICO_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ADM_PERIODICO_OP> GetFechaPago(DateTime fechaPago)
        {
            try
            {
                var result = await _context.ADM_PERIODICO_OP
                    .Where(e => e.FECHA_PAGO == fechaPago).FirstOrDefaultAsync();

                return (ADM_PERIODICO_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }


        public async Task<List<ADM_PERIODICO_OP>> GetAll()
        {
            try
            {
                var result = await _context.ADM_PERIODICO_OP.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_PERIODICO_OP>> Add(ADM_PERIODICO_OP entity)
        {

            ResultDto<ADM_PERIODICO_OP> result = new ResultDto<ADM_PERIODICO_OP>(null);
            try
            {
                await _context.ADM_PERIODICO_OP.AddAsync(entity);
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

        public async Task<ResultDto<ADM_PERIODICO_OP>> Update(ADM_PERIODICO_OP entity)
        {
            ResultDto<ADM_PERIODICO_OP> result = new ResultDto<ADM_PERIODICO_OP>(null);

            try
            {
                ADM_PERIODICO_OP entityUpdate = await GetCodigoPeriodicoOp(entity.CODIGO_PERIODICO_OP);
                if (entityUpdate != null)
                {
                    _context.ADM_PERIODICO_OP.Update(entity);
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
        public async Task<string> Delete(int codigoPeriodicoOp)
        {
            try
            {
                ADM_PERIODICO_OP entity = await GetCodigoPeriodicoOp (codigoPeriodicoOp);
                if (entity != null)
                {
                    _context.ADM_PERIODICO_OP.Remove(entity);
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
                var last = await _context.ADM_PERIODICO_OP.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PERIODICO_OP)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PERIODICO_OP + 1;
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
