using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmSolicitudesRepository:IAdmSolicitudesRepository
    {
        private readonly DataContextAdm _context;
        public AdmSolicitudesRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_SOLICITUDES> GetByCodigoSolicitud(int codigoSolicitud)
        {
            try
            {
                var result = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_SOLICITUD == codigoSolicitud).FirstOrDefaultAsync();

                return (ADM_SOLICITUDES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_SOLICITUDES>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_SOLICITUDES.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_SOLICITUDES>>Add(ADM_SOLICITUDES entity) 
        {

            ResultDto<ADM_SOLICITUDES> result = new ResultDto<ADM_SOLICITUDES>(null);
            try 
            {
                await _context.ADM_SOLICITUDES.AddAsync(entity);
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

        public async Task<ResultDto<ADM_SOLICITUDES>>Update(ADM_SOLICITUDES entity) 
        {
            ResultDto<ADM_SOLICITUDES> result = new ResultDto<ADM_SOLICITUDES>(null);

            try
            {
                ADM_SOLICITUDES entityUpdate = await GetByCodigoSolicitud(entity.CODIGO_SOLICITUD);
                if (entityUpdate != null)
                {
                    _context.ADM_SOLICITUDES.Update(entity);
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
        public async Task<string>Delete(int codigoSolicitud) 
        {
            try
            {
                ADM_SOLICITUDES entity = await GetByCodigoSolicitud(codigoSolicitud);
                if (entity != null)
                {
                    _context.ADM_SOLICITUDES.Remove(entity);
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
                var last = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_SOLICITUD)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_SOLICITUD + 1;
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
