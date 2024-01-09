using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class PreCargosRepository: IPreCargosRepository
    {
		
        private readonly DataContextPre _context;

        public PreCargosRepository(DataContextPre context)
        {
            _context = context;
        }
      
        public async Task<PRE_CARGOS> GetByCodigo(int codigoCargo)
        {
            try
            {
                var result = await _context.PRE_CARGOS.DefaultIfEmpty().Where(e => e.CODIGO_CARGO == codigoCargo).FirstOrDefaultAsync();

                return (PRE_CARGOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

    
        public async Task<List<PRE_CARGOS>> GetAll()
        {
            try
            {
                var result = await _context.PRE_CARGOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<PRE_CARGOS>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {
                var result = await _context.PRE_CARGOS.Where(x=>x.CODIGO_PRESUPUESTO==codigoPresupuesto).DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }





        public async Task<ResultDto<PRE_CARGOS>> Add(PRE_CARGOS entity)
        {
            ResultDto<PRE_CARGOS> result = new ResultDto<PRE_CARGOS>(null);
            try
            {



                await _context.PRE_CARGOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_CARGOS>> Update(PRE_CARGOS entity)
        {
            ResultDto<PRE_CARGOS> result = new ResultDto<PRE_CARGOS>(null);

            try
            {
                PRE_CARGOS entityUpdate = await GetByCodigo(entity.CODIGO_CARGO);
                if (entityUpdate != null)
                {


                    _context.PRE_CARGOS.Update(entity);
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

        public async Task<string> Delete(int tituloId)
        {

            try
            {
                PRE_CARGOS entity = await GetByCodigo(tituloId);
                if (entity != null)
                {
                    _context.PRE_CARGOS.Remove(entity);
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
                var last = await _context.PRE_CARGOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CARGO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CARGO + 1;
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

