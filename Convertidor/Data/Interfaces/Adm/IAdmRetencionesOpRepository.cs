using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmRetencionesOpRepository
    {
        Task<ADM_RETENCIONES_OP> GetCodigoRetencionOp(int codigoRetencionOp);

        Task<ADM_RETENCIONES_OP> GetByOrdenPagoCodigoRetencionTipoRetencion(int codigoOrdenPago, int codigoRetencion,
            int tipoRetencionId);
        Task<List<ADM_RETENCIONES_OP>> GetAll();
        Task<List<ADM_RETENCIONES_OP>> GetByOrdenPago(int codigoOrdenPago);
        Task<ResultDto<ADM_RETENCIONES_OP>> Add(ADM_RETENCIONES_OP entity);
        Task<ResultDto<ADM_RETENCIONES_OP>> Update(ADM_RETENCIONES_OP entity);
        Task<string> Delete(int codigoRetencionOp);
        Task<string> DeleteByOrdePago(int codigoOrdenPago);
        Task<int> GetNextKey();
        Task<string> UpdateMontos(int codigoRetencionOp, decimal montoRetencion, decimal baseImponible);
    }
}
