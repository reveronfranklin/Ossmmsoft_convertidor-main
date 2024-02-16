using Convertidor.Data.Entities.ADM;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmPucSolicitudRepository
    {
        Task<ADM_PUC_SOLICITUD> GetCodigoPucSolicitud(int codigoPucSolicitud);
        Task<List<ADM_PUC_SOLICITUD>> GetAll();
        Task<ResultDto<ADM_PUC_SOLICITUD>> Add(ADM_PUC_SOLICITUD entity);
        Task<ResultDto<ADM_PUC_SOLICITUD>> Update(ADM_PUC_SOLICITUD entity);
        Task<string> Delete(int codigoPucSolicitud);
        Task<int> GetNextKey();
    }
}
