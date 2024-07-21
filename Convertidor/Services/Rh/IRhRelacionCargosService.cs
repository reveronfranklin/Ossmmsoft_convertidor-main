using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Rh
{
	public interface IRhRelacionCargosService
	{

        Task<ResultDto<List<RhRelacionCargoDto>>> GetAll();
        Task<ResultDto<List<RhRelacionCargoDto>>> GetAllByPreRelacionCargo(int codigoPreRelacionCargo);
        Task<ResultDto<RhRelacionCargoDto>> Update(RhRelacionCargoUpdateDto dto);
        Task<ResultDto<RhRelacionCargoDto>> UpdateField(UpdateFieldDto dto);
        Task<ResultDto<RhRelacionCargoDto>> Create(RhRelacionCargoUpdateDto dto);
        Task<ResultDto<RhRelacionCargoDeleteDto>> Delete(RhRelacionCargoDeleteDto dto);

        Task<bool> RhRelacionCargoConRelacionDePersona(int codigoPreRelacionCargo);
	}
}

