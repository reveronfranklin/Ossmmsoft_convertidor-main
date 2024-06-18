using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhAriRepository : IRhAriRepository
    {
		
        private readonly DataContext _context;

        public RhAriRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_ARI>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_ARI.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona).ToListAsync();
        
                return (List<RH_ARI>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        

        public async Task<RH_ARI> GetByCodigo(int codigoAri)
        {
            try
            {
                var result = await _context.RH_ARI.DefaultIfEmpty()
                    .Where(e => e.CODIGO_ARI == codigoAri)
                    .OrderBy(x=>x.FECHA_ARI).FirstOrDefaultAsync();

                return (RH_ARI)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<RH_ARI>> Add(RH_ARI entity)
        {
            ResultDto<RH_ARI> result = new ResultDto<RH_ARI>(null);
            try
            {



                await _context.RH_ARI.AddAsync(entity);
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

        public async Task<ResultDto<RH_ARI>> Update(RH_ARI entity)
        {
            ResultDto<RH_ARI> result = new ResultDto<RH_ARI>(null);

            try
            {
                RH_ARI entityUpdate = await GetByCodigo(entity.CODIGO_ARI);
                if (entityUpdate != null)
                {


                    _context.RH_ARI.Update(entity);
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

        public async Task<string> Delete(int codigoAri)
        {

            try
            {
                RH_ARI entity = await GetByCodigo(codigoAri);
                if (entity != null)
                {
                    _context.RH_ARI.Remove(entity);
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
                var last = await _context.RH_ARI.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ARI)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ARI + 1;
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

