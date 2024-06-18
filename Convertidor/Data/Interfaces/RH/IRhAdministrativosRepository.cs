namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhAdministrativosRepository
	{
        
        Task<List<RH_ADMINISTRATIVOS>> GetByCodigoPersona(int codigoPersona);
        Task<RH_ADMINISTRATIVOS> GetPrimerMovimientoCodigoPersona(int codigoPersona);

        Task<RH_ADMINISTRATIVOS> GetByCodigo(int codigoAdministrativo);
        Task<ResultDto<RH_ADMINISTRATIVOS>> Add(RH_ADMINISTRATIVOS entity);
        Task<ResultDto<RH_ADMINISTRATIVOS>> Update(RH_ADMINISTRATIVOS entity);
        Task<string> Delete(int codigoAdministrativo);
        Task<int> GetNextKey();
	}
}

