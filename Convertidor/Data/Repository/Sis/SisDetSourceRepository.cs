using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class SisDetSourceRepository: Interfaces.Sis.ISisDetSourceRepository
    {
		

        private readonly DataContextSis _context;
        private readonly IConfiguration _configuration;
        public SisDetSourceRepository(DataContextSis context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<SIS_DET_SOURCE>> GetALL()
        {
            try
            {
                var result = await _context.SIS_DET_SOURCE.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

     

        public async Task<SIS_DET_SOURCE> GetById(int codigo)
        {
            try
            {
                var result = await _context.SIS_DET_SOURCE.DefaultIfEmpty().Where(x => x.CODIGO_DET_SOURCE == codigo).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        public async Task<List<SIS_DET_SOURCE>> GetByCodigoSource(int codigoSource)
        {
            try
            {
                var result = await _context.SIS_DET_SOURCE.DefaultIfEmpty().Where(x => x.CODIGO_SOURCE == codigoSource).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<SIS_DET_SOURCE>> Add(SIS_DET_SOURCE entity)
        {
            ResultDto<SIS_DET_SOURCE> result = new ResultDto<SIS_DET_SOURCE>(null);
            try
            {

                entity.CODIGO_DET_SOURCE = await GetNextKey();

                await _context.SIS_DET_SOURCE.AddAsync(entity);
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

        public async Task<ResultDto<SIS_DET_SOURCE>> Update(SIS_DET_SOURCE entity)
        {
            ResultDto<SIS_DET_SOURCE> result = new ResultDto<SIS_DET_SOURCE>(null);

            try
            {
                SIS_DET_SOURCE entityUpdate = await GetById(entity.CODIGO_DET_SOURCE);
                if (entityUpdate != null)
                {


                    _context.SIS_DET_SOURCE.Update(entity);
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
                var last = await _context.SIS_DET_SOURCE.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DET_SOURCE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DET_SOURCE + 1;
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

