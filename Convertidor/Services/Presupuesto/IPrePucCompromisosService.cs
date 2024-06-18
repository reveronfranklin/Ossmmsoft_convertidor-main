using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPrePucCompromisosService
    {
        Task<ResultDto<List<PrePucCompromisosResponseDto>>> GetAll();
        Task<ResultDto<PrePucCompromisosResponseDto>> Update(PrePucCompromisosUpdateDto dto);
        Task<ResultDto<PrePucCompromisosResponseDto>> Create(PrePucCompromisosUpdateDto dto); 
        Task<ResultDto<PrePucCompromisosDeleteDto>> Delete(PrePucCompromisosDeleteDto dto);
        
    }
}

