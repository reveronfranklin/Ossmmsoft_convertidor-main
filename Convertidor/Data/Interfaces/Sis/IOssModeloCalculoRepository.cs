using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssModeloCalculoRepository
{
    Task<OssModeloCalculo> GetByCodigo(int id);
    Task<List<OssModeloCalculo>> GetByAll();
    Task<ResultDto<OssModeloCalculo>> Add(OssModeloCalculo entity);
    Task<ResultDto<OssModeloCalculo>> Update(OssModeloCalculo entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();

}