using Convertidor.Data.Entities.ADM;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmDireccionProveedorRepository
{
    Task<ADM_DIR_PROVEEDOR> GetByCodigo(int id);
    Task<List<ADM_DIR_PROVEEDOR>> GetByProveedor(int codigoProveedor);
    Task<ResultDto<ADM_DIR_PROVEEDOR>> Add(ADM_DIR_PROVEEDOR entity);
    Task<ResultDto<ADM_DIR_PROVEEDOR>> Update(ADM_DIR_PROVEEDOR entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    Task<ADM_DIR_PROVEEDOR> GetByCodigoProveedor(int codigoProveedor);



}