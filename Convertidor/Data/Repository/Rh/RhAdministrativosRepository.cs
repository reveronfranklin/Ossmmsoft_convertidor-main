using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
    public class RhAdministrativosRepository : IRhAdministrativosRepository
    {
		
        private readonly DataContext _context;

        public RhAdministrativosRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_ADMINISTRATIVOS>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_ADMINISTRATIVOS.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona).ToListAsync();
        
                return (List<RH_ADMINISTRATIVOS>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<RH_ADMINISTRATIVOS> GetPrimerMovimientoCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _context.RH_ADMINISTRATIVOS.DefaultIfEmpty().Where(e => e.CODIGO_PERSONA == codigoPersona)
                            .OrderBy(x=>x.FECHA_INGRESO).FirstOrDefaultAsync();

                return (RH_ADMINISTRATIVOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_ADMINISTRATIVOS> GetByCodigo(int codigoAdministrativo)
        {
            try
            {
                var result = await _context.RH_ADMINISTRATIVOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_ADMINISTRATIVO == codigoAdministrativo)
                    .OrderBy(x=>x.FECHA_INGRESO).FirstOrDefaultAsync();

                return (RH_ADMINISTRATIVOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<RH_ADMINISTRATIVOS>> Add(RH_ADMINISTRATIVOS entity)
        {
            ResultDto<RH_ADMINISTRATIVOS> result = new ResultDto<RH_ADMINISTRATIVOS>(null);
            try
            {



                await _context.RH_ADMINISTRATIVOS.AddAsync(entity);
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

        public async Task<ResultDto<RH_ADMINISTRATIVOS>> Update(RH_ADMINISTRATIVOS entity)
        {
            ResultDto<RH_ADMINISTRATIVOS> result = new ResultDto<RH_ADMINISTRATIVOS>(null);

            try
            {
                RH_ADMINISTRATIVOS entityUpdate = await GetByCodigo(entity.CODIGO_ADMINISTRATIVO);
                if (entityUpdate != null)
                {


                    _context.RH_ADMINISTRATIVOS.Update(entity);
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

        public async Task<string> Delete(int codigoAdministrativo)
        {

            try
            {
                RH_ADMINISTRATIVOS entity = await GetByCodigo(codigoAdministrativo);
                if (entity != null)
                {
                    _context.RH_ADMINISTRATIVOS.Remove(entity);
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
                var last = await _context.RH_ADMINISTRATIVOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ADMINISTRATIVO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ADMINISTRATIVO + 1;
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

