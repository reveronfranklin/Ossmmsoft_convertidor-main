namespace Convertidor.Services.Rh
{
    public interface IRhHRetencionesSsoService
    {
        Task<List<RhTmpRetencionesSsoDto>> GetRetencionesHSso(FilterRetencionesDto filter);
        Task<ResultDto<string>> Create(List<RH_H_RETENCIONES_SSO> entities);
    }
}
