using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhComunicacionesRepository
	{
        Task<List<RH_COMUNICACIONES>> GetByCodigoPersona(int codigoPersona);
        Task<RH_COMUNICACIONES> GetByCodigo(int codigoComunicacion);
        Task<ResultDto<RH_COMUNICACIONES>> Add(RH_COMUNICACIONES entity);
        Task<ResultDto<RH_COMUNICACIONES>> Update(RH_COMUNICACIONES entity);
        Task<string> Delete(int codigoComunicacion);
        Task<int> GetNextKey();
	}
}

