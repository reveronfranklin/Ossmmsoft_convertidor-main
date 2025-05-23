using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmPagoElectronicoRepository
{
   

    Task<ResultDto<List<ADM_PAGOS_ELECTRONICOS>>> GetByLote(int codigoEmpresa, int codigoLote, int codigoPresupuesto,
        int usuario);
}