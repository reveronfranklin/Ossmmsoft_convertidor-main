using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPrePucCompromisosRepository
    {


        Task<PRE_PUC_COMPROMISOS> GetByCodigo(int codigoPucCompromiso);
        Task<List<PRE_PUC_COMPROMISOS>> GetAll();
        Task<ResultDto<PRE_PUC_COMPROMISOS>> Add(PRE_PUC_COMPROMISOS entity);
        Task<ResultDto<PRE_PUC_COMPROMISOS>> Update(PRE_PUC_COMPROMISOS entity);
        Task<string> Delete(int codigoPucCompromiso);
        Task<int> GetNextKey();
    }
}

