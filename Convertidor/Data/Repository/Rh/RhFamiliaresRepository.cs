using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhFamiliaresRepository : IRhFamiliaresRepository
    {
		
        private readonly DataContext _context;

        public RhFamiliaresRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_FAMILIARES>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_FAMILIARES.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona).ToListAsync();
        
                return (List<RH_FAMILIARES>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_FAMILIARES> GetByCodigo(int codigoFamiliar)
        {
            try
            {
                var result = await _context.RH_FAMILIARES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_FAMILIAR == codigoFamiliar)
                    .FirstOrDefaultAsync();

                return (RH_FAMILIARES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<RH_FAMILIARES>> Add(RH_FAMILIARES entity)
        {
            ResultDto<RH_FAMILIARES> result = new ResultDto<RH_FAMILIARES>(null);
            try
            {



                await _context.RH_FAMILIARES.AddAsync(entity);
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

        public async Task<ResultDto<RH_FAMILIARES>> Update(RH_FAMILIARES entity)
        {
            ResultDto<RH_FAMILIARES> result = new ResultDto<RH_FAMILIARES>(null);

            try
            {
                RH_FAMILIARES entityUpdate = await GetByCodigo(entity.CODIGO_FAMILIAR);
                if (entityUpdate != null)
                {


                    _context.RH_FAMILIARES.Update(entity);
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

        public async Task<string> Delete(int codigoFamiliar)
        {

            try
            {
                RH_FAMILIARES entity = await GetByCodigo(codigoFamiliar);
                if (entity != null)
                {
                    _context.RH_FAMILIARES.Remove(entity);
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
                var last = await _context.RH_FAMILIARES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_FAMILIAR)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_FAMILIAR + 1;
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

