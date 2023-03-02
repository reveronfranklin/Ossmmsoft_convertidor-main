using Convertidor.Data.EntitiesDestino;

namespace Convertidor.Data.Interfaces
{
    public interface IHistoricoPersonalCargoRepository
    {

        Task<bool> Add(HistoricoPersonalCargo entity);
        Task<bool> Add(List<HistoricoPersonalCargo> entities);
        Task<List<HistoricoPersonalCargo>> GetByLastDay(int days);
        Task<HistoricoPersonalCargo?> GetByKey(long codigoEmpresa, long codigoPersona, long codigoTipoNomina, long codigoPeriodo);
        Task Delete(int codigoEmpresa, int codigoPersona, int codigoTipoNomina, int codigoPeriodo);
        Task DeletePorDias(int days);

    }
}
