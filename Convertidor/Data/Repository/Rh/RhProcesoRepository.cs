using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhProcesoRepository: IRhProcesoRepository
    {
		
        private readonly DataContext _context;

        public RhProcesoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<RH_PROCESOS> GetByCodigo(int codigoProcesso)
        {
            try
            {
                var result = await _context.RH_PROCESOS.DefaultIfEmpty().Where(e => e.CODIGO_PROCESO == codigoProcesso).FirstOrDefaultAsync();
        
                return (RH_PROCESOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_PROCESOS>> GetAll()
        {
            try
            {
                var result = await _context.RH_PROCESOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ResultDto<RH_PROCESOS>> Add(RH_PROCESOS entity)
        {
            ResultDto<RH_PROCESOS> result = new ResultDto<RH_PROCESOS>(null);
            try
            {



                await _context.RH_PROCESOS.AddAsync(entity);
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

        public async Task<ResultDto<RH_PROCESOS>> Update(RH_PROCESOS entity)
        {
            ResultDto<RH_PROCESOS> result = new ResultDto<RH_PROCESOS>(null);

            try
            {
                RH_PROCESOS entityUpdate = await GetByCodigo(entity.CODIGO_PROCESO);
                if (entityUpdate != null)
                {


                    _context.RH_PROCESOS.Update(entity);
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

        public async Task<string> Delete(int codigoAdministrativo)
        {

            try
            {
                RH_PROCESOS entity = await GetByCodigo(codigoAdministrativo);
                if (entity != null)
                {
                    _context.RH_PROCESOS.Remove(entity);
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
                var last = await _context.RH_PROCESOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PROCESO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PROCESO + 1;
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

