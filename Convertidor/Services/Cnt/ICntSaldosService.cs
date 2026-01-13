using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntSaldosService
    {
        Task<ResultDto<List<CntSaldosResponseDto>>> GetAll();
        Task<ResultDto<CntSaldosResponseDto>> GetByCodigo(int codigoSaldo);
        Task<ResultDto<CntSaldosResponseDto>> Create(CntSaldosUpdateDto dto);
        Task<ResultDto<CntSaldosResponseDto>> Update(CntSaldosUpdateDto dto);
        Task<ResultDto<CntSaldosDeleteDto>> Delete(CntSaldosDeleteDto dto);
    }
}
