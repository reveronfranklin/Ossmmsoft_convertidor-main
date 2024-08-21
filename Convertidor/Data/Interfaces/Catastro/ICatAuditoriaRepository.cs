using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatAuditoriaRepository
    {
        Task<List<CAT_AUDITORIA>> GetAll();
        Task<ResultDto<CAT_AUDITORIA>> Add(CAT_AUDITORIA entity);
        Task<int> GetNextKey();
    }
}
