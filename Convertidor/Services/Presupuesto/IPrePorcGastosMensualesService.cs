using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPrePorcGastosMensualesService
    {
        Task<ResultDto<List<PrePorcGastosMensualesResponseDto>>> GetAll();
        Task<ResultDto<PrePorcGastosMensualesResponseDto>> Update(PrePorcGastosMensualesUpdateDto dto);
        Task<ResultDto<PrePorcGastosMensualesResponseDto>> Create(PrePorcGastosMensualesUpdateDto dto);
        Task<ResultDto<PrePorcGastosMensualesDeleteDto>> Delete(PrePorcGastosMensualesDeleteDto dto);

    }
}

