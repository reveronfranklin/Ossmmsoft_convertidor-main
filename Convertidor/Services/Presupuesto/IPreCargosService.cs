using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreCargosService
	{
        Task<ResultDto<List<PreCargosGetDto>>> GetAll();
        Task<ResultDto<PreCargosGetDto>> Update(PreCargosUpdateDto dto);
        Task<ResultDto<PreCargosGetDto>> Create(PreCargosUpdateDto dto);
        Task<PreCargosGetDto> MapPreCargo(PRE_CARGOS item);
        Task<ResultDto<PreCargosDeleteDto>> Delete(PreCargosDeleteDto dto);
        Task<ResultDto<List<PreCargosGetDto>>> GetAllByPresupuesto(FilterByPresupuestoDto filter);
    }
}

