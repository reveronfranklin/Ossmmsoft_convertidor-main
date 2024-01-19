using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhPersonasMovControlRepository : IRhPersonasMovControlRepository
    {
		
        private readonly DataContext _context;

        public RhPersonasMovControlRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_PERSONAS_MOV_CONTROL>> GetAll()
        {
            try
            {

                var result = await _context.RH_PERSONAS_MOV_CONTROL.DefaultIfEmpty().ToListAsync();
                return (List<RH_PERSONAS_MOV_CONTROL>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        public async Task<RH_PERSONAS_MOV_CONTROL> GetByCodigo(int codigoPersonaMoV)
        {
            try
            {

                var result = await _context.RH_PERSONAS_MOV_CONTROL.DefaultIfEmpty()
                    .Where(tn => tn.CODIGO_PERSONA_MOV_CTRL == codigoPersonaMoV).FirstOrDefaultAsync();
                return (RH_PERSONAS_MOV_CONTROL)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
       
        public async Task<List<RH_PERSONAS_MOV_CONTROL>> GetCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_PERSONAS_MOV_CONTROL.DefaultIfEmpty()
                    .Where(e => e.CODIGO_PERSONA == codigoPersona).ToListAsync();

                return (List<RH_PERSONAS_MOV_CONTROL>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ResultDto<RH_PERSONAS_MOV_CONTROL>> Add(RH_PERSONAS_MOV_CONTROL entity)
        {
            ResultDto<RH_PERSONAS_MOV_CONTROL> result = new ResultDto<RH_PERSONAS_MOV_CONTROL>(null);
            try
            {

                await _context.RH_PERSONAS_MOV_CONTROL.AddAsync(entity);
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

        public async Task<ResultDto<RH_PERSONAS_MOV_CONTROL>> Update(RH_PERSONAS_MOV_CONTROL entity)
        {
            ResultDto<RH_PERSONAS_MOV_CONTROL> result = new ResultDto<RH_PERSONAS_MOV_CONTROL>(null);

            try
            {
                RH_PERSONAS_MOV_CONTROL entityUpdate = await GetByCodigo(entity.CODIGO_PERSONA_MOV_CTRL);
                if (entityUpdate != null)
                {


                    _context.RH_PERSONAS_MOV_CONTROL.Update(entity);
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

        public async Task<string> Delete(int CodigoPersonaMovCtrl)
        {

            try
            {
                RH_PERSONAS_MOV_CONTROL entity = await GetByCodigo(CodigoPersonaMovCtrl);
                if (entity != null)
                {
                    _context.RH_PERSONAS_MOV_CONTROL.Remove(entity);
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
                var last = await _context.RH_PERSONAS_MOV_CONTROL.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PERSONA_MOV_CTRL)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PERSONA_MOV_CTRL + 1;
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


    


