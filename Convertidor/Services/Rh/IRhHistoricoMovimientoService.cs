using System;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhHistoricoMovimientoService
	{


        Task<List<ListHistoricoMovimientoDto>> GetByCodigoPersona(int codigoPersona);
        Task<List<ListHistoricoMovimientoDto>> GetByTipoNominaPeriodo(int tipoNomina, int codigoPeriodo);

        Task<List<ListHistoricoMovimientoDto>> GetByFechaNomina(DateTime desde, DateTime hasta);
        Task<List<ListHistoricoMovimientoDto>> GetByFechaNominaPersona(DateTime desde, DateTime hasta, int idPersona);
    }
}

