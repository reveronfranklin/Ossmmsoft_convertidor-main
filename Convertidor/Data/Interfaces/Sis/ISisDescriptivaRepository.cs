using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisDescriptivaRepository
{
    Task<List<SIS_DESCRIPTIVAS>> GetALL();
    Task<List<SIS_DESCRIPTIVAS>> GetALLByTituloId(int tituloId);
    Task<SIS_DESCRIPTIVAS> GetById(int id);
    Task<SIS_DESCRIPTIVAS> GetByCodigoDescripcion(string codigoDescripcion);
    Task<SIS_DESCRIPTIVAS> GetByExtra1(string codigoDescripcion);
    Task<ResultDto<SIS_DESCRIPTIVAS>> Add(SIS_DESCRIPTIVAS entity);
    Task<ResultDto<SIS_DESCRIPTIVAS>> Update(SIS_DESCRIPTIVAS entity);
    Task<int> GetNextKey();
    
}