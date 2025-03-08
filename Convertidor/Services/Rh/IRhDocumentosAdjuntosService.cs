using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Rh;

public interface IRhDocumentosAdjuntosService
{
    Task<ResultDto<List<RhDocumentosAdjuntosResponseDto>>> GetByNumeroDocumento(int numeroDocumento);
    Task<ResultDto<RhDocumentosAdjuntosResponseDto>> Update(RhDocumentosAdjuntosUpdateDto dto);
    Task<ResultDto<RhDocumentosAdjuntosResponseDto>> Create(RhDocumentosAdjuntosUpdateDto dto);
    Task<ResultDto<RhDocumentosAdjuntosDeleteDto>> Delete(RhDocumentosAdjuntosDeleteDto dto);
    Task<ResultDto<string>> AddOneImage(int codigoDocumento, IFormFile files);
    Task<ResultDto<List<RhDocumentosAdjuntosResponseDto>>> AddImage(int codigoDocumento, List<IFormFile> files);
    void MinFile(string nroDocumento, string foto);
    Task<ResultDto<List<RhDocumentosAdjuntosResponseDto>>> AddImageModel(RhDocumentosFilesUpdateDto dto);
    
}