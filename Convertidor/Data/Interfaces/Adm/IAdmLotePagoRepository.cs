using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmLotePagoRepository
{
    Task<ADM_LOTE_PAGO> GetByCodigo(int codigo);
    Task<ResultDto<List<ADM_LOTE_PAGO>>> GetAll(AdmLotePagoFilterDto filter);
}