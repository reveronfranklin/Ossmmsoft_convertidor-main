using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntDescriptivaRepository
    {
        Task<CNT_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId);
        Task<CNT_DESCRIPTIVAS> GetByCodigoDescriptivaTexto(string codigo);
        Task<List<CNT_DESCRIPTIVAS>> GetByTitulo(int tituloId);
        Task<bool> GetByIdAndTitulo(int tituloId, int id);
        Task<List<CNT_DESCRIPTIVAS>> GetAll();
        Task<CNT_DESCRIPTIVAS> GetByCodigo(int descripcionId);
        Task<List<CNT_DESCRIPTIVAS>> GetByFKID(int descripcionIdFk);
        Task<ResultDto<CNT_DESCRIPTIVAS>> Add(CNT_DESCRIPTIVAS entity);
        Task<ResultDto<CNT_DESCRIPTIVAS>> Update(CNT_DESCRIPTIVAS entity);
        Task<string> Delete(int descripcionId);
        Task<int> GetNextKey();
    }
}
