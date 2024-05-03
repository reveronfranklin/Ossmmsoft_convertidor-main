using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreProgramasSocialesRepository : IPreProgramasSocialesRepository
    {
        private readonly DataContextPre _context;

        public PreProgramasSocialesRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_PROGRAMAS_SOCIALES> GetByCodigo(int codigoPrgSocial)
        {
            try
            {
                var result = await _context.PRE_PROGRAMAS_SOCIALES.DefaultIfEmpty().Where(e => e.CODIGO_PRG_SOCIAL == codigoPrgSocial).FirstOrDefaultAsync();

                return (PRE_PROGRAMAS_SOCIALES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_PROGRAMAS_SOCIALES>> GetAll()
        {
            try
            {
                var result = await _context.PRE_PROGRAMAS_SOCIALES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<PRE_PROGRAMAS_SOCIALES>> Add(PRE_PROGRAMAS_SOCIALES entity)
        {
            ResultDto<PRE_PROGRAMAS_SOCIALES> result = new ResultDto<PRE_PROGRAMAS_SOCIALES>(null);
            try
            {



                await _context.PRE_PROGRAMAS_SOCIALES.AddAsync(entity);
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

        public async Task<ResultDto<PRE_PROGRAMAS_SOCIALES>> Update(PRE_PROGRAMAS_SOCIALES entity)
        {
            ResultDto<PRE_PROGRAMAS_SOCIALES> result = new ResultDto<PRE_PROGRAMAS_SOCIALES>(null);

            try
            {
                PRE_PROGRAMAS_SOCIALES entityUpdate = await GetByCodigo(entity.CODIGO_PRG_SOCIAL);
                if (entityUpdate != null)
                {


                    _context.PRE_PROGRAMAS_SOCIALES.Update(entity);
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

        public async Task<string> Delete(int codigoPrgSocial)
        {

            try
            {
                PRE_PROGRAMAS_SOCIALES entity = await GetByCodigo(codigoPrgSocial);
                if (entity != null)
                {
                    _context.PRE_PROGRAMAS_SOCIALES.Remove(entity);
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
                var last = await _context.PRE_PROGRAMAS_SOCIALES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PRG_SOCIAL)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PRG_SOCIAL + 1;
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
