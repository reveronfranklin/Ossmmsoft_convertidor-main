namespace Convertidor.Services.Rh
{
    public interface IRhHRetencionesFaovService
    {
        Task<List<RhTmpRetencionesFaovDto>> GetRetencionesHFaov(FilterRetencionesDto filter);
        Task<ResultDto<string>> Create(List<RH_H_RETENCIONES_FAOV> entities);
    }
}
