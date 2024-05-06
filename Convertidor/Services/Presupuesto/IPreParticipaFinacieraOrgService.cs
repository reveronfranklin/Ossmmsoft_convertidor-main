using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreParticipaFinacieraOrgService
    {
        Task<ResultDto<List<PreParticipaFinacieraOrgResponseDto>>> GetAll();
        Task<ResultDto<PreParticipaFinacieraOrgResponseDto>> Update(PreParticipaFinacieraOrgUpdateDto dto);
        Task<ResultDto<PreParticipaFinacieraOrgResponseDto>> Create(PreParticipaFinacieraOrgUpdateDto dto);
        Task<ResultDto<PreParticipaFinacieraOrgDeleteDto>> Delete(PreParticipaFinacieraOrgDeleteDto dto);

    }
}

