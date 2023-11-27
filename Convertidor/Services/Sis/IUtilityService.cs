using Convertidor.Dtos;

namespace Convertidor.Services.Sis;

public interface IUtilityService
{
    FechaDto GetFechaDto(DateTime fecha);
    List<string> GetListNacionalidad();
    List<string> GetListSexo();
    List<string> GetListStatus();
    List<string> GetListManoHabil();
}