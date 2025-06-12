using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Services.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
	public class BmConteoHistoricoRepository: IBmConteoHistoricoRepository
    {
		
        private readonly DataContextBmConteo _context;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IOssConfigServices _configServices;

        public BmConteoHistoricoRepository(DataContextBmConteo context,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                            IOssConfigServices configServices)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
            _configServices = configServices;
        }
      
        public async Task<BM_CONTEO_HISTORICO> GetByCodigo(int conteoId)
        {
            try
            {
                var result = await _context.BM_CONTEO_HISTORICO.DefaultIfEmpty().Where(e => e.CODIGO_BM_CONTEO == conteoId).FirstOrDefaultAsync();

                return (BM_CONTEO_HISTORICO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

  
        public async Task<List<BM_CONTEO_HISTORICO>> GetAll()
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.BM_CONTEO_HISTORICO.DefaultIfEmpty().Where(x=>x.CODIGO_EMPRESA==conectado.Empresa).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }




        public async Task<ResultDto<bool>> Add(BM_CONTEO_HISTORICO entities)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            try
            {



                await _context.BM_CONTEO_HISTORICO.AddAsync(entities);
                await _context.SaveChangesAsync();


                result.Data = true;
                result.IsValid = true;
                result.Message = "";
                return result;


            }
            catch (Exception ex)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }



        }




    }
}

