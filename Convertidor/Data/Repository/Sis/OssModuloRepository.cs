using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class OssModuloRepository: IOssModuloRepository
    {
		
        private readonly DataContextSis _context;

        public OssModuloRepository(DataContextSis context)
        {
            _context = context;
        }
      
        public async Task<OssModulo> GetByCodigo(int id)
        {
            try
            {
                var result = await _context.OssModulos.DefaultIfEmpty().Where(e => e.Id == id).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<OssModulo> GetByCodigoLargo(string codigo)
        {
            try
            {
                var result = await _context.OssModulos.DefaultIfEmpty().Where(e => e.Codigo == codigo).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
 
        public async Task<List<OssModulo>> GetByAll()
        {
            try
            {
                var result = await _context.OssModulos.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<OssModulo>> Add(OssModulo entity)
        {
            ResultDto<OssModulo> result = new ResultDto<OssModulo>(null);
            try
            {



                await _context.OssModulos.AddAsync(entity);
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

        public async Task<ResultDto<OssModulo>> Update(OssModulo entity)
        {
            ResultDto<OssModulo> result = new ResultDto<OssModulo>(null);

            try
            {
                OssModulo entityUpdate = await GetByCodigo(entity.Id);
                if (entityUpdate != null)
                {


                    _context.OssModulos.Update(entity);
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
                OssModulo entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.OssModulos.Remove(entity);
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
                var last = await _context.OssModulos.DefaultIfEmpty()
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

