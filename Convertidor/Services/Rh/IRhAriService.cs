namespace Convertidor.Services.Rh
{
	public interface IRhAriService
	{
        Task<List<RhAriResponseDto>> GetByCodigoPersona(int codigoPersona);
        Task<ResultDto<RhAriResponseDto>> Update(RhAriUpdateDto dto);
        Task<ResultDto<RhAriResponseDto>> Create(RhAriUpdateDto dto);
        Task<ResultDto<RhAriDeleteDto>> Delete(RhAriDeleteDto dto);
        
	}
}

