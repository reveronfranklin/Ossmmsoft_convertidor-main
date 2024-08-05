using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmProductosRepository
{
    Task<ADM_PRODUCTOS> GetByCodigo(int codigoProducto);
    Task<List<ADM_PRODUCTOS>> GetAll();
    Task<ResultDto<ADM_PRODUCTOS>> Add(ADM_PRODUCTOS entity);
    Task<ResultDto<ADM_PRODUCTOS>> Update(ADM_PRODUCTOS entity);
    Task<string> Delete(int codigoProducto);
    Task<int> GetNextKey();
    Task<ResultDto<List<AdmProductosResponse>>> GetAllPaginate(AdmProductosFilterDto filter);
    Task<ADM_PRODUCTOS> GetByCodigoReal(string codigoReal);
    Task<List<ADM_PRODUCTOS>> UpdateProductosCache();
}