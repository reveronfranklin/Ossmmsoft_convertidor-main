namespace Convertidor.Data.Interfaces.RH;

public interface IRhFamiliaresRepository
{
    Task<List<RH_FAMILIARES>> GetByCodigoPersona(int codigoPersona);
    Task<RH_FAMILIARES> GetByCodigo(int codigoFamiliar);
    Task<ResultDto<RH_FAMILIARES>> Add(RH_FAMILIARES entity);
    Task<ResultDto<RH_FAMILIARES>> Update(RH_FAMILIARES entity);
    Task<string> Delete(int codigoComunicacion);
    Task<int> GetNextKey();
}