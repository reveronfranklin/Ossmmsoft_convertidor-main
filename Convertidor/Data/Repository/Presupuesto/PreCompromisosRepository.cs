
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PreCompromisosRepository: IPreCompromisosRepository
    {
		
        private readonly DataContextPre _context;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreCompromisosRepository(DataContextPre context ,ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
      
        public async Task<PRE_COMPROMISOS> GetByCodigo(int codigoCompromiso)
        {
            try
            {
                var result = await _context.PRE_COMPROMISOS.DefaultIfEmpty().Where(e => e.CODIGO_COMPROMISO == codigoCompromiso).FirstOrDefaultAsync();

                return (PRE_COMPROMISOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

    
        public async Task<List<PRE_COMPROMISOS>> GetAll()
        {
            try
            {
                var result = await _context.PRE_COMPROMISOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

       

        public async Task<ResultDto<PRE_COMPROMISOS>> Add(PRE_COMPROMISOS entity)
        {
            ResultDto<PRE_COMPROMISOS> result = new ResultDto<PRE_COMPROMISOS>(null);
            try
            {



                await _context.PRE_COMPROMISOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_COMPROMISOS>> Update(PRE_COMPROMISOS entity)
        {
            ResultDto<PRE_COMPROMISOS> result = new ResultDto<PRE_COMPROMISOS>(null);

            try
            {
                PRE_COMPROMISOS entityUpdate = await GetByCodigo(entity.CODIGO_COMPROMISO);
                if (entityUpdate != null)
                {


                    _context.PRE_COMPROMISOS.Update(entity);
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

        public async Task<string> Delete(int codigoCompromiso)
        {

            try
            {
                PRE_COMPROMISOS entity = await GetByCodigo(codigoCompromiso);
                if (entity != null)
                {
                    _context.PRE_COMPROMISOS.Remove(entity);
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
                var last = await _context.PRE_COMPROMISOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_COMPROMISO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_COMPROMISO + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

        public async Task<PRE_COMPROMISOS> GetByNumeroCompromiso(string numeroCompromiso)
        {
            try
            {
                
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_COMPROMISOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_EMPRESA == conectado.Empresa && e.NUMERO_COMPROMISO == numeroCompromiso)
                    .FirstOrDefaultAsync();

                return (PRE_COMPROMISOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


    }
}

