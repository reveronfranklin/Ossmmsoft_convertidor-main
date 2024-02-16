namespace Convertidor.Services.Rh
{
	public interface IRhConceptosService
	{

        Task<List<RhConceptosResponseDto>> GetAll();
        Task<RhConceptosResponseDto> GetByCodigo(RhConceptosFilterDto filter);
        Task<List<RhConceptosResponseDto>> GetConceptosByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta);
        Task<ResultDto<RhConceptosResponseDto>> Create(RhConceptosUpdateDto dto);
        Task<ResultDto<RhConceptosResponseDto>> Update(RhConceptosUpdateDto dto);
        Task<RhConceptosResponseDto> GetByTipoNominaConcepto(int codigoTipoNomina, int codigoConcepto);
        Task<List<RhConceptosResponseDto>> GetByTipoNomina(int codigoTipoNomina);
        //Task<ResultDto<RhConceptosDeleteDto>> Delete(RhConceptosDeleteDto dto);

	}
}

