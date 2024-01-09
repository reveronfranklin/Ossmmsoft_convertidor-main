using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhDireccionesRepository
	{

        Task<List<RH_DIRECCIONES>> GetByCodigoPersona(int codigoPersona);
        Task<RH_DIRECCIONES> GetByCodigo(int codigoDireccion);
        Task<ResultDto<RH_DIRECCIONES>> Add(RH_DIRECCIONES entity);
        Task<ResultDto<RH_DIRECCIONES>> Update(RH_DIRECCIONES entity);
        Task<string> Delete(int codigoDireccion);
        Task<int> GetNextKey();

    }
}

