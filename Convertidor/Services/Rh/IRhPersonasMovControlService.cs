using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhPersonasMovControlService
    {



        Task<List<RhPersonasMovControlResponseDto>> GetCodigoPersona(int codigoPersona);
        Task<ResultDto<RhPersonasMovControlResponseDto>> Create(RhPersonasMovControlUpdateDto dto);
        Task<ResultDto<RhPersonasMovControlResponseDto>> Update(RhPersonasMovControlUpdateDto dto);
        Task<ResultDto<RhPersonasMovControlDeleteDto>> Delete(RhPersonasMovControlDeleteDto dto);

    }
}

