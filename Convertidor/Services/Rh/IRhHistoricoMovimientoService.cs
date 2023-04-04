using System;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhHistoricoMovimientoService
	{


        Task<List<ListHistoricoMovimientoDto>> GetByCodigoPersona(int codigoPersona);


    }
}

