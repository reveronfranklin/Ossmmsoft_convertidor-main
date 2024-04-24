using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreMetasService
    {
        Task<ResultDto<List<PreMetasResponseDto>>> GetAll();
        Task<ResultDto<PreMetasResponseDto>> Update(PreMetasUpdateDto dto);
        Task<ResultDto<PreMetasResponseDto>> Create(PreMetasUpdateDto dto); 
        Task<ResultDto<PreMetasDeleteDto>> Delete(PreMetasDeleteDto dto);
        
    }
}

