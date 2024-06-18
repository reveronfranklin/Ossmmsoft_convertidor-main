using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public interface IPreEquiposService
    {
        Task<ResultDto<List<PreEquiposResponseDto>>> GetAll();
        Task<ResultDto<PreEquiposResponseDto>> GetByCodigo(int codigoEquipo);
        Task<ResultDto<PreEquiposResponseDto>> Update(PreEquiposUpdateDto dto);
        Task<ResultDto<PreEquiposResponseDto>> Create(PreEquiposUpdateDto dto);
        Task<ResultDto<PreEquiposDeleteDto>> Delete(PreEquiposDeleteDto dto);
    }
}
