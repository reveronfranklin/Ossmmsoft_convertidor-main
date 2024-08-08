using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Sis
{
	public class SisAuthPermissionRepository: Interfaces.Sis.ISisAuthPermissionRepository
    {
		

        private readonly DataContextSis _context;
        public SisAuthPermissionRepository(DataContextSis context)
        {
            _context = context;
      
        }

        public async Task<List<AUTH_PERMISSION>> GetALL()
        {
            try
            {
                var result = await _context.AUTH_PERMISSION.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

     

        public async Task<AUTH_PERMISSION> GetByID(int id)
        {
            try
            {
                var result = await _context.AUTH_PERMISSION.DefaultIfEmpty().Where(x => x.ID == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<AUTH_PERMISSION>> Add(AUTH_PERMISSION entity)
        {
            ResultDto<AUTH_PERMISSION> result = new ResultDto<AUTH_PERMISSION>(null);
            try
            {

                entity.ID = await GetNextKey();

                await _context.AUTH_PERMISSION.AddAsync(entity);
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

        public async Task<ResultDto<AUTH_PERMISSION>> Update(AUTH_PERMISSION entity)
        {
            ResultDto<AUTH_PERMISSION> result = new ResultDto<AUTH_PERMISSION>(null);

            try
            {
                AUTH_PERMISSION entityUpdate = await GetByID(entity.ID);
                if (entityUpdate != null)
                {


                    _context.AUTH_PERMISSION.Update(entity);
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
                var last = await _context.AUTH_PERMISSION.DefaultIfEmpty()
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

