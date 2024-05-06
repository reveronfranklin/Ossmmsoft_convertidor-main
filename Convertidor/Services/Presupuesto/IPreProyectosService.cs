using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public interface IPreProyectosService
    {
        Task<ResultDto<List<PreProyectosResponseDto>>> GetAll();
        Task<ResultDto<PreProyectosResponseDto>> Update(PreProyectosUpdateDto dto);
        Task<ResultDto<PreProyectosResponseDto>> Create(PreProyectosUpdateDto dto);
        Task<ResultDto<PreProyectosDeleteDto>> Delete(PreProyectosDeleteDto dto);
    }
}
