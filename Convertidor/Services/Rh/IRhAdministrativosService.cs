namespace Convertidor.Services.Rh
{
	public interface IRhAdministrativosService
	{
        Task<List<RhAdministrativosResponseDto>> GetByCodigoPersona(int codigoPersona);
        Task<RH_ADMINISTRATIVOS> GetPrimerMovimientoByCodigoPersona(int codigoPersona);
        Task<ResultDto<RhAdministrativosResponseDto>> Update(RhAdministrativosUpdate dto);
        Task<ResultDto<RhAdministrativosResponseDto>> Create(RhAdministrativosUpdate dto);
        Task<ResultDto<RhAdministrativosDeleteDto>> Delete(RhAdministrativosDeleteDto dto);
        
	}
}

