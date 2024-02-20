namespace Convertidor.Services.Rh;

public interface IRhFamiliaresService
{
    Task<ResultDto<List<RhFamiliarResponseDto>>> GetByCodigoPersona(int codigoPersona);
    Task<ResultDto<RhFamiliarResponseDto>> Update(RhFamiliarUpdateDto dto);
    Task<ResultDto<RhFamiliarResponseDto>> Create(RhFamiliarUpdateDto dto);
    Task<ResultDto<RhFamiliarDeleteDto>> Delete(RhFamiliarDeleteDto dto);
    
}