﻿using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmPucOrdenPagoRepository : IAdmPucOrdenPagoRepository
    {
        private readonly DataContextAdm _context;
        public AdmPucOrdenPagoRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_PUC_ORDEN_PAGO> GetCodigoPucOrdenPago(int CodigoPucOrdenPago)
        {
            try
            {
                var result = await _context.ADM_PUC_ORDEN_PAGO
                    .Where(e => e.CODIGO_PUC_ORDEN_PAGO == CodigoPucOrdenPago).FirstOrDefaultAsync();

                return (ADM_PUC_ORDEN_PAGO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_PUC_ORDEN_PAGO>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_PUC_ORDEN_PAGO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_PUC_ORDEN_PAGO>>Add(ADM_PUC_ORDEN_PAGO entity) 
        {

            ResultDto<ADM_PUC_ORDEN_PAGO> result = new ResultDto<ADM_PUC_ORDEN_PAGO>(null);
            try 
            {
                await _context.ADM_PUC_ORDEN_PAGO.AddAsync(entity);
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

        public async Task<ResultDto<ADM_PUC_ORDEN_PAGO>>Update(ADM_PUC_ORDEN_PAGO entity) 
        {
            ResultDto<ADM_PUC_ORDEN_PAGO> result = new ResultDto<ADM_PUC_ORDEN_PAGO>(null);

            try
            {
                ADM_PUC_ORDEN_PAGO entityUpdate = await GetCodigoPucOrdenPago(entity.CODIGO_PUC_ORDEN_PAGO);
                if (entityUpdate != null)
                {
                    _context.ADM_PUC_ORDEN_PAGO.Update(entity);
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
        public async Task<string>Delete(int CodigoPucOrdenPago) 
        {
            try
            {
                ADM_PUC_ORDEN_PAGO entity = await GetCodigoPucOrdenPago(CodigoPucOrdenPago);
                if (entity != null)
                {
                    _context.ADM_PUC_ORDEN_PAGO.Remove(entity);
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
                var last = await _context.ADM_PUC_ORDEN_PAGO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PUC_ORDEN_PAGO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PUC_ORDEN_PAGO + 1;
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
