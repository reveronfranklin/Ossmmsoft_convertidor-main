using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
    public class BmDirHBienRepository : IBmDirHBienRepository
    {
		
        private readonly DataContextBm _context;

        public BmDirHBienRepository(DataContextBm context)
        {
            _context = context;
        }
        public async Task<BM_DIR_H_BIEN> GetByCodigoHDirBien(int codigoHDirBien)
        {
            try
            {
                var result = await _context.BM_DIR_H_BIEN.DefaultIfEmpty()
                    .Where(e => e.CODIGO_H_DIR_BIEN == codigoHDirBien).FirstOrDefaultAsync();
        
                return (BM_DIR_H_BIEN)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<BM_DIR_H_BIEN> GetByCodigoDirBien(int codigoDirBien)
        {
            try
            {
                var result = await _context.BM_DIR_H_BIEN.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DIR_BIEN == codigoDirBien).FirstOrDefaultAsync();

                return (BM_DIR_H_BIEN)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<BM_DIR_H_BIEN> GetByCodigoIcp(int codigoIcp)
        {
            try
            {
                var result = await _context.BM_DIR_H_BIEN.DefaultIfEmpty()
                    .Where(e => e.CODIGO_ICP == codigoIcp).FirstOrDefaultAsync();

                return (BM_DIR_H_BIEN)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
       
        public async Task<List<BM_DIR_H_BIEN>> GetAll()
        {
            try
            {
                var result = await _context.BM_DIR_H_BIEN.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<BM_DIR_H_BIEN>> Add(BM_DIR_H_BIEN entity)
        {
            ResultDto<BM_DIR_H_BIEN> result = new ResultDto<BM_DIR_H_BIEN>(null);
            try
            {



                await _context.BM_DIR_H_BIEN.AddAsync(entity);
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

        public async Task<ResultDto<BM_DIR_H_BIEN>> Update(BM_DIR_H_BIEN entity)
        {
            ResultDto<BM_DIR_H_BIEN> result = new ResultDto<BM_DIR_H_BIEN>(null);

            try
            {
                BM_DIR_H_BIEN entityUpdate = await GetByCodigoHDirBien(entity.CODIGO_H_DIR_BIEN);
                if (entityUpdate != null)
                {


                    _context.BM_DIR_H_BIEN.Update(entity);
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

        public async Task<string> Delete(int codigoHDirBien)
        {

            try
            {
                BM_DIR_H_BIEN entity = await GetByCodigoHDirBien(codigoHDirBien);
                if (entity != null)
                {
                    _context.BM_DIR_H_BIEN.Remove(entity);
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
                var last = await _context.BM_DIR_H_BIEN.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_H_DIR_BIEN)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_H_DIR_BIEN + 1;
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

