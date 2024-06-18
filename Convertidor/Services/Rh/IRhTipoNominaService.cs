namespace Convertidor.Services.Rh
{
	public interface IRhTipoNominaService
	{
        Task<List<RhTiposNominaResponseDto>> GetAll();
        Task<RhTiposNominaResponseDto> GetByCodigo(RhTiposNominaFilterDto filter);
        Task<List<RhTiposNominaResponseDto>> GetTipoNominaByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta);
        Task<ResultDto<RhTiposNominaResponseDto>> Create(RhTiposNominaUpdateDto dto);
        Task<ResultDto<RhTiposNominaResponseDto>> Update(RhTiposNominaUpdateDto dto);
        Task<ResultDto<RhTiposNominaDeleteDto>> Delete(RhTiposNominaDeleteDto dto);
    }
}

