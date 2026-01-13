using Convertidor.Data.EntitiesDestino.ADM;

namespace Convertidor.Data.DestinoInterfaces.ADM;

public interface IAdmProveedoresDestinoRepository
{
    Task<ResultDto<bool>> Add(ADM_PROVEEDORES entities);
    Task<string> Delete(int codigoProveedor);

}