using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhHistoricoMovimientoRepository
	{
        Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetAll();
        Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetByCodigoPersona(int codigoPersona);
        Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetByTipoNominaPeriodo(int tipoNomina, int codigoPeriodo);
        Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetByFechaNomina(DateTime desde, DateTime hasta);
        Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetByFechaNominaPersona(DateTime desde, DateTime hasta, int idPersona);
        Task<RH_V_HISTORICO_MOVIMIENTOS> GetPrimerMovimientoByCodigoPersona(int codigoPersona);
    }
}

