using Convertidor.Data.EntitiesDestino.ADM;

namespace Convertidor.Data.DestinoInterfaces.ADM;

public interface IAdmDescriptivaDestinoRepository
{
    Task<ResultDto<bool>> Add(ADM_DESCRIPTIVAS entities);
    Task<string> Delete(int idDescriptiva);

}