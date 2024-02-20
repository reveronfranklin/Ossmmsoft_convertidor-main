using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssModuloRepository
{
    Task<OssModulo> GetByCodigo(int id);
    Task<OssModulo> GetByCodigoLargo(string codigo);
    Task<List<OssModulo>> GetByAll();
    Task<ResultDto<OssModulo>> Add(OssModulo entity);
    Task<ResultDto<OssModulo>> Update(OssModulo entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    

}