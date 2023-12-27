using Convertidor.Data.Entities.ADM;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmActividadProveedorRepository
{
    Task<ADM_ACT_PROVEEDOR> GetByCodigo(int id);
    Task<List<ADM_ACT_PROVEEDOR>> GetByProveedor(int codigoProveedor);
    Task<ResultDto<ADM_ACT_PROVEEDOR>> Add(ADM_ACT_PROVEEDOR entity);
    Task<ResultDto<ADM_ACT_PROVEEDOR>> Update(ADM_ACT_PROVEEDOR entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    


}