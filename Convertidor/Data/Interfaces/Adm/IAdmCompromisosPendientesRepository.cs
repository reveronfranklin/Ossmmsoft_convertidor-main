using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmCompromisosPendientesRepository
{
    Task<List<ADM_V_COMPROMISO_PENDIENTE>> GetCompromisosPendientesPorCodigoPresupuesto(int codigoPresupuesto);
}