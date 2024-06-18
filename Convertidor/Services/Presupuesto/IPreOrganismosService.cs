using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreOrganismosService
    {
        Task<ResultDto<List<PreOrganismosResponseDto>>> GetAll();
        Task<ResultDto<PreOrganismosResponseDto>> Update(PreOrganismosUpdateDto dto);
        Task<ResultDto<PreOrganismosResponseDto>> Create(PreOrganismosUpdateDto dto);
        Task<ResultDto<PreOrganismosDeleteDto>> Delete(PreOrganismosDeleteDto dto);

    }
}

