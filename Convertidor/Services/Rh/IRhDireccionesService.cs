using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhDireccionesService
	{

        Task<List<RhDireccionesResponseDto>> GetByCodigoPersona(int codigoPersona);
        Task<ResultDto<RhDireccionesResponseDto>> Update(RhDireccionesUpdate dto);
        Task<ResultDto<RhDireccionesResponseDto>> Create(RhDireccionesUpdate dto);
        Task<ResultDto<RhDireccionesDeleteDto>> Delete(RhDireccionesDeleteDto dto);

    }
}

