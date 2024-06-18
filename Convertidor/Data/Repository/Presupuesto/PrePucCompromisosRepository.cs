using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class PrePucCompromisosRepository: IPrePucCompromisosRepository
    {
		
        private readonly DataContextPre _context;

        public PrePucCompromisosRepository(DataContextPre context)
        {
            _context = context;
        }
      
        public async Task<PRE_PUC_COMPROMISOS> GetByCodigo(int codigoPucCompromiso)
        {
            try
            {
                var result = await _context.PRE_PUC_COMPROMISOS.DefaultIfEmpty().Where(e => e.CODIGO_PUC_COMPROMISO == codigoPucCompromiso).FirstOrDefaultAsync();

                return (PRE_PUC_COMPROMISOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

    
        public async Task<List<PRE_PUC_COMPROMISOS>> GetAll()
        {
            try
            {
                var result = await _context.PRE_PUC_COMPROMISOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

       

        public async Task<ResultDto<PRE_PUC_COMPROMISOS>> Add(PRE_PUC_COMPROMISOS entity)
        {
            ResultDto<PRE_PUC_COMPROMISOS> result = new ResultDto<PRE_PUC_COMPROMISOS>(null);
            try
            {



                await _context.PRE_PUC_COMPROMISOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_PUC_COMPROMISOS>> Update(PRE_PUC_COMPROMISOS entity)
        {
            ResultDto<PRE_PUC_COMPROMISOS> result = new ResultDto<PRE_PUC_COMPROMISOS>(null);

            try
            {
                PRE_PUC_COMPROMISOS entityUpdate = await GetByCodigo(entity.CODIGO_PUC_COMPROMISO);
                if (entityUpdate != null)
                {


                    _context.PRE_PUC_COMPROMISOS.Update(entity);
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

        public async Task<string> Delete(int codigoPucCompromiso)
        {

            try
            {
                PRE_PUC_COMPROMISOS entity = await GetByCodigo(codigoPucCompromiso);
                if (entity != null)
                {
                    _context.PRE_PUC_COMPROMISOS.Remove(entity);
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
                var last = await _context.PRE_PUC_COMPROMISOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PUC_COMPROMISO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PUC_COMPROMISO + 1;
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

