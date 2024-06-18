using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisSourceRepository
{
    Task<List<SIS_SOURCE>> GetALL();
    Task<SIS_SOURCE> GetById(int codigo);
    Task<ResultDto<SIS_SOURCE>> Add(SIS_SOURCE entity);
    Task<ResultDto<SIS_SOURCE>> Update(SIS_SOURCE entity);
    Task<int> GetNextKey();
    Task<ResultDto<string>> GetDataGenericReport(string queryString);

}