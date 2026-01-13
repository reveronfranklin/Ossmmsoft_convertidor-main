using Convertidor.Dtos.Sis;

namespace Convertidor.Data.Interfaces.Sis;

public interface ISisCuentaBancoService
{
    Task<ResultDto<SisCuentaBancoResponseDto>> Update(SisCuentaBancoUpdateDto dto);
    Task<ResultDto<SisCuentaBancoResponseDto>> Create(SisCuentaBancoUpdateDto dto);
    Task<ResultDto<SisCuentaBancoDeleteDto>> Delete(SisCuentaBancoDeleteDto dto);
    Task<ResultDto<List<SisCuentaBancoResponseDto>>> GetAll(SisCuentaBancoFilterDto filter);
    

}