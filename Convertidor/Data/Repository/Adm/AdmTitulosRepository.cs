using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmTitulosRepository: IAdmTitulosRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmTitulosRepository(DataContextAdm context)
        {
            _context = context;
        }
      
        public async Task<ADM_TITULOS> GetByCodigo(int tituloId)
        {
            try
            {
                var result = await _context.ADM_TITULOS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).FirstOrDefaultAsync();

                return (ADM_TITULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ADM_TITULOS> GetByCodigoString(string codigo)
        {
            try
            {
                var result = await _context.ADM_TITULOS.DefaultIfEmpty().Where(e => e.CODIGO.Trim() == codigo).FirstOrDefaultAsync();

                return (ADM_TITULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_TITULOS>> GetAll()
        {
            try
            {
                var result = await _context.ADM_TITULOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<ADM_TITULOS>> Add(ADM_TITULOS entity)
        {
            ResultDto<ADM_TITULOS> result = new ResultDto<ADM_TITULOS>(null);
            try
            {



                await _context.ADM_TITULOS.AddAsync(entity);
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

        public async Task<ResultDto<ADM_TITULOS>> Update(ADM_TITULOS entity)
        {
            ResultDto<ADM_TITULOS> result = new ResultDto<ADM_TITULOS>(null);

            try
            {
                ADM_TITULOS entityUpdate = await GetByCodigo(entity.TITULO_ID);
                if (entityUpdate != null)
                {


                    _context.ADM_TITULOS.Update(entity);
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

        public async Task<string> Delete(int tituloId)
        {

            try
            {
                ADM_TITULOS entity = await GetByCodigo(tituloId);
                if (entity != null)
                {
                    _context.ADM_TITULOS.Remove(entity);
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
                var last = await _context.ADM_TITULOS.DefaultIfEmpty()
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

