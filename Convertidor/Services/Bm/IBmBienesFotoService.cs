using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm;

public interface IBmBienesFotoService
{
    Task<ResultDto<List<BmBienesFotoResponseDto>>> GetByNumeroPlaca(string numeroPlaca);
    Task<ResultDto<BmBienesFotoResponseDto>> Update(BmBienesFotoUpdateDto dto);
    Task<ResultDto<BmBienesFotoResponseDto>> Create(BmBienesFotoUpdateDto dto);
    Task<ResultDto<BmBienesFotoDeleteDto>> Delete(BmBienesFotoDeleteDto dto);
    Task<string> CopiarArchivos();
    Task<ResultDto<List<BmBienesFotoResponseDto>>> AddImage(int codigoBien, List<IFormFile> files);
    Task<ResultDto<List<BmBienesFotoResponseDto>>> AddImageModel(BmBienesimageUpdateDto dto);
    Task CreateBardCode();
    Task CreateBardCodeMultiple();
}