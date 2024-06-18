namespace Convertidor.Services.Rh
{
    public interface IRhHRetencionesFjpService
    {
        Task<List<RhTmpRetencionesFjpDto>> GetRetencionesHFjp(FilterRetencionesDto filter);
        Task<ResultDto<string>> Create(List<RH_H_RETENCIONES_FJP> entities);
    }
}
