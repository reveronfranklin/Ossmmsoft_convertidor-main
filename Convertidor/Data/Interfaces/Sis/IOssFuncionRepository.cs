using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Sis;

public interface IOssFuncionRepository
{
    Task<OssFuncion> GetByCodigo(int id);
    Task<OssFuncion> GetByFuncion(string funcion);
    Task<List<OssFuncion>> GetByAll();
    Task<ResultDto<OssFuncion>> Add(OssFuncion entity);
    Task<ResultDto<OssFuncion>> Update(OssFuncion entity);
    Task<string> Delete(int id);
    Task<int> GetNextKey();

}