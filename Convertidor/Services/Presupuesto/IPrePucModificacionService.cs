using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPrePucModificacionService
    {
        Task<ResultDto<List<PrePucModificacionResponseDto>>> GetAll();
        Task<ResultDto<PrePucModificacionResponseDto>> Update(PrePucModificacionUpdateDto dto);
        Task<ResultDto<PrePucModificacionResponseDto>> Create(PrePucModificacionUpdateDto dto); 
        Task<ResultDto<PrePucModificacionDeleteDto>> Delete(PrePucModificacionDeleteDto dto);
        
    }
}

