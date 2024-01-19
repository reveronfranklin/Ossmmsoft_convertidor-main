using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH;

public interface IRhConceptosAcumulaRepository
{
    Task<RH_CONCEPTOS_ACUMULA> GetByCodigo(int id);
    Task<List<RH_CONCEPTOS_ACUMULA>> GetByAll();
    Task<List<RH_CONCEPTOS_ACUMULA>> GetByCodigoConcepto(int codigoConcepto);
    Task<ResultDto<RH_CONCEPTOS_ACUMULA>> Add(RH_CONCEPTOS_ACUMULA entity);
    Task<ResultDto<RH_CONCEPTOS_ACUMULA>> Update(RH_CONCEPTOS_ACUMULA entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();

}