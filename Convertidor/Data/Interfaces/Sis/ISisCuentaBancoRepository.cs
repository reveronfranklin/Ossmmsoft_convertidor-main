using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisCuentaBancoRepository
{
    Task<SIS_CUENTAS_BANCOS> GetByCodigo(int code);
    Task<SIS_CUENTAS_BANCOS> GetById(int id);
    Task<ResultDto<List<SIS_CUENTAS_BANCOS>>> GetAll(SisCuentaBancoFilterDto filter);
    Task<ResultDto<SIS_CUENTAS_BANCOS>> Add(SIS_CUENTAS_BANCOS entity);
    Task<ResultDto<SIS_CUENTAS_BANCOS>> Update(SIS_CUENTAS_BANCOS entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();
    Task<bool> ExisteBanco(int codigoBanco);
    Task<SIS_CUENTAS_BANCOS> GetByCodigoCuenta(string codigoCuenta);
}