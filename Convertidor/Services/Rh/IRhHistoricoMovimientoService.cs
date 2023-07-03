using System;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhHistoricoMovimientoService
	{


        Task<List<ListHistoricoMovimientoDto>> GetByCodigoPersona(int codigoPersona);
        Task<List<ListHistoricoMovimientoDto>> GetByTipoNominaPeriodo(int tipoNomina, int codigoPeriodo);

        Task<List<ListHistoricoMovimientoDto>> GetByFechaNomina(DateTime desde, DateTime hasta);
    }
}

