namespace Convertidor.Services.Rh
{
	public interface IRhEducacionService
	{

		Task<List<RhEducacionResponseDto>> GetByCodigoPersona(int codigoPersona);
        Task<ResultDto<RhEducacionResponseDto>> Update(RhEducacionUpdate dto);
        Task<ResultDto<RhEducacionResponseDto>> Create(RhEducacionUpdate dto);
        Task<ResultDto<RhEducacionDeleteDto>> Delete(RhEducacionDeleteDto dto);

    }
}

