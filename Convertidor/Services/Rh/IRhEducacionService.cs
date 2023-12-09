using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhEducacionService
	{
        Task<List<ListEducacionDto>> GetByCodigoPersona(int codigoPersona);
        
        Task<ResultDto<RhEducacionResponseDto>> Update(RhEducacionUpdate dto);
        Task<ResultDto<RhEducacionResponseDto>> Create(RhEducacionUpdate dto);
        Task<ResultDto<RhEducacionDeleteDto>> Delete(RhEducacionDeleteDto dto);

    }
}

