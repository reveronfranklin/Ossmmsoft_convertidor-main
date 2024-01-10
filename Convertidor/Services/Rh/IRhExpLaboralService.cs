using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhExpLaboralService
    {
        Task<List<RhExpLaboralResponseDto>> GetByCodigoPersona(int codigoPersona);
        Task<ResultDto<RhExpLaboralResponseDto>> Update(RhExpLaboralUpdateDto dto);
        Task<ResultDto<RhExpLaboralResponseDto>> Create(RhExpLaboralUpdateDto dto);
        Task<ResultDto<RhExpLaboralDeleteDto>> Delete(RhExpLaboralDeleteDto dto);

    }
}

