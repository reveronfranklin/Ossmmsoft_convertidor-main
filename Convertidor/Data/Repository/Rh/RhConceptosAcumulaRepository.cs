using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class RhConceptosAcumulaRepository: IRhConceptosAcumulaRepository
    {
		
        private readonly DataContext _context;

        public RhConceptosAcumulaRepository(DataContext context)
        {
            _context = context;
        }
      
        public async Task<RH_CONCEPTOS_ACUMULA> GetByCodigo(int id)
        {
            try
            {
                var result = await _context.RH_CONCEPTOS_ACUMULA.DefaultIfEmpty().Where(e => e.CODIGO_CONCEPTO_ACUMULA == id).FirstOrDefaultAsync();

                return (RH_CONCEPTOS_ACUMULA)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<RH_CONCEPTOS_ACUMULA>> GetByCodigoConcepto(int codigoConcepto)
        {
            try
            {
                var result = await _context.RH_CONCEPTOS_ACUMULA.DefaultIfEmpty().Where(e => e.CODIGO_CONCEPTO == codigoConcepto).ToListAsync();

                return (List<RH_CONCEPTOS_ACUMULA>)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<RH_CONCEPTOS_ACUMULA>> GetByAll()
        {
            try
            {
                var result = await _context.RH_CONCEPTOS_ACUMULA.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<RH_CONCEPTOS_ACUMULA>> Add(RH_CONCEPTOS_ACUMULA entity)
        {
            ResultDto<RH_CONCEPTOS_ACUMULA> result = new ResultDto<RH_CONCEPTOS_ACUMULA>(null);
            try
            {



                await _context.RH_CONCEPTOS_ACUMULA.AddAsync(entity);
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

        public async Task<ResultDto<RH_CONCEPTOS_ACUMULA>> Update(RH_CONCEPTOS_ACUMULA entity)
        {
            ResultDto<RH_CONCEPTOS_ACUMULA> result = new ResultDto<RH_CONCEPTOS_ACUMULA>(null);

            try
            {
                RH_CONCEPTOS_ACUMULA entityUpdate = await GetByCodigo(entity.CODIGO_CONCEPTO_ACUMULA);
                if (entityUpdate != null)
                {


                    _context.RH_CONCEPTOS_ACUMULA.Update(entity);
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
                RH_CONCEPTOS_ACUMULA entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.RH_CONCEPTOS_ACUMULA.Remove(entity);
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
                var last = await _context.RH_CONCEPTOS_ACUMULA.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CONCEPTO_ACUMULA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CONCEPTO_ACUMULA + 1;
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

