using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhDireccionesRepository : IRhDireccionesRepository
    {
		
        private readonly DataContext _context;

        public RhDireccionesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_DIRECCIONES>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_DIRECCIONES.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona).ToListAsync();
        
                return (List<RH_DIRECCIONES>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_DIRECCIONES> GetByCodigo(int codigoDireccion)
        {
            try
            {
                var result = await _context.RH_DIRECCIONES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DIRECCION == codigoDireccion)
                    .OrderBy(x => x.FECHA_INS).FirstOrDefaultAsync();

                return (RH_DIRECCIONES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<RH_DIRECCIONES>> Add(RH_DIRECCIONES entity)
        {
            ResultDto<RH_DIRECCIONES> result = new ResultDto<RH_DIRECCIONES>(null);
            try
            {



                await _context.RH_DIRECCIONES.AddAsync(entity);
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

        public async Task<ResultDto<RH_DIRECCIONES>> Update(RH_DIRECCIONES entity)
        {
            ResultDto<RH_DIRECCIONES> result = new ResultDto<RH_DIRECCIONES>(null);

            try
            {
                RH_DIRECCIONES entityUpdate = await GetByCodigo(entity.CODIGO_DIRECCION);
                if (entityUpdate != null)
                {


                    _context.RH_DIRECCIONES.Update(entity);
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

        public async Task<string> Delete(int codigoDireccion)
        {

            try
            {
                RH_DIRECCIONES entity = await GetByCodigo(codigoDireccion);
                if (entity != null)
                {
                    _context.RH_DIRECCIONES.Remove(entity);
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
                var last = await _context.RH_DIRECCIONES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DIRECCION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DIRECCION + 1;
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

