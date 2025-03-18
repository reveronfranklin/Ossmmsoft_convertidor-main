using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class SisDescriptivaRepository: Interfaces.Sis.ISisDescriptivaRepository
    {
		

        private readonly DataContextSis _context;
        private readonly IConfiguration _configuration;
        public SisDescriptivaRepository(DataContextSis context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<SIS_DESCRIPTIVAS>> GetALL()
        {
            try
            {
                var result = await _context.SIS_DESCRIPTIVAS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        

        public async Task<SIS_DESCRIPTIVAS> GetById(int id)
        {
            try
            {
                var result = await _context.SIS_DESCRIPTIVAS.DefaultIfEmpty().Where(x => x.DESCRIPCION_ID == id).FirstOrDefaultAsync();

                
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        public async Task<SIS_DESCRIPTIVAS> GetByCodigoDescripcion(string codigoDescripcion)
        {
            try
            {
                var result = await _context.SIS_DESCRIPTIVAS.DefaultIfEmpty().Where(x => x.CODIGO_DESCRIPCION == codigoDescripcion).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        
        public async Task<SIS_DESCRIPTIVAS> GetByExtra1(string codigoDescripcion)
        {
            try
            {
                var result = await _context.SIS_DESCRIPTIVAS.DefaultIfEmpty().Where(x => x.EXTRA1 == codigoDescripcion).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<SIS_DESCRIPTIVAS>> Add(SIS_DESCRIPTIVAS entity)
        {
            ResultDto<SIS_DESCRIPTIVAS> result = new ResultDto<SIS_DESCRIPTIVAS>(null);
            try
            {

                entity.DESCRIPCION_ID = await GetNextKey();

                await _context.SIS_DESCRIPTIVAS.AddAsync(entity);
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

        public async Task<ResultDto<SIS_DESCRIPTIVAS>> Update(SIS_DESCRIPTIVAS entity)
        {
            ResultDto<SIS_DESCRIPTIVAS> result = new ResultDto<SIS_DESCRIPTIVAS>(null);

            try
            {
                SIS_DESCRIPTIVAS entityUpdate = await GetById(entity.DESCRIPCION_ID);
                if (entityUpdate != null)
                {


                    _context.SIS_DESCRIPTIVAS.Update(entity);
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

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.SIS_DESCRIPTIVAS.DefaultIfEmpty()
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

