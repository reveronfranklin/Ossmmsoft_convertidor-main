using Convertidor.Data.Entities.ADM;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmComunicacionProveedorRepository
{
    Task<ADM_COM_PROVEEDOR> GetByCodigo(int id);
    Task<List<ADM_COM_PROVEEDOR>> GetByProveedor(int codigoProveedor);
    Task<ResultDto<ADM_COM_PROVEEDOR>> Add(ADM_COM_PROVEEDOR entity);
    Task<ResultDto<ADM_COM_PROVEEDOR>> Update(ADM_COM_PROVEEDOR entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    Task<ADM_COM_PROVEEDOR> GetByProveedorAndPrincipal(int codigoProveedor, int principal);



}