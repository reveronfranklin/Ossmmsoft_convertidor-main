using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssVariableRepository
{
    Task<OssVariable> GetByCodigo(string id);
    Task<OssVariable> GetById(int id);
    Task<List<OssVariable>> GetByAll();
    Task<ResultDto<OssVariable>> Add(OssVariable entity);
    Task<ResultDto<OssVariable>> Update(OssVariable entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    

}