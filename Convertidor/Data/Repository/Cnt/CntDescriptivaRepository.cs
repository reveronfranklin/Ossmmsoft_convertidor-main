
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntDescriptivaRepository: ICntDescriptivaRepository
    {
		
        private readonly DataContextCnt _context;

        public CntDescriptivaRepository(DataContextCnt context)
        {
            _context = context;
        }
        public async Task<CNT_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId)
        {
            try
            {
                var result = await _context.CNT_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.DESCRIPCION_ID == descripcionId).FirstOrDefaultAsync();
        
                return (CNT_DESCRIPTIVAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<CNT_DESCRIPTIVAS> GetByCodigoDescriptivaTexto(string codigo)
        {
            try
            {
                var result = await _context.CNT_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.CODIGO == codigo).FirstOrDefaultAsync();

                return (CNT_DESCRIPTIVAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<CNT_DESCRIPTIVAS>> GetByTitulo(int tituloId)
        {
            try
            {
                var result = await _context.CNT_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).ToListAsync();

                return (List<CNT_DESCRIPTIVAS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<bool> GetByIdAndTitulo(int tituloId,int id)
        {
            try
            {
                var descriptiva = await _context.CNT_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId && e.DESCRIPCION_ID==id).FirstOrDefaultAsync();
                var result = false;
                if (descriptiva != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return false;
            }

        }
        
        
        public async Task<List<CNT_DESCRIPTIVAS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_DESCRIPTIVAS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<CNT_DESCRIPTIVAS> GetByCodigo(int descripcionId)
        {
            try
            {

                var result = await _context.CNT_DESCRIPTIVAS.DefaultIfEmpty()
                    .Where(x => x.DESCRIPCION_ID == descripcionId)
                    .FirstOrDefaultAsync();
                return (CNT_DESCRIPTIVAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }


        public async Task<List<CNT_DESCRIPTIVAS>> GetByFKID(int descripcionIdFk)
        {
            try
            {

                var result = await _context.CNT_DESCRIPTIVAS.DefaultIfEmpty()
                    .Where(x => x.DESCRIPCION_FK_ID== descripcionIdFk)
                    .ToListAsync();
                return (List<CNT_DESCRIPTIVAS>)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }



        public async Task<ResultDto<CNT_DESCRIPTIVAS>> Add(CNT_DESCRIPTIVAS entity)
        {
            ResultDto<CNT_DESCRIPTIVAS> result = new ResultDto<CNT_DESCRIPTIVAS>(null);
            try
            {



                await _context.CNT_DESCRIPTIVAS.AddAsync(entity);
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

        public async Task<ResultDto<CNT_DESCRIPTIVAS>> Update(CNT_DESCRIPTIVAS entity)
        {
            ResultDto<CNT_DESCRIPTIVAS> result = new ResultDto<CNT_DESCRIPTIVAS>(null);

            try
            {
                CNT_DESCRIPTIVAS entityUpdate = await GetByCodigo(entity.DESCRIPCION_ID);
                if (entityUpdate != null)
                {


                    _context.CNT_DESCRIPTIVAS.Update(entity);
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

        public async Task<string> Delete(int descripcionId)
        {

            try
            {
                CNT_DESCRIPTIVAS entity = await GetByCodigo(descripcionId);
                if (entity != null)
                {
                    _context.CNT_DESCRIPTIVAS.Remove(entity);
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
                var last = await _context.CNT_DESCRIPTIVAS.DefaultIfEmpty()
                    .OrderByDescending(x => x.DESCRIPCION_ID)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.DESCRIPCION_ID + 1;
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

