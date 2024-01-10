using Convertidor.Data.Entities.ADM;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmProveedoresRepository
{
    Task<ADM_PROVEEDORES> GetByCodigo(int id);
    Task<List<ADM_PROVEEDORES>> GetByAll();
    Task<ResultDto<ADM_PROVEEDORES>> Add(ADM_PROVEEDORES entity);
    Task<ResultDto<ADM_PROVEEDORES>> Update(ADM_PROVEEDORES entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    


}