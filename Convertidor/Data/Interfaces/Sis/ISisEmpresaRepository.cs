using Convertidor.Data.Entities.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisEmpresaRepository
{
    Task<SIS_EMPRESAS> GetByCodigo(int codigoEmpresa);
}