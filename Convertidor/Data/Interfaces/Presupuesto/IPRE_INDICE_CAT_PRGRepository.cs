using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
    public interface IPRE_INDICE_CAT_PRGRepository
    {
        Task<IEnumerable<PRE_INDICE_CAT_PRG>> GetByLastDay(int days);
        Task<IEnumerable<PRE_INDICE_CAT_PRG>> GetAll();
        Task<List<PRE_INDICE_CAT_PRG>> GetAllByCodigoPresupuesto(int codigoPresupuesto);
        Task<ResultDto<PRE_INDICE_CAT_PRG>> Update(PRE_INDICE_CAT_PRG entity);
        Task<ResultDto<PRE_INDICE_CAT_PRG>> Create(PRE_INDICE_CAT_PRG entity);
        Task<PRE_INDICE_CAT_PRG> GetByCodigos(PreIndiceCategoriaProgramaticaUpdateDto entity);
        Task<PRE_INDICE_CAT_PRG> GetByCodigo(int codigoIcp);
    }
}
