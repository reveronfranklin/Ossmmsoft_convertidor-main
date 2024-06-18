namespace Convertidor.Services.Rh
{
    public interface IRhHRetencionesCahService
    {
        Task<List<RhTmpRetencionesCahDto>> GetRetencionesHCah(FilterRetencionesDto filter);
        Task<ResultDto<string>> Create(List<RH_H_RETENCIONES_CAH> entities);
    }
}
