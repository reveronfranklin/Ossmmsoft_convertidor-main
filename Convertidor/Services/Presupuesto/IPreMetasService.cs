using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreMetasService
    {
        Task<ResultDto<List<PreMetasResponseDto>>> GetAllByPresupuesto(FilterByPresupuestoProyectoDto filter);
        Task<ResultDto<PreMetasResponseDto>> Update(PreMetasUpdateDto dto);
        Task<ResultDto<PreMetasResponseDto>> Create(PreMetasUpdateDto dto); 
        Task<ResultDto<PreMetasDeleteDto>> Delete(PreMetasDeleteDto dto);
        
    }
}

