using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmPucContratoRepository: IAdmPucContratoRepository
    {
        private readonly DataContextAdm _context;
        public AdmPucContratoRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_PUC_CONTRATO> GetCodigoPucContrato(int codigoPucContrato)
        {
            try
            {
                var result = await _context.ADM_PUC_CONTRATO.DefaultIfEmpty()
                    .Where(e => e.CODIGO_PUC_CONTRATO == codigoPucContrato).FirstOrDefaultAsync();

                return (ADM_PUC_CONTRATO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_PUC_CONTRATO>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_PUC_CONTRATO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_PUC_CONTRATO>>Add(ADM_PUC_CONTRATO entity) 
        {

            ResultDto<ADM_PUC_CONTRATO> result = new ResultDto<ADM_PUC_CONTRATO>(null);
            try 
            {
                await _context.ADM_PUC_CONTRATO.AddAsync(entity);
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

        public async Task<ResultDto<ADM_PUC_CONTRATO>>Update(ADM_PUC_CONTRATO entity) 
        {
            ResultDto<ADM_PUC_CONTRATO> result = new ResultDto<ADM_PUC_CONTRATO>(null);

            try
            {
                ADM_PUC_CONTRATO entityUpdate = await GetCodigoPucContrato(entity.CODIGO_PUC_CONTRATO);
                if (entityUpdate != null)
                {
                    _context.ADM_PUC_CONTRATO.Update(entity);
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
        public async Task<string>Delete(int codigoPucContrato) 
        {
            try
            {
                ADM_PUC_CONTRATO entity = await GetCodigoPucContrato(codigoPucContrato);
                if (entity != null)
                {
                    _context.ADM_PUC_CONTRATO.Remove(entity);
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
                var last = await _context.ADM_PUC_CONTRATO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PUC_CONTRATO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PUC_CONTRATO + 1;
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
