using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssCalculoRepository
{
    Task<OssCalculo> GetById(int id);

    Task<List<OssCalculo>> GetByIdCalculo(int idCalculo);
    Task<ResultDto<OssCalculo>> Add(OssCalculo entity);
    Task<ResultDto<OssCalculo>> Update(OssCalculo entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();

}