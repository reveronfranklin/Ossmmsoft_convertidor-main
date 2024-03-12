using System;
using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Services.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
	public class BmConteoDetalleHistoricoRepository: IBmConteoDetalleHistoricoRepository
    {
		
        private readonly DataContextBm _context;
        private readonly IOssConfigServices _configServices;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public BmConteoDetalleHistoricoRepository(DataContextBm context,
                                                    ISisUsuarioRepository sisUsuarioRepository,
                                                    IOssConfigServices configServices)
        {
            _context = context;
            _configServices = configServices;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
      



        public async Task<List<BM_CONTEO_DETALLE_HISTORICO>> GetAllByConteo(int codigoConteo)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.BM_CONTEO_DETALLE_HISTORICO.DefaultIfEmpty()
                    .Where(c=>c.CODIGO_EMPRESA==conectado.Empresa && c.CODIGO_BM_CONTEO==codigoConteo)
                    .OrderBy(x=> x.UNIDAD_TRABAJO)
                    .ThenBy(x => x.NUMERO_PLACA)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
     
     
      

        
        
        public async Task<ResultDto<List<BM_CONTEO_DETALLE_HISTORICO>>> AddRange(List<BM_CONTEO_DETALLE_HISTORICO> entities)
        {
            ResultDto<List<BM_CONTEO_DETALLE_HISTORICO>> result = new ResultDto<List<BM_CONTEO_DETALLE_HISTORICO>>(null);
            try
            {



                await _context.BM_CONTEO_DETALLE_HISTORICO.AddRangeAsync(entities);
                await _context.SaveChangesAsync();


                result.Data = entities;
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

      



    }
}

