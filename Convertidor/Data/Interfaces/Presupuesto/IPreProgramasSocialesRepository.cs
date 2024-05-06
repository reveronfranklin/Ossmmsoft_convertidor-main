using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
    public interface IPreProgramasSocialesRepository
    {
        Task<PRE_PROGRAMAS_SOCIALES> GetByCodigo(int codigoPrgSocial);
        Task<List<PRE_PROGRAMAS_SOCIALES>> GetAll();
        Task<ResultDto<PRE_PROGRAMAS_SOCIALES>> Add(PRE_PROGRAMAS_SOCIALES entity);
        Task<ResultDto<PRE_PROGRAMAS_SOCIALES>> Update(PRE_PROGRAMAS_SOCIALES entity);
        Task<string> Delete(int codigoPrgSocial);
        Task<int> GetNextKey();
    }
}
