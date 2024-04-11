using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisDetSourceRepository
{
    Task<List<SIS_DET_SOURCE>> GetALL();
    Task<SIS_DET_SOURCE> GetById(int codigo);
    Task<List<SIS_DET_SOURCE>> GetByCodigoSource(int codigoSource);
    Task<ResultDto<SIS_DET_SOURCE>> Add(SIS_DET_SOURCE entity);
    Task<ResultDto<SIS_DET_SOURCE>> Update(SIS_DET_SOURCE entity);
    Task<int> GetNextKey();
    
}