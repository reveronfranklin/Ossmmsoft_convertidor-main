namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhRelacionCargosRepository
	{
        Task<List<RH_RELACION_CARGOS>> GetAll();
        Task<List<RH_RELACION_CARGOS>> GetAllByPreCodigoRelacionCargos(int preCodigoRelaciconCargo);
        Task<RH_RELACION_CARGOS> GetByCodigo(int codigoRelacionCargo);
        Task<ResultDto<RH_RELACION_CARGOS>> Add(RH_RELACION_CARGOS entity);
        Task<ResultDto<RH_RELACION_CARGOS>> Update(RH_RELACION_CARGOS entity);
        Task<string> Delete(int codigoRelacionCargo);
        Task<int> GetNextKey();
        Task<RH_RELACION_CARGOS> GetUltimoCargoPorPersona(int codigoPersona);


	}
}

