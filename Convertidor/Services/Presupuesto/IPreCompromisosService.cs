using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreCompromisosService
    {
        Task<ResultDto<List<PreCompromisosResponseDto>>> GetAll();
        Task<ResultDto<PreCompromisosResponseDto>> Update(PreCompromisosUpdateDto dto);
        Task<ResultDto<PreCompromisosResponseDto>> Create(PreCompromisosUpdateDto dto); 
        Task<ResultDto<PreCompromisosDeleteDto>> Delete(PreCompromisosDeleteDto dto);
        
    }
}

