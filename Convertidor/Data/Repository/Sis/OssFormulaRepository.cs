using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class OssFormulaRepository: IOssFormulaRepository
    {
		
        private readonly DataContextSis _context;

        public OssFormulaRepository(DataContextSis context)
        {
            _context = context;
        }
      
      
        public async Task<OssFormula> GetById(int id)
        {
            try
            {
                var result = await _context.OssFormulas.DefaultIfEmpty().Where(e => e.Id == id).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
 
        public async Task<List<OssFormula>> GetByAll()
        {
            try
            {
                var result = await _context.OssFormulas.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<OssFormula>> Add(OssFormula entity)
        {
            ResultDto<OssFormula> result = new ResultDto<OssFormula>(null);
            try
            {



                await _context.OssFormulas.AddAsync(entity);
                _context.SaveChanges();


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

        public async Task<ResultDto<OssFormula>> Update(OssFormula entity)
        {
            ResultDto<OssFormula> result = new ResultDto<OssFormula>(null);

            try
            {
                OssFormula entityUpdate = await GetById(entity.Id);
                if (entityUpdate != null)
                {


                    _context.OssFormulas.Update(entity);
                    _context.SaveChanges();
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
                OssFormula entity = await GetById(id);
                if (entity != null)
                {
                    _context.OssFormulas.Remove(entity);
                    _context.SaveChanges();
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
                var last = await _context.OssFormulas.DefaultIfEmpty()
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

