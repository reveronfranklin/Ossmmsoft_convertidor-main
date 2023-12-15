using System;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhConceptosService
	{

        Task<List<ListConceptosDto>> GetAll();

        Task<List<ListConceptosDto>> GetConceptosByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta);
        Task<ResultDto<RhConceptosResponseDto>> Create(RhConceptosUpdateDto dto);
        Task<ResultDto<RhConceptosResponseDto>> Update(RhConceptosUpdateDto dto);
        //Task<ResultDto<RhConceptosDeleteDto>> Delete(RhConceptosDeleteDto dto);

    }
}

