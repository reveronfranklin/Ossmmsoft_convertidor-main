using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmChequesRepository: IAdmChequesRepository
    {
        private readonly DataContextAdm _context;
        public AdmChequesRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_CHEQUES> GetByCodigoCheque(int codigoCheque)
        {
            try
            {
                var result = await _context.ADM_CHEQUES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_CHEQUE == codigoCheque).FirstOrDefaultAsync();

                return (ADM_CHEQUES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_CHEQUES>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_CHEQUES.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_CHEQUES>>Add(ADM_CHEQUES entity) 
        {

            ResultDto<ADM_CHEQUES> result = new ResultDto<ADM_CHEQUES>(null);
            try 
            {
                await _context.ADM_CHEQUES.AddAsync(entity);
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

        public async Task<ResultDto<ADM_CHEQUES>>Update(ADM_CHEQUES entity) 
        {
            ResultDto<ADM_CHEQUES> result = new ResultDto<ADM_CHEQUES>(null);

            try
            {
                ADM_CHEQUES entityUpdate = await GetByCodigoCheque(entity.CODIGO_CHEQUE);
                if (entityUpdate != null)
                {
                    _context.ADM_CHEQUES.Update(entity);
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
        public async Task<string>Delete(int codigoCheque) 
        {
            try
            {
                ADM_CHEQUES entity = await GetByCodigoCheque(codigoCheque);
                if (entity != null)
                {
                    _context.ADM_CHEQUES.Remove(entity);
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
                var last = await _context.ADM_CHEQUES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CHEQUE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CHEQUE + 1;
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
