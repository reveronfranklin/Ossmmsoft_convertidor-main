namespace Convertidor.Services.Rh
{
    public interface IRhTmpRetencionesFaovService
    {
        Task<ResultDto<List<RhTmpRetencionesFaovDto>>> GetRetencionesFaov(FilterRetencionesDto filter);
    }
}
