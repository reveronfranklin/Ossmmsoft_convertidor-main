using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmLotePagoRepository
{
    Task<ADM_LOTE_PAGO> GetByCodigo(int codigo);
    Task<ResultDto<List<ADM_LOTE_PAGO>>> GetAll(AdmLotePagoFilterDto filter);
    Task<ResultDto<ADM_LOTE_PAGO>> Add(ADM_LOTE_PAGO entity);
    Task<ResultDto<ADM_LOTE_PAGO>> Update(ADM_LOTE_PAGO entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    Task<string> UpdateSearchText(int codigoLote);
    Task ReconstruirSearchTextPago(int codigoPresupuesto);

}