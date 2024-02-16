using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmCompromisoOpRepository : IAdmCompromisoOpRepository
    {
        private readonly DataContextAdm _context;
        public AdmCompromisoOpRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_COMPROMISO_OP> GetCodigoCompromisoOp(int CodigoCompromisoOp)
        {
            try
            {
                var result = await _context.ADM_COMPROMISO_OP
                    .Where(e => e.CODIGO_COMPROMISO_OP == CodigoCompromisoOp).FirstOrDefaultAsync();

                return (ADM_COMPROMISO_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_COMPROMISO_OP>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_COMPROMISO_OP.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_COMPROMISO_OP>>Add(ADM_COMPROMISO_OP entity) 
        {

            ResultDto<ADM_COMPROMISO_OP> result = new ResultDto<ADM_COMPROMISO_OP>(null);
            try 
            {
                await _context.ADM_COMPROMISO_OP.AddAsync(entity);
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

        public async Task<ResultDto<ADM_COMPROMISO_OP>>Update(ADM_COMPROMISO_OP entity) 
        {
            ResultDto<ADM_COMPROMISO_OP> result = new ResultDto<ADM_COMPROMISO_OP>(null);

            try
            {
                ADM_COMPROMISO_OP entityUpdate = await GetCodigoCompromisoOp(entity.CODIGO_COMPROMISO_OP);
                if (entityUpdate != null)
                {
                    _context.ADM_COMPROMISO_OP.Update(entity);
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
        public async Task<string>Delete(int CodigoCompromisoOp) 
        {
            try
            {
                ADM_COMPROMISO_OP entity = await GetCodigoCompromisoOp(CodigoCompromisoOp);
                if (entity != null)
                {
                    _context.ADM_COMPROMISO_OP.Remove(entity);
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
                var last = await _context.ADM_COMPROMISO_OP.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_COMPROMISO_OP)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_COMPROMISO_OP + 1;
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
