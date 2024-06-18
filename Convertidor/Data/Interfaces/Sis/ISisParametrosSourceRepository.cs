using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisParametrosSourceRepository
{
    Task<List<SIS_P_SOURCE>> GetALL();
    Task<SIS_P_SOURCE> GetById(int codigo);
    Task<List<SIS_P_SOURCE>> GetByCodigoSource(int codigoSource);
    Task<ResultDto<SIS_P_SOURCE>> Add(SIS_P_SOURCE entity);
    Task<ResultDto<SIS_P_SOURCE>> Update(SIS_P_SOURCE entity);
    Task<int> GetNextKey();
    
}