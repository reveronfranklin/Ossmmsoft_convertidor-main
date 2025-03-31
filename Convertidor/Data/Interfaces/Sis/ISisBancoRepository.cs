using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisBancoRepository
{
    Task<SIS_BANCOS> GetByCodigo(int code);
    Task<SIS_BANCOS> GetByCodigoInterbancario(string codigoInterbancario);
    Task<SIS_BANCOS> GetById(int id);
    Task<ResultDto<List<SIS_BANCOS>>> GetAll(SisBancoFilterDto filter);
    Task<ResultDto<SIS_BANCOS>> Add(SIS_BANCOS entity);
    Task<ResultDto<SIS_BANCOS>> Update(SIS_BANCOS entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();


}