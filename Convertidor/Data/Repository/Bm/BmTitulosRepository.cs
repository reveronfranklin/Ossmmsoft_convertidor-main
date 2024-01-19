using System;
using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
	public class BmTitulosRepository: IBmTitulosRepository
    {
		
        private readonly DataContextBm _context;

        public BmTitulosRepository(DataContextBm context)
        {
            _context = context;
        }
      
        public async Task<BM_TITULOS> GetByCodigo(int tituloId)
        {
            try
            {
                var result = await _context.BM_TITULOS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).FirstOrDefaultAsync();

                return (BM_TITULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<BM_TITULOS> GetByCodigoString(string codigo)
        {
            try
            {
                var result = await _context.BM_TITULOS.DefaultIfEmpty().Where(e => e.CODIGO.Trim() == codigo).FirstOrDefaultAsync();

                return (BM_TITULOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<BM_TITULOS>> GetAll()
        {
            try
            {
                var result = await _context.BM_TITULOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<BM_TITULOS>> Add(BM_TITULOS entity)
        {
            ResultDto<BM_TITULOS> result = new ResultDto<BM_TITULOS>(null);
            try
            {



                await _context.BM_TITULOS.AddAsync(entity);
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

        public async Task<ResultDto<BM_TITULOS>> Update(BM_TITULOS entity)
        {
            ResultDto<BM_TITULOS> result = new ResultDto<BM_TITULOS>(null);

            try
            {
                BM_TITULOS entityUpdate = await GetByCodigo(entity.TITULO_ID);
                if (entityUpdate != null)
                {


                    _context.BM_TITULOS.Update(entity);
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
                BM_TITULOS entity = await GetByCodigo(tituloId);
                if (entity != null)
                {
                    _context.BM_TITULOS.Remove(entity);
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
                var last = await _context.BM_TITULOS.DefaultIfEmpty()
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

