using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhHistoricoMovimientoService
	{


        Task<List<ListHistoricoMovimientoDto>> GetByCodigoPersona(int codigoPersona);
        Task<List<ListHistoricoMovimientoDto>> GetByTipoNominaPeriodo(int tipoNomina, int codigoPeriodo);

        Task<List<ListHistoricoMovimientoDto>> GetByFechaNomina(DateTime desde, DateTime hasta);
        Task<List<ListHistoricoMovimientoDto>> GetByFechaNominaPersona(DateTime desde, DateTime hasta, int idPersona);
        Task<List<ListHistoricoMovimientoDto>> GetByProceso(FilterHistoricoNominaPeriodo filter);
        Task<List<ListHistoricoMovimientoDto>> GetByIndividual(FilterHistoricoNominaPeriodo filter);
        Task<List<ListHistoricoMovimientoDto>> GetByMasivo(FilterHistoricoNominaPeriodo filter);
        Task<RH_V_HISTORICO_MOVIMIENTOS> GetPrimerMovimientoByCodigoPersona(int codigoPersona);
        Task<List<RhResumenPagoPorPersona>> GetResumenPagoCodigoPersona(int codigoPersona);
	}
}

