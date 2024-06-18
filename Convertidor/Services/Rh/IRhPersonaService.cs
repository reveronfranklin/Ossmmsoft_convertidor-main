namespace Convertidor.Services.Rh
{
	public interface IRhPersonaService
	{

     
        Task<ListPersonasDto> GetByCodigoPersona(int codigoPersona);
        Task<ResultDto<List<ListSimplePersonaDto>>> GetAllSimple();
        Task<ResultDto<List<ListSimplePersonaDto>>> GetAll();
        Task<PersonasDto> GetPersona(int codigoPersona);
        Task<RH_RELACION_CARGOS> CargoActual(int codigoPersona);
        Task<ResultDto<PersonasDto>> Update(RhPersonaUpdateDto dto);
        Task<ResultDto<PersonasDto>> Create(RhPersonaUpdateDto dto);
        Task<ResultDto<RhPersonaDeleteDto>> Delete(RhPersonaDeleteDto dto);
        List<string> GetListNacionalidad();
        Task<ResultDto<PersonasDto>> AddImage(int codigoPersona, List<IFormFile> files);
	}
}

