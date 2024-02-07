using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmReintegrosRepository: IAdmReintegrosRepository
    {
        private readonly DataContextAdm _context;
        public AdmReintegrosRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_REINTEGROS> GetCodigoReintegro(int codigoReintegro)
        {
            try
            {
                var result = await _context.ADM_REINTEGROS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_REINTEGRO == codigoReintegro).FirstOrDefaultAsync();

                return (ADM_REINTEGROS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_REINTEGROS>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_REINTEGROS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_REINTEGROS>>Add(ADM_REINTEGROS entity) 
        {

            ResultDto<ADM_REINTEGROS> result = new ResultDto<ADM_REINTEGROS>(null);
            try 
            {
                await _context.ADM_REINTEGROS.AddAsync(entity);
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

        public async Task<ResultDto<ADM_REINTEGROS>>Update(ADM_REINTEGROS entity) 
        {
            ResultDto<ADM_REINTEGROS> result = new ResultDto<ADM_REINTEGROS>(null);

            try
            {
                ADM_REINTEGROS entityUpdate = await GetCodigoReintegro(entity.CODIGO_REINTEGRO);
                if (entityUpdate != null)
                {
                    _context.ADM_REINTEGROS.Update(entity);
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
        public async Task<string>Delete(int codigoReintegro) 
        {
            try
            {
                ADM_REINTEGROS entity = await GetCodigoReintegro(codigoReintegro);
                if (entity != null)
                {
                    _context.ADM_REINTEGROS.Remove(entity);
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
                var last = await _context.ADM_REINTEGROS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_REINTEGRO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_REINTEGRO + 1;
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
