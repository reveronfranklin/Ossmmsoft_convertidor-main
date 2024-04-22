using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmContratosRepository: IAdmContratosRepository
    {
        private readonly DataContextAdm _context;
        public AdmContratosRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_CONTRATOS> GetByCodigoContrato(int codigoContrato)
        {
            try
            {
                var result = await _context.ADM_CONTRATOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_CONTRATO == codigoContrato).FirstOrDefaultAsync();

                return (ADM_CONTRATOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_CONTRATOS>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_CONTRATOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_CONTRATOS>>Add(ADM_CONTRATOS entity) 
        {

            ResultDto<ADM_CONTRATOS> result = new ResultDto<ADM_CONTRATOS>(null);
            try 
            {
                await _context.ADM_CONTRATOS.AddAsync(entity);
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

        public async Task<ResultDto<ADM_CONTRATOS>>Update(ADM_CONTRATOS entity) 
        {
            ResultDto<ADM_CONTRATOS> result = new ResultDto<ADM_CONTRATOS>(null);

            try
            {
                ADM_CONTRATOS entityUpdate = await GetByCodigoContrato(entity.CODIGO_CONTRATO);
                if (entityUpdate != null)
                {
                    _context.ADM_CONTRATOS.Update(entity);
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
        public async Task<string>Delete(int codigoContrato) 
        {
            try
            {
                ADM_CONTRATOS entity = await GetByCodigoContrato(codigoContrato);
                if (entity != null)
                {
                    _context.ADM_CONTRATOS.Remove(entity);
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
                var last = await _context.ADM_CONTRATOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CONTRATO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CONTRATO + 1;
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
