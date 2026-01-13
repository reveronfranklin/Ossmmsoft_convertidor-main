using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Dtos.Adm.PreOrdenPago;

namespace Convertidor.Data.DestinoInterfaces.ADM;

public interface IAdmPreOrdenPagoRepository
{
    Task<ADM_PRE_ORDEN_PAGO> GetById(int id);
    Task<ResultDto<List<ADM_PRE_ORDEN_PAGO>>> GetAll(AdmPreOrdenPagoFilterDto filter);
    Task<ADM_PRE_ORDEN_PAGO> GetByNumeroFactura(string numeroFactura);
    Task<ADM_PRE_ORDEN_PAGO> GetByRifNumeroFactura(string rif, string numeroFactura);
    Task<ResultDto<ADM_PRE_ORDEN_PAGO>> Add(ADM_PRE_ORDEN_PAGO entity);
    Task<ResultDto<ADM_PRE_ORDEN_PAGO>> Update(ADM_PRE_ORDEN_PAGO entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    Task<ResultDto<string>> DeleteALL();

}