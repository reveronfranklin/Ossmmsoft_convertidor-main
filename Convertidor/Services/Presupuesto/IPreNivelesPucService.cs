using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreNivelesPucService
    {
        Task<ResultDto<List<PreNivelesPucResponseDto>>> GetAll();
        Task<ResultDto<PreNivelesPucResponseDto>> Update(PreNivelesPucUpdateDto dto);
        Task<ResultDto<PreNivelesPucResponseDto>> Create(PreNivelesPucUpdateDto dto);
        Task<ResultDto<PreNivelesPucDeleteDto>> Delete(PreNivelesPucDeleteDto dto);

    }
}

