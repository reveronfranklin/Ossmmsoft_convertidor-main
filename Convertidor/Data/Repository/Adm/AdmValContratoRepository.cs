using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmValContratoRepository: IAdmValContratoRepository
    {
        private readonly DataContextAdm _context;
        public AdmValContratoRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_VAL_CONTRATO> GetCodigoValContrato(int codigoValContrato)
        {
            try
            {
                var result = await _context.ADM_VAL_CONTRATO.DefaultIfEmpty()
                    .Where(e => e.CODIGO_VAL_CONTRATO == codigoValContrato).FirstOrDefaultAsync();

                return (ADM_VAL_CONTRATO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_VAL_CONTRATO>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_VAL_CONTRATO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_VAL_CONTRATO>>Add(ADM_VAL_CONTRATO entity) 
        {

            ResultDto<ADM_VAL_CONTRATO> result = new ResultDto<ADM_VAL_CONTRATO>(null);
            try 
            {
                await _context.ADM_VAL_CONTRATO.AddAsync(entity);
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

        public async Task<ResultDto<ADM_VAL_CONTRATO>>Update(ADM_VAL_CONTRATO entity) 
        {
            ResultDto<ADM_VAL_CONTRATO> result = new ResultDto<ADM_VAL_CONTRATO>(null);

            try
            {
                ADM_VAL_CONTRATO entityUpdate = await GetCodigoValContrato(entity.CODIGO_VAL_CONTRATO);
                if (entityUpdate != null)
                {
                    _context.ADM_VAL_CONTRATO.Update(entity);
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
        public async Task<string>Delete(int codigoValContrato) 
        {
            try
            {
                ADM_VAL_CONTRATO entity = await GetCodigoValContrato(codigoValContrato);
                if (entity != null)
                {
                    _context.ADM_VAL_CONTRATO.Remove(entity);
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
                var last = await _context.ADM_VAL_CONTRATO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_VAL_CONTRATO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_VAL_CONTRATO + 1;
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
