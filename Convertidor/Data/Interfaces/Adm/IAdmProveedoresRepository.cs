using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm.Proveedores;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmProveedoresRepository
{
    Task<ADM_PROVEEDORES> GetByCodigo(int id);
     Task<ADM_PROVEEDORES> GetByNombre(int codigoEmpresa, string nombreProveedor);
    Task<List<ADM_PROVEEDORES>> GetByAll();
    Task<ResultDto<List<ADM_PROVEEDORES>>> GetByAll(AdmProveedoresFilterDto filter);
    Task<ResultDto<ADM_PROVEEDORES>> Add(ADM_PROVEEDORES entity);
    Task<ResultDto<ADM_PROVEEDORES>> Update(ADM_PROVEEDORES entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    Task<ADM_PROVEEDORES> GetByTipo(int idTipoProveedor);



}