using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class OssVariableRepository: IOssVariableRepository
    {
		
        private readonly DataContextSis _context;

        public OssVariableRepository(DataContextSis context)
        {
            _context = context;
        }
      
        public async Task<OssVariable> GetByCodigo(string code)
        {
            try
            {
                var result = await _context.OssVariables.DefaultIfEmpty().Where(e => e.Code == code).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<OssVariable> GetById(int id)
        {
            try
            {
                var result = await _context.OssVariables.DefaultIfEmpty().Where(e => e.Id == id).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
 
        public async Task<List<OssVariable>> GetByAll()
        {
            try
            {
                var result = await _context.OssVariables.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<OssVariable>> Add(OssVariable entity)
        {
            ResultDto<OssVariable> result = new ResultDto<OssVariable>(null);
            try
            {



                await _context.OssVariables.AddAsync(entity);
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

        public async Task<ResultDto<OssVariable>> Update(OssVariable entity)
        {
            ResultDto<OssVariable> result = new ResultDto<OssVariable>(null);

            try
            {
                OssVariable entityUpdate = await GetById(entity.Id);
                if (entityUpdate != null)
                {


                    _context.OssVariables.Update(entity);
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

        public async Task<string> Delete(int id)
        {

            try
            {
                OssVariable entity = await GetById(id);
                if (entity != null)
                {
                    _context.OssVariables.Remove(entity);
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
                var last = await _context.OssVariables.DefaultIfEmpty()
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.Id + 1;
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

