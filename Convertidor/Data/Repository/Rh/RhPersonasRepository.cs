using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhPersonasRepository: IRhPersonasRepository
    {
		
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public RhPersonasRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<List<RH_PERSONAS>> GetAll()
        {
            try
            {

                var result = await _context.RH_PERSONAS.DefaultIfEmpty().ToListAsync();
                return (List<RH_PERSONAS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_PERSONAS> GetCedula(int cedula)
        {
            try
            {

                var result = await _context.RH_PERSONAS.DefaultIfEmpty().Where(p=> p.CEDULA==cedula).FirstOrDefaultAsync();
                return (RH_PERSONAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_PERSONAS> GetCodigoPersona(int codigoPersona)
        {
            try
            {

                var result = await _context.RH_PERSONAS.DefaultIfEmpty().Where(p => p.CODIGO_PERSONA == codigoPersona).FirstOrDefaultAsync();
                return (RH_PERSONAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<RH_PERSONAS>> Add(RH_PERSONAS entity)
        {
            ResultDto<RH_PERSONAS> result = new ResultDto<RH_PERSONAS>(null);
            try
            {

                var settings = _configuration.GetSection("urlReport").Get<Settings>();
                var empresString = @settings.EmpresaConfig;
                var empresa = Int32.Parse(empresString);
                entity.CODIGO_EMPRESA = empresa;
                await _context.RH_PERSONAS.AddAsync(entity);
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

        public async Task<ResultDto<RH_PERSONAS>> Update(RH_PERSONAS entity)
        {
            ResultDto<RH_PERSONAS> result = new ResultDto<RH_PERSONAS>(null);
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var empresString = @settings.EmpresaConfig;
            var empresa = Int32.Parse(empresString);
            try
            {
                

                    entity.CODIGO_EMPRESA = empresa;
                    _context.RH_PERSONAS.Update(entity);
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

        public async Task<string> Delete(int codigoRelacionCargo)
        {

            try
            {
                RH_PERSONAS entity = await GetCodigoPersona(codigoRelacionCargo);
                if (entity != null)
                {
                    _context.RH_PERSONAS.Remove(entity);
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
                var last = await _context.RH_PERSONAS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PERSONA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PERSONA + 1;
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

