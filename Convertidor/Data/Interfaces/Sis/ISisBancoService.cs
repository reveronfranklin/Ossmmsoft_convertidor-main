using Convertidor.Dtos.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisBancoService
{
    Task<ResultDto<SisBancoResponseDto>> Update(SisBancoUpdateDto dto);
    Task<ResultDto<SisBancoResponseDto>> Create(SisBancoUpdateDto dto);
    Task<ResultDto<SisBancoDeleteDto>> Delete(SisBancoDeleteDto dto);
    Task<ResultDto<List<SisBancoResponseDto>>> GetAll(SisBancoFilterDto filter);
    

}