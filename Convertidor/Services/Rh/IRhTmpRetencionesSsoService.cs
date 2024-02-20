namespace Convertidor.Services.Rh
{
    public interface IRhTmpRetencionesSsoService
    {
        Task<ResultDto<List<RhTmpRetencionesSsoDto>>> GetRetencionesSso(FilterRetencionesDto filter);
    }
}
