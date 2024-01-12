using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH;

public interface IRhConceptosFormulaRepository
{
    Task<RH_FORMULA_CONCEPTOS> GetByCodigo(int id);
    Task<List<RH_FORMULA_CONCEPTOS>> GetByCodigoConcepto(int codigoConcepto);
    Task<List<RH_FORMULA_CONCEPTOS>> GetByAll();
    Task<ResultDto<RH_FORMULA_CONCEPTOS>> Add(RH_FORMULA_CONCEPTOS entity);
    Task<ResultDto<RH_FORMULA_CONCEPTOS>> Update(RH_FORMULA_CONCEPTOS entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();

}