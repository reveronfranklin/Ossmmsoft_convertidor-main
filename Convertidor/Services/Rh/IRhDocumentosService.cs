using System;
using Convertidor.Data.Entities.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhDocumentosService
	{
        Task<List<RhDocumentosResponseDto>> GetByCodigoPersona(int codigoPersona);
        
        Task<ResultDto<RhDocumentosResponseDto>> Update(RhDocumentosUpdate dto);
        Task<ResultDto<RhDocumentosResponseDto>> Create(RhDocumentosUpdate dto);
        Task<ResultDto<RhDocumentosDeleteDto>> Delete(RhDocumentosDeleteDto dto);

    }
}

