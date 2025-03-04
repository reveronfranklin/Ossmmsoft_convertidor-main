namespace Convertidor.Services.Rh
{
	public interface IRhDocumentosDetallesService
    {

        Task<List<RhDocumentosDetallesResponseDto>> GetByDocumento(int codigoDocumento);
        Task<ResultDto<RhDocumentosDetallesResponseDto>> Update(RhDocumentosDetallesUpdate dto);
        Task<ResultDto<RhDocumentosDetallesResponseDto>> Create(RhDocumentosDetallesUpdate dto);
        Task<ResultDto<RhDocumentosDetallesDeleteDto>> Delete(RhDocumentosDetallesDeleteDto dto);

    }
}

