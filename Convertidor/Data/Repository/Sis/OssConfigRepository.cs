using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class OssConfigRepository: Interfaces.Sis.IOssConfigRepository
    {
		

        private readonly DataContextSis _context;
        private readonly IConfiguration _configuration;
        public OssConfigRepository(DataContextSis context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<OSS_CONFIG>> GetALL()
        {
            try
            {
                var result = await _context.OSS_CONFIG.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<List<OSS_CONFIG>> GetListByClave(string clave)
        {
            try
            {
                var result = await _context.OSS_CONFIG.DefaultIfEmpty().Where(x=>x.CLAVE==clave).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<OSS_CONFIG> GetByClave(string clave)
        {
            try
            {
                var result = await _context.OSS_CONFIG.DefaultIfEmpty().Where(x => x.CLAVE == clave).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<OSS_CONFIG>> Add(OSS_CONFIG entity)
        {
            ResultDto<OSS_CONFIG> result = new ResultDto<OSS_CONFIG>(null);
            try
            {

                entity.ID = await GetNextKey();

                await _context.OSS_CONFIG.AddAsync(entity);
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

        public async Task<ResultDto<OSS_CONFIG>> Update(OSS_CONFIG entity)
        {
            ResultDto<OSS_CONFIG> result = new ResultDto<OSS_CONFIG>(null);

            try
            {
                OSS_CONFIG entityUpdate = await GetByClave(entity.CLAVE);
                if (entityUpdate != null)
                {


                    _context.OSS_CONFIG.Update(entity);
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

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.OSS_CONFIG.DefaultIfEmpty()
                    .OrderByDescending(x => x.ID)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.ID + 1;
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

