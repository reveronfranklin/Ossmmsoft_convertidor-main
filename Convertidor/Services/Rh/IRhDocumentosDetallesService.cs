using Convertidor.Dtos;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public interface IRhDocumentosDetallesService
    {
        
        Task<ResultDto<RhDocumentosDetallesResponseDto>> Update(RhDocumentosDetallesUpdate dto);
        Task<ResultDto<RhDocumentosDetallesResponseDto>> Create(RhDocumentosDetallesUpdate dto);
        Task<ResultDto<RhDocumentosDetallesDeleteDto>> Delete(RhDocumentosDetallesDeleteDto dto);

    }
}

