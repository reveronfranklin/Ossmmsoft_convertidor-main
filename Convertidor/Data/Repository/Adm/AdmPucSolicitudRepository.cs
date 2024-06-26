﻿using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmPucSolicitudRepository: IAdmPucSolicitudRepository
    {
        private readonly DataContextAdm _context;
        public AdmPucSolicitudRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_PUC_SOLICITUD> GetCodigoPucSolicitud(int codigoPucSolicitud)
        {
            try
            {
                var result = await _context.ADM_PUC_SOLICITUD.DefaultIfEmpty()
                    .Where(e => e.CODIGO_PUC_SOLICITUD == codigoPucSolicitud).FirstOrDefaultAsync();

                return (ADM_PUC_SOLICITUD)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<bool> ExistePresupuesto(int codigoPresupuesto)
        {
            var result = false;
            try
            {
                var solicitud = await _context.ADM_PUC_SOLICITUD.DefaultIfEmpty()
                    .Where(e => e.CODIGO_PRESUPUESTO == codigoPresupuesto).FirstOrDefaultAsync();
                if (solicitud != null)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return false;
            }

        }

        public async Task<List<ADM_PUC_SOLICITUD>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_PUC_SOLICITUD.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_PUC_SOLICITUD>>Add(ADM_PUC_SOLICITUD entity) 
        {

            ResultDto<ADM_PUC_SOLICITUD> result = new ResultDto<ADM_PUC_SOLICITUD>(null);
            try 
            {
                await _context.ADM_PUC_SOLICITUD.AddAsync(entity);
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

        public async Task<ResultDto<ADM_PUC_SOLICITUD>>Update(ADM_PUC_SOLICITUD entity) 
        {
            ResultDto<ADM_PUC_SOLICITUD> result = new ResultDto<ADM_PUC_SOLICITUD>(null);

            try
            {
                ADM_PUC_SOLICITUD entityUpdate = await GetCodigoPucSolicitud(entity.CODIGO_PUC_SOLICITUD);
                if (entityUpdate != null)
                {
                    _context.ADM_PUC_SOLICITUD.Update(entity);
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
        public async Task<string>Delete(int codigoPucSolicitud) 
        {
            try
            {
                ADM_PUC_SOLICITUD entity = await GetCodigoPucSolicitud(codigoPucSolicitud);
                if (entity != null)
                {
                    _context.ADM_PUC_SOLICITUD.Remove(entity);
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
                var last = await _context.ADM_PUC_SOLICITUD.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PUC_SOLICITUD)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PUC_SOLICITUD + 1;
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
