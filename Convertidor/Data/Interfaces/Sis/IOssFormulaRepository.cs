using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssFormulaRepository
{
    Task<OssFormula> GetById(int id);
    Task<List<OssFormula>> GetByIdModeloCalculo(int idModeloCalculo);
    Task<List<OssFormula>> GetByAll();
    Task<ResultDto<OssFormula>> Add(OssFormula entity);
    Task<ResultDto<OssFormula>> Update(OssFormula entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();

}