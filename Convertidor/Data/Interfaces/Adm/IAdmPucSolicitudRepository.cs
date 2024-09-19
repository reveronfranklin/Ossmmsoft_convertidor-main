using Convertidor.Data.Entities.ADM;
using Convertidor.Dtos.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmPucSolicitudRepository
    {
        Task<ADM_PUC_SOLICITUD> GetCodigoPucSolicitud(int codigoPucSolicitud);
        Task<List<ADM_PUC_SOLICITUD>> GetAll();
        Task<List<ADM_PUC_SOLICITUD>> GetByDetalleSolicitud(int codigoDetalleSolicitud);
        Task<List<ADM_PUC_SOLICITUD>> GetBySolicitud(int codigoSolicitud);
        Task<ResultDto<ADM_PUC_SOLICITUD>> Add(ADM_PUC_SOLICITUD entity);
        Task<ResultDto<ADM_PUC_SOLICITUD>> Update(ADM_PUC_SOLICITUD entity);
        Task<string> Delete(int codigoPucSolicitud);
        Task<int> GetNextKey();
        Task<bool> ExistePresupuesto(int codigoPresupuesto);

        Task<bool> ExisteByDetalleSolicitud(int codigoDetalleSolicitud);
        Task<ResultDto<ADM_PUC_SOLICITUD>> GetByIcpPucFInanciado(AdmPucSolicitudUpdateDto dto);
        Task<string> UpdateMontoComprometido(int codigoSolicitud);
        Task EliminaImputacion(int codigoPresupuesto, int codigoSolicitud);
    }
}
