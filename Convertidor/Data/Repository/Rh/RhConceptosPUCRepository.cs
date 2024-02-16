using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class RhConceptosPUCRepository: IRhConceptosPUCRepository
    {
		
        private readonly DataContext _context;

        public RhConceptosPUCRepository(DataContext context)
        {
            _context = context;
        }
      
        public async Task<RH_CONCEPTOS_PUC> GetByCodigo(int id)
        {
            try
            {
                var result = await _context.RH_CONCEPTOS_PUC.DefaultIfEmpty().Where(e => e.CODIGO_CONCEPTO_PUC == id).FirstOrDefaultAsync();

                return (RH_CONCEPTOS_PUC)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<RH_CONCEPTOS_PUC>> GetByCodigoConcepto(int codigoConcepto)
        {
            try
            {
                var result = await _context.RH_CONCEPTOS_PUC.DefaultIfEmpty().Where(e => e.CODIGO_CONCEPTO == codigoConcepto).ToListAsync();

                return (List<RH_CONCEPTOS_PUC>)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<RH_CONCEPTOS_PUC>> GetByAll()
        {
            try
            {
                var result = await _context.RH_CONCEPTOS_PUC.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<RH_CONCEPTOS_PUC>> Add(RH_CONCEPTOS_PUC entity)
        {
            ResultDto<RH_CONCEPTOS_PUC> result = new ResultDto<RH_CONCEPTOS_PUC>(null);
            try
            {



                await _context.RH_CONCEPTOS_PUC.AddAsync(entity);
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

        public async Task<ResultDto<RH_CONCEPTOS_PUC>> Update(RH_CONCEPTOS_PUC entity)
        {
            ResultDto<RH_CONCEPTOS_PUC> result = new ResultDto<RH_CONCEPTOS_PUC>(null);

            try
            {
                RH_CONCEPTOS_PUC entityUpdate = await GetByCodigo(entity.CODIGO_CONCEPTO_PUC);
                if (entityUpdate != null)
                {


                    _context.RH_CONCEPTOS_PUC.Update(entity);
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

        public async Task<string> Delete(int id)
        {

            try
            {
                RH_CONCEPTOS_PUC entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.RH_CONCEPTOS_PUC.Remove(entity);
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
                var last = await _context.RH_CONCEPTOS_PUC.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CONCEPTO_PUC)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CONCEPTO_PUC + 1;
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

