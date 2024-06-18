namespace Convertidor.Data.Interfaces.RH;

public interface IRhConceptosPUCRepository
{
    Task<RH_CONCEPTOS_PUC> GetByCodigo(int id);
    Task<List<RH_CONCEPTOS_PUC>> GetByCodigoConcepto(int codigoConcepto);
    Task<List<RH_CONCEPTOS_PUC>> GetByAll();
    Task<ResultDto<RH_CONCEPTOS_PUC>> Add(RH_CONCEPTOS_PUC entity);
    Task<ResultDto<RH_CONCEPTOS_PUC>> Update(RH_CONCEPTOS_PUC entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    
}