using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Sis
{
	public class SisAuthContentTypeRepository: Interfaces.Sis.ISisAuthContentTypeRepository
    {
		

        private readonly DataContextSis _context;
        public SisAuthContentTypeRepository(DataContextSis context)
        {
            _context = context;
      
        }

        public async Task<List<AUTH_CONTENT_TYPE>> GetALL()
        {
            try
            {
                var result = await _context.AUTH_CONTENT_TYPE.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

     

        public async Task<AUTH_CONTENT_TYPE> GetByID(int id)
        {
            try
            {
                var result = await _context.AUTH_CONTENT_TYPE.DefaultIfEmpty().Where(x => x.ID == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<AUTH_CONTENT_TYPE>> Add(AUTH_CONTENT_TYPE entity)
        {
            ResultDto<AUTH_CONTENT_TYPE> result = new ResultDto<AUTH_CONTENT_TYPE>(null);
            try
            {

                entity.ID = await GetNextKey();

                await _context.AUTH_CONTENT_TYPE.AddAsync(entity);
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

        public async Task<ResultDto<AUTH_CONTENT_TYPE>> Update(AUTH_CONTENT_TYPE entity)
        {
            ResultDto<AUTH_CONTENT_TYPE> result = new ResultDto<AUTH_CONTENT_TYPE>(null);

            try
            {
                AUTH_CONTENT_TYPE entityUpdate = await GetByID(entity.ID);
                if (entityUpdate != null)
                {


                    _context.AUTH_CONTENT_TYPE.Update(entity);
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
                var last = await _context.AUTH_CONTENT_TYPE.DefaultIfEmpty()
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

