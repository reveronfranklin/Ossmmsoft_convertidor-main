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
        Task<string> Delete(int codigoICP);
        Task<PRE_INDICE_CAT_PRG> GetHastaActividad(int ano, int codIcp, string sector, string programa, string subPrograma, string proyecto, string actividad);
        Task<PRE_INDICE_CAT_PRG> GetHastaProyecto(int ano, int codIcp, string sector, string programa, string subPrograma, string proyecto);
        Task<PRE_INDICE_CAT_PRG> GetHastaSubPrograma(int ano, int codIcp, string sector, string programa, string subPrograma);
        Task<PRE_INDICE_CAT_PRG> GetHastaPrograma(int ano, int codIcp, string sector, string programa);
        Task<PRE_INDICE_CAT_PRG> GetHastaSector(int ano, int codIcp, string sector);
        Task<ResultDto<List<PRE_INDICE_CAT_PRG>>> ClonarByCodigoPresupuesto(int codigoPresupuestoOrigen, int codigoPresupuestoDestino);

    }
}
