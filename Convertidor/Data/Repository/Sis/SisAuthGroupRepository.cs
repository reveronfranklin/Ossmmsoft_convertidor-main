using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Sis
{
	public class SisAuthGroupRepository: Interfaces.Sis.ISisAuthGroupRepository
    {
		

        private readonly DataContextSis _context;
        public SisAuthGroupRepository(DataContextSis context)
        {
            _context = context;
      
        }

        public async Task<List<AUTH_GROUP>> GetALL()
        {
            try
            {
                var result = await _context.AUTH_GROUP.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

     

        public async Task<AUTH_GROUP> GetByID(int id)
        {
            try
            {
                var result = await _context.AUTH_GROUP.DefaultIfEmpty().Where(x => x.ID == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<AUTH_GROUP>> Add(AUTH_GROUP entity)
        {
            ResultDto<AUTH_GROUP> result = new ResultDto<AUTH_GROUP>(null);
            try
            {

                entity.ID = await GetNextKey();

                await _context.AUTH_GROUP.AddAsync(entity);
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

        public async Task<ResultDto<AUTH_GROUP>> Update(AUTH_GROUP entity)
        {
            ResultDto<AUTH_GROUP> result = new ResultDto<AUTH_GROUP>(null);

            try
            {
                AUTH_GROUP entityUpdate = await GetByID(entity.ID);
                if (entityUpdate != null)
                {


                    _context.AUTH_GROUP.Update(entity);
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
                var last = await _context.AUTH_GROUP.DefaultIfEmpty()
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

