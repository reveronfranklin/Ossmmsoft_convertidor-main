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
        private int Empresa => int.TryParse(_configuration["settings:EmpresaConfig"], out var empresa) && empresa > 0
            ? empresa : throw new InvalidOperationException("EmpresaConfig debe ser un numero valido.");

        public async Task<List<RH_PERSONAS>> GetAll()
        {
            var empresa = Empresa;
            try
            {

                var result = await _context.RH_PERSONAS.DefaultIfEmpty().Where(p=> p.CODIGO_PERSONA>0 && p.CODIGO_EMPRESA == empresa).ToListAsync();
                return (List<RH_PERSONAS>)result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error tecnico al consultar Personas: {ex.Message}", ex);
            }

        }

        public async Task<RH_PERSONAS> GetCedula(int cedula)
        {
            var empresa = Empresa;
            try
            {

                var result = await _context.RH_PERSONAS.DefaultIfEmpty().Where(p=> p.CEDULA==cedula && p.CODIGO_EMPRESA == empresa).FirstOrDefaultAsync();
                return (RH_PERSONAS)result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error tecnico al consultar Personas: {ex.Message}", ex);
            }

        }

        public async Task<RH_PERSONAS> GetCodigoPersona(int codigoPersona)
        {
            var empresa = Empresa;
            try
            {

                var result = await _context.RH_PERSONAS.DefaultIfEmpty().Where(p => p.CODIGO_PERSONA == codigoPersona && p.CODIGO_EMPRESA == empresa).FirstOrDefaultAsync();
                return (RH_PERSONAS)result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error tecnico al consultar Personas: {ex.Message}", ex);
            }

        }

        public async Task<ResultDto<RH_PERSONAS>> Add(RH_PERSONAS entity)
        {
            ResultDto<RH_PERSONAS> result = new ResultDto<RH_PERSONAS>(null);
            try
            {

          
                entity.CODIGO_EMPRESA = Empresa;
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
                result.Message = $"Error tecnico: {ex.InnerException?.Message ?? ex.Message}";
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
                    _context.Entry(entity).Property(p => p.ESTADO_SALUD_ID).IsModified = entity.EstadoSaludPresente;
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
                result.Message = $"Error tecnico: {ex.InnerException?.Message ?? ex.Message}";
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

