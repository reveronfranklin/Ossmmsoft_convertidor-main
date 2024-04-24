using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Services.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
	public class BmConteoRepository: IBmConteoRepository
    {
		
        private readonly DataContextBm _context;
        private readonly IOssConfigServices _configServices;

        public BmConteoRepository(DataContextBm context,IOssConfigServices configServices)
        {
            _context = context;
            _configServices = configServices;
        }
      
        public async Task<BM_CONTEO> GetByCodigo(int conteoId)
        {
            try
            {
                var result = await _context.BM_CONTEO.DefaultIfEmpty().Where(e => e.CODIGO_BM_CONTEO == conteoId).FirstOrDefaultAsync();

                return (BM_CONTEO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

  
        public async Task<List<BM_CONTEO>> GetAll()
        {
            try
            {
                var result = await _context.BM_CONTEO.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<BM_CONTEO>> Add(BM_CONTEO entity)
        {
            ResultDto<BM_CONTEO> result = new ResultDto<BM_CONTEO>(null);
            try
            {



                await _context.BM_CONTEO.AddAsync(entity);
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

        public async Task<ResultDto<BM_CONTEO>> Update(BM_CONTEO entity)
        {
            ResultDto<BM_CONTEO> result = new ResultDto<BM_CONTEO>(null);

            try
            {
                BM_CONTEO entityUpdate = await GetByCodigo(entity.CODIGO_BM_CONTEO);
                if (entityUpdate != null)
                {


                    _context.BM_CONTEO.Update(entity);
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

        public async Task<string> Delete(BM_CONTEO entity )
        {
            try
            {
           
                if (entity != null)
                {
                    _context.BM_CONTEO.Remove(entity);
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
                result = await _configServices.GetNextByClave("CODIGO_BM_CONTEO");

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

