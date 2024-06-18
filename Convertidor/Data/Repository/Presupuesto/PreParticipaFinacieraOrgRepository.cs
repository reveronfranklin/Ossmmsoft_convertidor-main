
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreParticipaFinacieraOrgRepository : IPreParticipaFinacieraOrgRepository
    {
        private readonly DataContextPre _context;

        public PreParticipaFinacieraOrgRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_PARTICIPA_FINANCIERA_ORG> GetByCodigo(int codigoParticipaFinancOrg)
        {
            try
            {
                var result = await _context.PRE_PARTICIPA_FINANCIERA_ORG.DefaultIfEmpty().Where(e => e.CODIGO_PARTICIPA_FINANC_ORG == codigoParticipaFinancOrg).FirstOrDefaultAsync();

                return (PRE_PARTICIPA_FINANCIERA_ORG)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_PARTICIPA_FINANCIERA_ORG>> GetAll()
        {
            try
            {
                var result = await _context.PRE_PARTICIPA_FINANCIERA_ORG.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<PRE_PARTICIPA_FINANCIERA_ORG>> Add(PRE_PARTICIPA_FINANCIERA_ORG entity)
        {
            ResultDto<PRE_PARTICIPA_FINANCIERA_ORG> result = new ResultDto<PRE_PARTICIPA_FINANCIERA_ORG>(null);
            try
            {



                await _context.PRE_PARTICIPA_FINANCIERA_ORG.AddAsync(entity);
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

        public async Task<ResultDto<PRE_PARTICIPA_FINANCIERA_ORG>> Update(PRE_PARTICIPA_FINANCIERA_ORG entity)
        {
            ResultDto<PRE_PARTICIPA_FINANCIERA_ORG> result = new ResultDto<PRE_PARTICIPA_FINANCIERA_ORG>(null);

            try
            {
                PRE_PARTICIPA_FINANCIERA_ORG entityUpdate = await GetByCodigo(entity.CODIGO_PARTICIPA_FINANC_ORG);
                if (entityUpdate != null)
                {


                    _context.PRE_PARTICIPA_FINANCIERA_ORG.Update(entity);
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

        public async Task<string> Delete(int codigoParticipaFinancOrg)
        {

            try
            {
                PRE_PARTICIPA_FINANCIERA_ORG entity = await GetByCodigo(codigoParticipaFinancOrg);
                if (entity != null)
                {
                    _context.PRE_PARTICIPA_FINANCIERA_ORG.Remove(entity);
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
                var last = await _context.PRE_PARTICIPA_FINANCIERA_ORG.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PARTICIPA_FINANC_ORG)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PARTICIPA_FINANC_ORG + 1;
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
