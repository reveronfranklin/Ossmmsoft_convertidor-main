using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis;

public interface ISisDescriptivaService
{
    Task<ResultDto<List<SisDescriptivasGetDto>>> GetAllByTitulo(SisDescriptivaFilterDto filter);
}