using Convertidor.Data.EntitiesDestino.ADM;

namespace Convertidor.Data.DestinoInterfaces.ADM;

public interface IAdmBeneficiariosOpDestinoRepository
{
    Task<ResultDto<bool>> Add(List<ADM_BENEFICIARIOS_OP> entities);
    Task<string> Delete(int codigoOrdenPago);

}