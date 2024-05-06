using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreObjetivosService
    {
        Task<ResultDto<List<PreObjetivosResponseDto>>> GetAll();
        Task<ResultDto<PreObjetivosResponseDto>> Update(PreObjetivosUpdateDto dto);
        Task<ResultDto<PreObjetivosResponseDto>> Create(PreObjetivosUpdateDto dto);
        Task<ResultDto<PreObjetivosDeleteDto>> Delete(PreObjetivosDeleteDto dto);

    }
}

