using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public interface IPreProgramasSocialesService
    {
        Task<ResultDto<List<PreProgramasSocialesResponseDto>>> GetAll();
        Task<ResultDto<PreProgramasSocialesResponseDto>> Update(PreProgramasSocialesUpdateDto dto);
        Task<ResultDto<PreProgramasSocialesResponseDto>> Create(PreProgramasSocialesUpdateDto dto);
        Task<ResultDto<PreProgramasSocialesDeleteDto>> Delete(PreProgramasSocialesDeleteDto dto);
    }
}
