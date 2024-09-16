using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmDetalleSolicitudService
    {
        Task<ResultDto<List<AdmDetalleSolicitudResponseDto>>> GetAll();
   
        Task<ResultDto<List<AdmDetalleSolicitudResponseDto>>> GetByCodigoSolicitud(AdmSolicitudesFilterDto filter);
        Task<ResultDto<AdmDetalleSolicitudResponseDto>> Update(AdmDetalleSolicitudUpdateDto dto);
        Task<ResultDto<AdmDetalleSolicitudResponseDto>> Create(AdmDetalleSolicitudUpdateDto dto);
        Task<ResultDto<AdmDetalleSolicitudDeleteDto>> Delete(AdmDetalleSolicitudDeleteDto dto);
        Task<TotalesResponseDto> GetTotales(int codigoPresupuesto, int codigoSolicitud);
    }
}
