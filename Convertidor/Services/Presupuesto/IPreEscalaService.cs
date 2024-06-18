using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public interface IPreEscalaService
    {
        Task<ResultDto<List<PreEscalaResponseDto>>> GetAll();
        Task<ResultDto<PreEscalaResponseDto>> GetByCodigo(int codigoEscala);
        Task<ResultDto<PreEscalaResponseDto>> Update(PreEscalaUpdateDto dto);
        Task<ResultDto<PreEscalaResponseDto>> Create(PreEscalaUpdateDto dto);
        Task<ResultDto<PreEscalaDeleteDto>> Delete(PreEscalaDeleteDto dto);
    }
}
