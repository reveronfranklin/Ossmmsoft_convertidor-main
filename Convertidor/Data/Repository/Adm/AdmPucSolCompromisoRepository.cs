﻿using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmPucSolCompromisoRepository: IAdmPucSolCompromisoRepository
    {
        private readonly DataContextAdm _context;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public AdmPucSolCompromisoRepository(DataContextAdm context, ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ADM_PUC_SOL_COMPROMISO> GetbyCodigoPucSolicitud(int codigoPucSolicitud)
        {
            try
            {
                
                var result = await _context.ADM_PUC_SOL_COMPROMISO.DefaultIfEmpty().Where(x => x.CODIGO_PUC_SOLICITUD == codigoPucSolicitud).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<List<ADM_PUC_SOL_COMPROMISO>> GetAllbyCodigoPucSolicitud(int codigoPucSolicitud)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.ADM_PUC_SOL_COMPROMISO.DefaultIfEmpty().Where(x => x.CODIGO_EMPRESA == conectado.Empresa && x.CODIGO_PUC_SOLICITUD == codigoPucSolicitud).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
       


        public async Task<List<ADM_PUC_SOL_COMPROMISO>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_PUC_SOL_COMPROMISO.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_PUC_SOL_COMPROMISO>>Add(ADM_PUC_SOL_COMPROMISO entity) 
        {

            ResultDto<ADM_PUC_SOL_COMPROMISO> result = new ResultDto<ADM_PUC_SOL_COMPROMISO>(null);
            try 
            {
                await _context.ADM_PUC_SOL_COMPROMISO.AddAsync(entity);
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

        public async Task<ResultDto<ADM_PUC_SOL_COMPROMISO>>Update(ADM_PUC_SOL_COMPROMISO entity) 
        {
            ResultDto<ADM_PUC_SOL_COMPROMISO> result = new ResultDto<ADM_PUC_SOL_COMPROMISO>(null);

            try
            {
                ADM_PUC_SOL_COMPROMISO entityUpdate = await GetbyCodigoPucSolicitud(entity.CODIGO_PUC_SOLICITUD);
                if (entityUpdate != null)
                {
                    _context.ADM_PUC_SOL_COMPROMISO.Update(entity);
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
                ADM_PUC_SOL_COMPROMISO entity = await GetbyCodigoPucSolicitud(codigoPucSolicitud);
                if (entity != null)
                {
                    _context.ADM_PUC_SOL_COMPROMISO.Remove(entity);
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
                var last = await _context.ADM_PUC_SOL_COMPROMISO.DefaultIfEmpty()
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