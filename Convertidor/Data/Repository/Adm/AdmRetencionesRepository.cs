using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmRetencionesRepository : IAdmRetencionesRepository
    {
        private readonly DataContextAdm _context;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public AdmRetencionesRepository(DataContextAdm context,ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ADM_RETENCIONES> GetCodigoRetencion(int codigoRetencion)
        {
            try
            {
                var result = await _context.ADM_RETENCIONES
                    .Where(e => e.CODIGO_RETENCION == codigoRetencion).FirstOrDefaultAsync();

                return (ADM_RETENCIONES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<ADM_RETENCIONES> GetByExtra1(string extra1)
        {
            try
            {
                var result = await _context.ADM_RETENCIONES
                    .Where(e => e.EXTRA1 == extra1).FirstOrDefaultAsync();

                return (ADM_RETENCIONES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_RETENCIONES>> GetAll() 
        {
            try
            {
                
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.ADM_RETENCIONES.Where(x=>x.CODIGO_EMPRESA==conectado.Empresa).DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
      

        public async Task<ResultDto<ADM_RETENCIONES>>Add(ADM_RETENCIONES entity) 
        {

            ResultDto<ADM_RETENCIONES> result = new ResultDto<ADM_RETENCIONES>(null);
            try 
            {
                await _context.ADM_RETENCIONES.AddAsync(entity);
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

        public async Task<ResultDto<ADM_RETENCIONES>>Update(ADM_RETENCIONES entity) 
        {
            ResultDto<ADM_RETENCIONES> result = new ResultDto<ADM_RETENCIONES>(null);

            try
            {
                ADM_RETENCIONES entityUpdate = await GetCodigoRetencion(entity.CODIGO_RETENCION);
                if (entityUpdate != null)
                {
                    _context.ADM_RETENCIONES.Update(entity);
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
        public async Task<string>Delete(int codigoRetencion) 
        {
            try
            {
                ADM_RETENCIONES entity = await GetCodigoRetencion(codigoRetencion);
                if (entity != null)
                {
                    _context.ADM_RETENCIONES.Remove(entity);
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
                var last = await _context.ADM_RETENCIONES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_RETENCION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_RETENCION + 1;
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
