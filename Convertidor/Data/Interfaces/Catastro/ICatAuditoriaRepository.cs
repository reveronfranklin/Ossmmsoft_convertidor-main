using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatAuditoriaRepository
    {
        Task<List<CAT_AUDITORIA>> GetAll();
        Task<CAT_AUDITORIA> GetByCodigo(int codigoArrendamientoInmueble);
        Task<ResultDto<CAT_AUDITORIA>> Add(CAT_AUDITORIA entity);
        Task<ResultDto<CAT_AUDITORIA>> Update(CAT_AUDITORIA entity);
        Task<string> Delete(int codigoAuditoria);
        Task<int> GetNextKey();
    }
}
