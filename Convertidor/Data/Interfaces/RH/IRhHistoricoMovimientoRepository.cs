using System;
using Convertidor.Data.Entities.Rh;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhHistoricoMovimientoRepository
	{
        Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetAll();
        Task<List<RH_V_HISTORICO_MOVIMIENTOS>> GetByCodigoPersona(int codigoPersona);


    }
}

