using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmHOrdenPagoRepository: IAdmHOrdenPagoRepository
    {
        private readonly DataContextAdm _context;
        public AdmHOrdenPagoRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_H_ORDEN_PAGO> GetCodigoHOrdenPago(int codigoHOrdenPago)
        {
            try
            {
                var result = await _context.ADM_H_ORDEN_PAGO
                    .Where(e => e.CODIGO_H_ORDEN_PAGO == codigoHOrdenPago).FirstOrDefaultAsync();

                return (ADM_H_ORDEN_PAGO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_H_ORDEN_PAGO>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_H_ORDEN_PAGO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_H_ORDEN_PAGO>>Add(ADM_H_ORDEN_PAGO entity) 
        {

            ResultDto<ADM_H_ORDEN_PAGO> result = new ResultDto<ADM_H_ORDEN_PAGO>(null);
            try 
            {
                await _context.ADM_H_ORDEN_PAGO.AddAsync(entity);
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

        public async Task<ResultDto<ADM_H_ORDEN_PAGO>>Update(ADM_H_ORDEN_PAGO entity) 
        {
            ResultDto<ADM_H_ORDEN_PAGO> result = new ResultDto<ADM_H_ORDEN_PAGO>(null);

            try
            {
                ADM_H_ORDEN_PAGO entityUpdate = await GetCodigoHOrdenPago(entity.CODIGO_H_ORDEN_PAGO);
                if (entityUpdate != null)
                {
                    _context.ADM_H_ORDEN_PAGO.Update(entity);
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
        public async Task<string>Delete(int codigoHOrdenPago) 
        {
            try
            {
                ADM_H_ORDEN_PAGO entity = await GetCodigoHOrdenPago(codigoHOrdenPago);
                if (entity != null)
                {
                    _context.ADM_H_ORDEN_PAGO.Remove(entity);
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
                var last = await _context.ADM_H_ORDEN_PAGO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_H_ORDEN_PAGO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_H_ORDEN_PAGO + 1;
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
