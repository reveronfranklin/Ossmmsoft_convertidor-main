using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;

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

