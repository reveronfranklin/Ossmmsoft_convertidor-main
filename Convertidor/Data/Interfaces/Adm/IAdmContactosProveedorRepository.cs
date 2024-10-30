using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmContactosProveedorRepository
{
    Task<ADM_CONTACTO_PROVEEDOR> GetByCodigo(int id);
    Task<List<ADM_CONTACTO_PROVEEDOR>> GetByProveedor(int codigoProveedor);
    Task<ResultDto<ADM_CONTACTO_PROVEEDOR>> Add(ADM_CONTACTO_PROVEEDOR entity);
    Task<ResultDto<ADM_CONTACTO_PROVEEDOR>> Update(ADM_CONTACTO_PROVEEDOR entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    
}