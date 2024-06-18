using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhEducacionRepository: IRhEducacionRepository
    {
		
        private readonly DataContext _context;

        public RhEducacionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_EDUCACION>> GetAll()
        {
            try
            {

                var result = await _context.RH_EDUCACION.DefaultIfEmpty().ToListAsync();
                return (List<RH_EDUCACION>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_EDUCACION>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {

                var result = await _context.RH_EDUCACION.DefaultIfEmpty().Where(e=>e.CODIGO_PERSONA==codigoPersona).ToListAsync();
                return (List<RH_EDUCACION>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_EDUCACION> GetByCodigo(int codigoEducacion)
        {
            try
            {
                var result = await _context.RH_EDUCACION.DefaultIfEmpty()
                    .Where(e => e.CODIGO_EDUCACION == codigoEducacion)
                    .OrderBy(x => x.FECHA_INI).FirstOrDefaultAsync();

                return (RH_EDUCACION)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<RH_EDUCACION>> Add(RH_EDUCACION entity)
        {
            ResultDto<RH_EDUCACION> result = new ResultDto<RH_EDUCACION>(null);
            try
            {



                await _context.RH_EDUCACION.AddAsync(entity);
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

        public async Task<ResultDto<RH_EDUCACION>> Update(RH_EDUCACION entity)
        {
            ResultDto<RH_EDUCACION> result = new ResultDto<RH_EDUCACION>(null);

            try
            {
                RH_EDUCACION entityUpdate = await GetByCodigo(entity.CODIGO_EDUCACION);
                if (entityUpdate != null)
                {
                    _context.RH_EDUCACION.Update(entity);
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

        public async Task<string> Delete(int codigoEducacion)
        {

            try
            {
                RH_EDUCACION entity = await GetByCodigo(codigoEducacion);
                if (entity != null)
                {
                    _context.RH_EDUCACION.Remove(entity);
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
                var last = await _context.RH_EDUCACION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_EDUCACION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_EDUCACION + 1;
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

