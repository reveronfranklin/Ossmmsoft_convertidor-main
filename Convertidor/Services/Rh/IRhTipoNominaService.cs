using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhTipoNominaService
	{
        Task<List<ListTipoNominaDto>> GetAll();

        Task<List<ListTipoNominaDto>> GetTipoNominaByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta);
        Task<ResultDto<RhTiposNominaResponseDto>> Create(RhTiposNominaUpdateDto dto);
        Task<ResultDto<RhTiposNominaResponseDto>> Update(RhTiposNominaUpdateDto dto);
        Task<ResultDto<RhTiposNominaDeleteDto>> Delete(RhTiposNominaDeleteDto dto);
    }
}

