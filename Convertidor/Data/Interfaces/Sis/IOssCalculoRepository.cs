using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssCalculoRepository
{
    Task<OssCalculo> GetById(int id);

    Task<List<OssCalculo>> GetByIdCalculo(int idCalculo);
    Task<ResultDto<OssCalculo>> Add(OssCalculo entity);
    Task<ResultDto<OssCalculo>> Update(OssCalculo entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    Task<OssCalculo> GetByIdCalculoCode(int idCalculo, string codeVariable);
    void ExecuteQueryUpdateValor(string valueFormula, int id,string tipoVariable);
    Task<List<OssCalculo>> GetFormulasByIdCalculo(int idCalculo);

}