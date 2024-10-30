using Convertidor.Data.EntitiesDestino.ADM;

namespace Convertidor.Data.DestinoInterfaces.ADM;

public interface IAdmContactosProveedorDestinoRepository
{
     Task<ResultDto<bool>>Add(List<ADM_CONTACTO_PROVEEDOR> entities);
     Task<string> Delete(int codigoProveedor);

}