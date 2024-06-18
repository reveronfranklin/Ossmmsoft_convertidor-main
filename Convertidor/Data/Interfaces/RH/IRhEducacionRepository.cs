namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhEducacionRepository
	{
        Task<List<RH_EDUCACION>> GetAll();
        Task<List<RH_EDUCACION>> GetByCodigoPersona(int codigoPersona);
        Task<RH_EDUCACION> GetByCodigo(int codigoEducacion);

        Task<ResultDto<RH_EDUCACION>> Add(RH_EDUCACION entity);
        Task<ResultDto<RH_EDUCACION>> Update(RH_EDUCACION entity);
        Task<string> Delete(int codigoEducacion);
        Task<int> GetNextKey();

    }
}

