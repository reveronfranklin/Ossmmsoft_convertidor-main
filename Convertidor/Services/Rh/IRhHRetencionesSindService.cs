namespace Convertidor.Services.Rh
{
    public interface IRhHRetencionesSindService
    {
        Task<List<RhTmpRetencionesSindDto>> GetRetencionesHSind(FilterRetencionesDto filter);
        Task<ResultDto<string>> Create(List<RH_H_RETENCIONES_SIND> entities);
    }
}
