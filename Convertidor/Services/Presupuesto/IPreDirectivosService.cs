using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public interface IPreDirectivosService
    {
        Task<ResultDto<List<PreDirectivosResponseDto>>> GetAll();
        Task<ResultDto<PreDirectivosResponseDto>> Update(PreDirectivosUpdateDto dto);
        Task<ResultDto<PreDirectivosResponseDto>> Create(PreDirectivosUpdateDto dto);
        Task<ResultDto<PreDirectivosDeleteDto>> Delete(PreDirectivosDeleteDto dto);
    }
}
