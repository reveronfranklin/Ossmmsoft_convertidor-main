
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PreOrganismosRepository : IPreOrganismosRepository
    {
        private readonly DataContextPre _context;

        public PreOrganismosRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_ORGANISMOS> GetByCodigo(int codigoOrganismo)
        {
            try
            {
                var result = await _context.PRE_ORGANISMOS.DefaultIfEmpty().Where(e => e.CODIGO_ORGANISMO == codigoOrganismo).FirstOrDefaultAsync();

                return (PRE_ORGANISMOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_ORGANISMOS>> GetAll()
        {
            try
            {
                var result = await _context.PRE_ORGANISMOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<PRE_ORGANISMOS>> Add(PRE_ORGANISMOS entity)
        {
            ResultDto<PRE_ORGANISMOS> result = new ResultDto<PRE_ORGANISMOS>(null);
            try
            {



                await _context.PRE_ORGANISMOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_ORGANISMOS>> Update(PRE_ORGANISMOS entity)
        {
            ResultDto<PRE_ORGANISMOS> result = new ResultDto<PRE_ORGANISMOS>(null);

            try
            {
                PRE_ORGANISMOS entityUpdate = await GetByCodigo(entity.CODIGO_ORGANISMO);
                if (entityUpdate != null)
                {


                    _context.PRE_ORGANISMOS.Update(entity);
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

        public async Task<string> Delete(int codigoOrganismo)
        {

            try
            {
                PRE_ORGANISMOS entity = await GetByCodigo(codigoOrganismo);
                if (entity != null)
                {
                    _context.PRE_ORGANISMOS.Remove(entity);
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
                var last = await _context.PRE_ORGANISMOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ORGANISMO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ORGANISMO + 1;
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
