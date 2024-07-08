using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public interface ICntAuxiliaresService
    {
        Task<ResultDto<List<CntAuxiliaresResponseDto>>> GetAll();
        Task<ResultDto<CntAuxiliaresResponseDto>> GetByCodigo(int codigoAuxiliar);
        Task<ResultDto<List<CntAuxiliaresResponseDto>>> GetAllByCodigoMayor(int codigoMayor);
        Task<ResultDto<CntAuxiliaresResponseDto>> Create(CntAuxiliaresUpdateDto dto);
        Task<ResultDto<CntAuxiliaresResponseDto>> Update(CntAuxiliaresUpdateDto dto);
        Task<ResultDto<CntAuxiliaresDeleteDto>> Delete(CntAuxiliaresDeleteDto dto);
    }
}
