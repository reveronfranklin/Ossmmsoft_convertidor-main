using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntTitulosRepository: ICntTitulosRepository
    {
		
        private readonly DataContextCnt _context;

        public CntTitulosRepository(DataContextCnt context)
        {
            _context = context;
        }
      
        public async Task<CNT_TITULOS> GetByCodigo(int tituloId)
        {
            try
            {
                var result = await _context.CNT_TITULOS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).FirstOrDefaultAsync();

                return (CNT_TITULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<CNT_TITULOS> GetByCodigoString(string codigo)
        {
            try
            {
                var result = await _context.CNT_TITULOS.DefaultIfEmpty().Where(e => e.CODIGO.Trim() == codigo).FirstOrDefaultAsync();

                return (CNT_TITULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<CNT_TITULOS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_TITULOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<CNT_TITULOS>> Add(CNT_TITULOS entity)
        {
            ResultDto<CNT_TITULOS> result = new ResultDto<CNT_TITULOS>(null);
            try
            {



                await _context.CNT_TITULOS.AddAsync(entity);
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

        public async Task<ResultDto<CNT_TITULOS>> Update(CNT_TITULOS entity)
        {
            ResultDto<CNT_TITULOS> result = new ResultDto<CNT_TITULOS>(null);

            try
            {
                CNT_TITULOS entityUpdate = await GetByCodigo(entity.TITULO_ID);
                if (entityUpdate != null)
                {


                    _context.CNT_TITULOS.Update(entity);
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

        public async Task<string> Delete(int tituloId)
        {

            try
            {
                CNT_TITULOS entity = await GetByCodigo(tituloId);
                if (entity != null)
                {
                    _context.CNT_TITULOS.Remove(entity);
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
                var last = await _context.CNT_TITULOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.TITULO_ID)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.TITULO_ID + 1;
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

