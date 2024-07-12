using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmSolCompromisoRepository : IAdmSolCompromisoRepository
    {
        private readonly DataContextAdm _context;

        public AdmSolCompromisoRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<List<ADM_SOL_COMPROMISO>> GetAll()
        {
            try
            {
                var result = await _context.ADM_SOL_COMPROMISO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ADM_SOL_COMPROMISO> GetByCodigo(int codigoSolCompromiso)
        {
            try
            {
                var result = await _context.ADM_SOL_COMPROMISO.DefaultIfEmpty()
                    .Where(e => e.CODIGO_SOL_COMPROMISO == codigoSolCompromiso).FirstOrDefaultAsync();

                return (ADM_SOL_COMPROMISO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<ADM_SOL_COMPROMISO>> Add(ADM_SOL_COMPROMISO entity)
        {

            ResultDto<ADM_SOL_COMPROMISO> result = new ResultDto<ADM_SOL_COMPROMISO>(null);
            try
            {
                await _context.ADM_SOL_COMPROMISO.AddAsync(entity);
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

        public async Task<ResultDto<ADM_SOL_COMPROMISO>> Update(ADM_SOL_COMPROMISO entity)
        {
            ResultDto<ADM_SOL_COMPROMISO> result = new ResultDto<ADM_SOL_COMPROMISO>(null);

            try
            {
                ADM_SOL_COMPROMISO entityUpdate = await GetByCodigo(entity.CODIGO_SOL_COMPROMISO);
                if (entityUpdate != null)
                {
                    _context.ADM_SOL_COMPROMISO.Update(entity);
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

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.ADM_SOL_COMPROMISO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_SOL_COMPROMISO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_SOL_COMPROMISO + 1;
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
