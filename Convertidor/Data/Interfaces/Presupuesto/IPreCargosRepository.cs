using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreCargosRepository
	{


        Task<PRE_CARGOS> GetByCodigo(int codigoCargo);
        Task<List<PRE_CARGOS>> GetAll();
        Task<List<PRE_CARGOS>> GetAllByPresupuesto(int codigoPresupuesto);
        Task<ResultDto<PRE_CARGOS>> Add(PRE_CARGOS entity);
        Task<ResultDto<PRE_CARGOS>> Update(PRE_CARGOS entity);
        Task<string> Delete(int tituloId);
        Task<int> GetNextKey();
    }
}

