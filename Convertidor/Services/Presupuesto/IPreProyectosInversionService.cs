using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public interface IPreProyectosInversionService
    {
        Task<ResultDto<List<PreProyectosInversionResponseDto>>> GetAll();
        Task<ResultDto<PreProyectosInversionResponseDto>> Update(PreProyectosInversionUpdateDto dto);
        Task<ResultDto<PreProyectosInversionResponseDto>> Create(PreProyectosInversionUpdateDto dto);
        Task<ResultDto<PreProyectosInversionDeleteDto>> Delete(PreProyectosInversionDeleteDto dto);
    }
}
