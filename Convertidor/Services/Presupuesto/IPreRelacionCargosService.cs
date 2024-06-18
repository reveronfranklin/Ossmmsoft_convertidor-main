using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreRelacionCargosService
	{

        Task<ResultDto<List<PreRelacionCargoGetDto>>> GetAll();
        Task<ResultDto<List<PreRelacionCargoGetDto>>> GetAllByPresupuesto(FilterByPresupuestoDto filter);
        Task<ResultDto<PreRelacionCargoGetDto>> Update(PreRelacionCargoUpdateDto dto);
        Task<ResultDto<PreRelacionCargoGetDto>> Create(PreRelacionCargoUpdateDto dto);
        Task<PreRelacionCargoGetDto> MapPreRelacionCargo(PRE_RELACION_CARGOS item);
        Task<ResultDto<PreRelacionCargoDeleteDto>> Delete(PreRelacionCargoDeleteDto dto);
        Task<ResultDto<PreRelacionCargoGetDto>> UpdateField(UpdateFieldDto dto);
    }
}

