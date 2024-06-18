namespace Convertidor.Services.Rh;

public interface IRhComunicacionService
{
    Task<ResultDto<List<RhComunicacionResponseDto>>> GetByCodigoPersona(int codigoPersona);
    Task<ResultDto<RhComunicacionResponseDto>> Update(RhComunicacionUpdate dto);
    Task<ResultDto<RhComunicacionResponseDto>> Create(RhComunicacionUpdate dto);
    Task<ResultDto<RhComunicacionDeleteDto>> Delete(RhComunicacionDeleteDto dto);
}