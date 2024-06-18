using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
    public interface IPreDescriptivaRepository
	{

        Task<PRE_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId);
        Task<List<PRE_DESCRIPTIVAS>> GetByTitulo(int tituloId);
        Task<List<PRE_DESCRIPTIVAS>> GetAll();
        Task<PRE_DESCRIPTIVAS> GetByCodigo(int descripcionId);
        Task<ResultDto<PRE_DESCRIPTIVAS>> Add(PRE_DESCRIPTIVAS entity);
        Task<ResultDto<PRE_DESCRIPTIVAS>> Update(PRE_DESCRIPTIVAS entity);
        Task<string> Delete(int descripcionId);
        Task<int> GetNextKey();
        Task<PRE_DESCRIPTIVAS> GetByCodigoDescriptivaTexto(string codigo);
        Task<List<PRE_DESCRIPTIVAS>> GetByFKID(int descripcionIdFk);
        Task<bool> GetByIdAndTitulo(int tituloId, int id);
	}
}

