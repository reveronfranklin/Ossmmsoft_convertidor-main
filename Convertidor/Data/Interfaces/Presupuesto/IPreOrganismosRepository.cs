using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreOrganismosRepository
    {
        Task<PRE_ORGANISMOS> GetByCodigo(int codigoOrganismo);
        Task<List<PRE_ORGANISMOS>> GetAll();
        Task<ResultDto<PRE_ORGANISMOS>> Add(PRE_ORGANISMOS entity);
        Task<ResultDto<PRE_ORGANISMOS>> Update(PRE_ORGANISMOS entity);
        Task<string> Delete(int codigoOrganismo);
        Task<int> GetNextKey();
    }
}

