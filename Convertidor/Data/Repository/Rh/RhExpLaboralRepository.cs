using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhExpLaboralRepository : IRhExpLaboralRepository
    {
		
        private readonly DataContext _context;

        public RhExpLaboralRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<RH_EXP_LABORAL> GetByCodigo(int codigoExpLaboral)
        {
            try
            {
                var result = await _context.RH_EXP_LABORAL.DefaultIfEmpty()
                    .Where(e => e.CODIGO_EXP_LABORAL == codigoExpLaboral)
                    .OrderBy(x=>x.FECHA_INS).FirstOrDefaultAsync();

                return (RH_EXP_LABORAL)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_EXP_LABORAL>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_EXP_LABORAL.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona).ToListAsync();

                return (List<RH_EXP_LABORAL>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<RH_EXP_LABORAL>> Add(RH_EXP_LABORAL entity)
        {
            ResultDto<RH_EXP_LABORAL> result = new ResultDto<RH_EXP_LABORAL>(null);
            try
            {

                await _context.RH_EXP_LABORAL.AddAsync(entity);
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

        public async Task<ResultDto<RH_EXP_LABORAL>> Update(RH_EXP_LABORAL entity)
        {
            ResultDto<RH_EXP_LABORAL> result = new ResultDto<RH_EXP_LABORAL>(null);

            try
            {
                RH_EXP_LABORAL entityUpdate = await GetByCodigo(entity.CODIGO_EXP_LABORAL);
                if (entityUpdate != null)
                {


                    _context.RH_EXP_LABORAL.Update(entity);
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

        public async Task<string> Delete(int codigoExpLaboral)
        {

            try
            {
                RH_EXP_LABORAL entity = await GetByCodigo(codigoExpLaboral);
                if (entity != null)
                {
                    _context.RH_EXP_LABORAL.Remove(entity);
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
                var last = await _context.RH_EXP_LABORAL.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_EXP_LABORAL)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_EXP_LABORAL+ 1;
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

