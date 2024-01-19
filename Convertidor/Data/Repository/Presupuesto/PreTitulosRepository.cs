using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class PreTitulosRepository: IPreTitulosRepository
    {
		
        private readonly DataContextPre _context;

        public PreTitulosRepository(DataContextPre context)
        {
            _context = context;
        }
      
        public async Task<PRE_TITULOS> GetByCodigo(int tituloId)
        {
            try
            {
                var result = await _context.PRE_TITULOS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).FirstOrDefaultAsync();

                return (PRE_TITULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<PRE_TITULOS> GetByCodigoString(string codigo)
        {
            try
            {
                var result = await _context.PRE_TITULOS.DefaultIfEmpty().Where(e => e.CODIGO.Trim() == codigo).FirstOrDefaultAsync();

                return (PRE_TITULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_TITULOS>> GetAll()
        {
            try
            {
                var result = await _context.PRE_TITULOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<PRE_TITULOS>> Add(PRE_TITULOS entity)
        {
            ResultDto<PRE_TITULOS> result = new ResultDto<PRE_TITULOS>(null);
            try
            {



                await _context.PRE_TITULOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_TITULOS>> Update(PRE_TITULOS entity)
        {
            ResultDto<PRE_TITULOS> result = new ResultDto<PRE_TITULOS>(null);

            try
            {
                PRE_TITULOS entityUpdate = await GetByCodigo(entity.TITULO_ID);
                if (entityUpdate != null)
                {


                    _context.PRE_TITULOS.Update(entity);
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
                PRE_TITULOS entity = await GetByCodigo(tituloId);
                if (entity != null)
                {
                    _context.PRE_TITULOS.Remove(entity);
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
                var last = await _context.PRE_TITULOS.DefaultIfEmpty()
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

