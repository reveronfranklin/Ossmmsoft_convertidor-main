using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Sis
{
	public class OssAuthUserPermissionsRepository: IOssAuthUserPermissionsRepository
    {
		

        private readonly DataContextSis _context;
        public OssAuthUserPermissionsRepository(DataContextSis context)
        {
            _context = context;
      
        }

        public async Task<List<AUTH_USER_USER_PERMISSIONS>> GetALL()
        {
            try
            {
                var result = await _context.AUTH_USER_USER_PERMISSIONS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

     

        public async Task<AUTH_USER_USER_PERMISSIONS> GetByID(int id)
        {
            try
            {
                var result = await _context.AUTH_USER_USER_PERMISSIONS.DefaultIfEmpty().Where(x => x.ID == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<AUTH_USER_USER_PERMISSIONS>> Add(AUTH_USER_USER_PERMISSIONS entity)
        {
            ResultDto<AUTH_USER_USER_PERMISSIONS> result = new ResultDto<AUTH_USER_USER_PERMISSIONS>(null);
            try
            {

                entity.ID = await GetNextKey();

                await _context.AUTH_USER_USER_PERMISSIONS.AddAsync(entity);
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

        public async Task<ResultDto<AUTH_USER_USER_PERMISSIONS>> Update(AUTH_USER_USER_PERMISSIONS entity)
        {
            ResultDto<AUTH_USER_USER_PERMISSIONS> result = new ResultDto<AUTH_USER_USER_PERMISSIONS>(null);

            try
            {
                AUTH_USER_USER_PERMISSIONS entityUpdate = await GetByID(entity.ID);
                if (entityUpdate != null)
                {


                    _context.AUTH_USER_USER_PERMISSIONS.Update(entity);
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
                var last = await _context.AUTH_USER_USER_PERMISSIONS.DefaultIfEmpty()
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

