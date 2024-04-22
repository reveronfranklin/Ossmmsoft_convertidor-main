using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm
{
    public class BmConteoDetalleHistoricoService: IBmConteoDetalleHistoricoService
    {

      
        private readonly IBmConteoDetalleHistoricoRepository _repository;
        private readonly IBM_V_BM1Service _bm1Service;

        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly IConfiguration _configuration;
        public BmConteoDetalleHistoricoService(IBmConteoDetalleHistoricoRepository repository,
                                IBM_V_BM1Service bm1Service, 
                                ISisUsuarioRepository sisUsuarioRepository,
                                IRhPersonasRepository rhPersonasRepository,
                                IConfiguration configuration)
		{
            _repository = repository;
            _bm1Service = bm1Service;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _configuration = configuration;
           

        }

    public FechaDto GetFechaDto(DateTime fecha)
    {
        var FechaDesdeObj = new FechaDto();
        FechaDesdeObj.Year = fecha.Year.ToString();
        string month = "00" + fecha.Month.ToString();
        string day = "00" + fecha.Day.ToString();
        FechaDesdeObj.Month = month.Substring(month.Length - 2);
        FechaDesdeObj.Day = day.Substring(day.Length - 2);

        return FechaDesdeObj;
    }

     public async Task<ResultDto<List<BmConteoDetalleResumenResponseDto>>> GetResumen(int codigoConteo)
    {
       
       
        ResultDto<List<BmConteoDetalleResumenResponseDto>> response = new ResultDto<List<BmConteoDetalleResumenResponseDto>>(null);
        try
        {
            
            
            var result = await _repository.GetAllByConteo(codigoConteo);
      
                var lista = from s in result
                              group s by new
                              {
                                  
                                  CodigoBmConteo = s.CODIGO_BM_CONTEO,
                                  Conteo = s.CONTEO,
                                  Cantidad=s.CANTIDAD,
                                  CantidadContada=s.CANTIDAD_CONTADA
                                
                                  
                              } into g
                              select new BmConteoDetalleResumenResponseDto()
                              {
                                 
                                  CodigoBmConteo = g.Key.CodigoBmConteo,
                                  Conteo = g.Key.Conteo,
                                  Cantidad = g.Sum(u => u.CANTIDAD),
                                  CantidadContada = g.Sum(u => u.CANTIDAD_CONTADA)
                                  
                                

                              };
                
            
           
              
                
                
            response.Data = lista.ToList();
            response.IsValid = true;
            response.Message = "";
            response.LinkData= $"";
            return response;
        }
        catch (Exception ex)
        {
            response.Data = null;
            response.IsValid = true;
            response.Message = ex.InnerException.Message;
            return response;
        }
       
    }




    public async Task<ResultDto<List<BM_CONTEO_DETALLE_HISTORICO>>> AddRange(List<BM_CONTEO_DETALLE_HISTORICO> entities)
    {
        var result = await _repository.AddRange(entities);
        return result;

    }


    public async Task<List<BM_CONTEO_DETALLE_HISTORICO>> GetAllByConteo(int codigoConteo)
    {

        return await _repository.GetAllByConteo(codigoConteo);

    }

    }
}

