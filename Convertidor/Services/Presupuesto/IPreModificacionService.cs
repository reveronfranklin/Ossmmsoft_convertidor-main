using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreModificacionService
    {
        Task<ResultDto<List<PreModificacionResponseDto>>> GetAll();
        Task<ResultDto<PreModificacionResponseDto>> Update(PreModificacionUpdateDto dto);
        Task<ResultDto<PreModificacionResponseDto>> Create(PreModificacionUpdateDto dto); 
        Task<ResultDto<PreModificacionDeleteDto>> Delete(PreModificacionDeleteDto dto);

        Task<PreModificacionResponseDto> GetByCodigo(int codigoModificacion);


    }
}

