using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class RhConceptosFormulaRepository: IRhConceptosFormulaRepository
    {
		
        private readonly DataContext _context;

        public RhConceptosFormulaRepository(DataContext context)
        {
            _context = context;
        }
      
        public async Task<RH_FORMULA_CONCEPTOS> GetByCodigo(int id)
        {
            try
            {
                var result = await _context.RH_FORMULA_CONCEPTOS.DefaultIfEmpty().Where(e => e.CODIGO_FORMULA_CONCEPTO == id).FirstOrDefaultAsync();

                return (RH_FORMULA_CONCEPTOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<RH_FORMULA_CONCEPTOS>> GetByCodigoConcepto(int codigoConcepto)
        {
            try
            {
                var result = await _context.RH_FORMULA_CONCEPTOS.DefaultIfEmpty().Where(e => e.CODIGO_CONCEPTO == codigoConcepto).ToListAsync();

                return (List<RH_FORMULA_CONCEPTOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<RH_FORMULA_CONCEPTOS>> GetByAll()
        {
            try
            {
                var result = await _context.RH_FORMULA_CONCEPTOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<RH_FORMULA_CONCEPTOS>> Add(RH_FORMULA_CONCEPTOS entity)
        {
            ResultDto<RH_FORMULA_CONCEPTOS> result = new ResultDto<RH_FORMULA_CONCEPTOS>(null);
            try
            {



                await _context.RH_FORMULA_CONCEPTOS.AddAsync(entity);
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

        public async Task<ResultDto<RH_FORMULA_CONCEPTOS>> Update(RH_FORMULA_CONCEPTOS entity)
        {
            ResultDto<RH_FORMULA_CONCEPTOS> result = new ResultDto<RH_FORMULA_CONCEPTOS>(null);

            try
            {
                RH_FORMULA_CONCEPTOS entityUpdate = await GetByCodigo(entity.CODIGO_FORMULA_CONCEPTO);
                if (entityUpdate != null)
                {


                    _context.RH_FORMULA_CONCEPTOS.Update(entity);
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
                RH_FORMULA_CONCEPTOS entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.RH_FORMULA_CONCEPTOS.Remove(entity);
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
                var last = await _context.RH_FORMULA_CONCEPTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_FORMULA_CONCEPTO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_FORMULA_CONCEPTO + 1;
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

