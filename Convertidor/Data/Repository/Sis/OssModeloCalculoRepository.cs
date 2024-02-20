using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class OssModeloCalculoRepository: IOssModeloCalculoRepository
    {
		
        private readonly DataContextSis _context;

        public OssModeloCalculoRepository(DataContextSis context)
        {
            _context = context;
        }
      
        public async Task<OssModeloCalculo> GetByCodigo(int id)
        {
            try
            {
                var result = await _context.OssModeloCalculos.DefaultIfEmpty().Where(e => e.Id == id).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
      
 
        public async Task<List<OssModeloCalculo>> GetByAll()
        {
            try
            {
                var result = await _context.OssModeloCalculos.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<OssModeloCalculo>> Add(OssModeloCalculo entity)
        {
            ResultDto<OssModeloCalculo> result = new ResultDto<OssModeloCalculo>(null);
            try
            {



                await _context.OssModeloCalculos.AddAsync(entity);
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

        public async Task<ResultDto<OssModeloCalculo>> Update(OssModeloCalculo entity)
        {
            ResultDto<OssModeloCalculo> result = new ResultDto<OssModeloCalculo>(null);

            try
            {
                OssModeloCalculo entityUpdate = await GetByCodigo(entity.Id);
                if (entityUpdate != null)
                {


                    _context.OssModeloCalculos.Update(entity);
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
                OssModeloCalculo entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.OssModeloCalculos.Remove(entity);
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
                var last = await _context.OssModeloCalculos.DefaultIfEmpty()
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

