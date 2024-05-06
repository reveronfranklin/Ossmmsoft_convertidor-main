using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPrePorcGastosMensualesRepository
    {
        Task<PRE_PORC_GASTOS_MENSUALES> GetByCodigo(int codigoPorGastosMes);
        Task<List<PRE_PORC_GASTOS_MENSUALES>> GetAll();
        Task<ResultDto<PRE_PORC_GASTOS_MENSUALES>> Add(PRE_PORC_GASTOS_MENSUALES entity);
        Task<ResultDto<PRE_PORC_GASTOS_MENSUALES>> Update(PRE_PORC_GASTOS_MENSUALES entity);
        Task<string> Delete(int codigoPorGastosMes);
        Task<int> GetNextKey();
    }
}

