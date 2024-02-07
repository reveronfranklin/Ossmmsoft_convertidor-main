using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhProcesosService
	{
        Task<RH_PROCESOS> GetByCodigo(int codigoProcesso);
        Task<ResultDto<List<RhProcesosDto>>> GetAll();
        Task<ResultDto<List<RhProcesosResponseDtoDto>>> GetAllRhProcesoResponseDto();
        Task<ResultDto<RhProcesosResponseDtoDto>> Update(RhProcesosUpdateDtoDto dto);
        Task<ResultDto<RhProcesosResponseDtoDto>> Create(RhProcesosUpdateDtoDto dto);
        Task<ResultDto<RhProcesosDeleteDtoDto>> Delete(RhProcesosDeleteDtoDto dto);
        
	}
}

