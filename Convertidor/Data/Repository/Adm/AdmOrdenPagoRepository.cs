﻿using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmOrdenPagoRepository: IAdmOrdenPagoRepository
    {
        private readonly DataContextAdm _context;
        public AdmOrdenPagoRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_ORDEN_PAGO> GetCodigoOrdenPago(int codigoOrdenPago)
        {
            try
            {
                var result = await _context.ADM_ORDEN_PAGO
                    .Where(e => e.CODIGO_ORDEN_PAGO == codigoOrdenPago).FirstOrDefaultAsync();

                return (ADM_ORDEN_PAGO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_ORDEN_PAGO>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_ORDEN_PAGO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_ORDEN_PAGO>>Add(ADM_ORDEN_PAGO entity) 
        {

            ResultDto<ADM_ORDEN_PAGO> result = new ResultDto<ADM_ORDEN_PAGO>(null);
            try 
            {
                await _context.ADM_ORDEN_PAGO.AddAsync(entity);
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

        public async Task<ResultDto<ADM_ORDEN_PAGO>>Update(ADM_ORDEN_PAGO entity) 
        {
            ResultDto<ADM_ORDEN_PAGO> result = new ResultDto<ADM_ORDEN_PAGO>(null);

            try
            {
                ADM_ORDEN_PAGO entityUpdate = await GetCodigoOrdenPago(entity.CODIGO_ORDEN_PAGO);
                if (entityUpdate != null)
                {
                    _context.ADM_ORDEN_PAGO.Update(entity);
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
        public async Task<string>Delete(int codigoOrdenPago) 
        {
            try
            {
                ADM_ORDEN_PAGO entity = await GetCodigoOrdenPago(codigoOrdenPago);
                if (entity != null)
                {
                    _context.ADM_ORDEN_PAGO.Remove(entity);
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
                var last = await _context.ADM_ORDEN_PAGO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ORDEN_PAGO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ORDEN_PAGO + 1;
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
