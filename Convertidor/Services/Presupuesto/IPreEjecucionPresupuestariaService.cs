using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public interface IPreEjecucionPresupuestariaService
    {
        Task<ResultDto<List<PreEjecucionPresupuestariaResponseDto>>> GetAll();
        Task<ResultDto<PreEjecucionPresupuestariaResponseDto>> Update(PreEjecucionPresupuestariaUpdateDto dto);
        Task<ResultDto<PreEjecucionPresupuestariaResponseDto>> Create(PreEjecucionPresupuestariaUpdateDto dto);
        Task<ResultDto<PreEjecucionPresupuestariaDeleteDto>> Delete(PreEjecucionPresupuestariaDeleteDto dto);
    }
}
