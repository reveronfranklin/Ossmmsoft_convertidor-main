using System.Security.Claims;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_RELACION_CARGOSRepository : IPRE_RELACION_CARGOSRepository
    {
		
        private readonly DataContextPre _context;
        private readonly IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        public PRE_RELACION_CARGOSRepository(DataContextPre context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


     
        public async Task<List<PRE_RELACION_CARGOS>> GetAll()
        {
            try
            {

                var result = await _context.PRE_RELACION_CARGOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<List<PRE_RELACION_CARGOS>> GetAllByCodigoPresupuesto(int codigoPresupuesto)
        {
            try
            {

                var result = await _context.PRE_RELACION_CARGOS.Where(x=>x.CODIGO_PRESUPUESTO==codigoPresupuesto).DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }


        public async Task<PRE_RELACION_CARGOS> GetByPresupuestoIcpCargo(int codigoPresupuesto,int codigoIcp,int codigoCargo)
        {
            try
            {

                var result = await _context.PRE_RELACION_CARGOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_ICP==codigoIcp && x.CODIGO_CARGO == codigoCargo)
                    .FirstOrDefaultAsync();
                return (PRE_RELACION_CARGOS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }
        public async Task<PRE_RELACION_CARGOS> GetByPresupuestoIcp(int codigoPresupuesto, int codigoIcp)
        {
            try
            {

                var result = await _context.PRE_RELACION_CARGOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_ICP == codigoIcp)
                    .FirstOrDefaultAsync();
                return (PRE_RELACION_CARGOS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }

        public async Task<PRE_RELACION_CARGOS> GetByCodigo(int codigoRelacionCargo)
        {
            try
            {

                var result = await _context.PRE_RELACION_CARGOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_RELACION_CARGO == codigoRelacionCargo)
                    .FirstOrDefaultAsync();
                return (PRE_RELACION_CARGOS)result!;
               
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }

        public async Task<List<PRE_RELACION_CARGOS>> GetByCodigoCargo(int codigoCargo)
        {
            try
            {

                var result = await _context.PRE_RELACION_CARGOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_CARGO == codigoCargo)
                    .ToListAsync();
                return (List<PRE_RELACION_CARGOS>)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }

        public async Task<ResultDto<PRE_RELACION_CARGOS>> GetByIcp(int codigoIcp)
        {

            ResultDto<PRE_RELACION_CARGOS> result = new ResultDto<PRE_RELACION_CARGOS>(null);

            try
            {

                var presupuesto = await _context.PRE_RELACION_CARGOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_ICP == codigoIcp)
                    .FirstOrDefaultAsync();
                result.Data = presupuesto;
                result.IsValid = true;
                result.Message = "";
                return result;
               

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.InnerException.Message;
                return result;
               
            }



        }


  

        public async Task<ResultDto<PRE_RELACION_CARGOS>> Add(PRE_RELACION_CARGOS entity)
        {
            ResultDto<PRE_RELACION_CARGOS> result = new ResultDto<PRE_RELACION_CARGOS>(null);
            try
            {

                var settings = _configuration.GetSection("Settings").Get<Settings>();
                var empresString = @settings.EmpresaConfig;
                var empresa = Int32.Parse(empresString);
                entity.CODIGO_EMPRESA = empresa;
                await _context.PRE_RELACION_CARGOS.AddAsync(entity);
                _context.SaveChanges();


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

        public async Task<ResultDto<PRE_RELACION_CARGOS>> Update(PRE_RELACION_CARGOS entity)
        {
            ResultDto<PRE_RELACION_CARGOS> result = new ResultDto<PRE_RELACION_CARGOS>(null);
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var empresString = @settings.EmpresaConfig;
            var empresa = Int32.Parse(empresString);
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name); //_service.GetMyName();

            try
            {
                PRE_RELACION_CARGOS entityUpdate = await GetByCodigo(entity.CODIGO_RELACION_CARGO);
                if (entityUpdate != null)
                {

                    entityUpdate.CODIGO_EMPRESA = empresa;
                    _context.PRE_RELACION_CARGOS.Update(entity);
                    _context.SaveChanges();
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

        public async Task<string> Delete(int codigoRelacionCargo)
        {

            try
            {
                PRE_RELACION_CARGOS entity = await GetByCodigo(codigoRelacionCargo);
                if (entity != null)
                {
                    _context.PRE_RELACION_CARGOS.Remove(entity);
                    _context.SaveChanges();
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
                var last = await _context.PRE_RELACION_CARGOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_RELACION_CARGO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result= last.CODIGO_RELACION_CARGO + 1;
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

