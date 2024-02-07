using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmPucReintegroRepository: IAdmPucReintegroRepository
    {
        private readonly DataContextAdm _context;
        public AdmPucReintegroRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_PUC_REINTEGRO> GetCodigoPucReintegro(int codigoPucReintegro)
        {
            try
            {
                var result = await _context.ADM_PUC_REINTEGRO.DefaultIfEmpty()
                    .Where(e => e.CODIGO_PUC_REINTEGRO == codigoPucReintegro).FirstOrDefaultAsync();

                return (ADM_PUC_REINTEGRO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_PUC_REINTEGRO>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_PUC_REINTEGRO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_PUC_REINTEGRO>>Add(ADM_PUC_REINTEGRO entity) 
        {

            ResultDto<ADM_PUC_REINTEGRO> result = new ResultDto<ADM_PUC_REINTEGRO>(null);
            try 
            {
                await _context.ADM_PUC_REINTEGRO.AddAsync(entity);
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

        public async Task<ResultDto<ADM_PUC_REINTEGRO>>Update(ADM_PUC_REINTEGRO entity) 
        {
            ResultDto<ADM_PUC_REINTEGRO> result = new ResultDto<ADM_PUC_REINTEGRO>(null);

            try
            {
                ADM_PUC_REINTEGRO entityUpdate = await GetCodigoPucReintegro(entity.CODIGO_PUC_REINTEGRO);
                if (entityUpdate != null)
                {
                    _context.ADM_PUC_REINTEGRO.Update(entity);
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
        public async Task<string>Delete(int codigoPucReintegro) 
        {
            try
            {
                ADM_PUC_REINTEGRO entity = await GetCodigoPucReintegro(codigoPucReintegro);
                if (entity != null)
                {
                    _context.ADM_PUC_REINTEGRO.Remove(entity);
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
                var last = await _context.ADM_PUC_REINTEGRO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PUC_REINTEGRO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PUC_REINTEGRO + 1;
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
