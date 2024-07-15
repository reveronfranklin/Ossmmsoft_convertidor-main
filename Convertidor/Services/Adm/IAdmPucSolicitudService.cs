using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmPucSolicitudService
    {
        Task<ResultDto<List<AdmPucSolicitudResponseDto>>> GetAll();
        Task<ResultDto<List<AdmPucSolicitudResponseDto>>> GetByDetalleSolicitud(AdmPucSolicitudFilterDto filter);
        Task<ResultDto<AdmPucSolicitudResponseDto>> Update(AdmPucSolicitudUpdateDto dto);
        Task<ResultDto<AdmPucSolicitudResponseDto>> Create(AdmPucSolicitudUpdateDto dto);
        Task<ResultDto<AdmPucSolicitudDeleteDto>> Delete(AdmPucSolicitudDeleteDto dto);
        Task<ResultDto<bool>> PresupuestoExiste(int codigoPresupuesto);
        Task<ResultDto<AdmPucSolicitudResponseDto>> GetByCodigo(int codigoPucSolicitud);
    }
}
