using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public interface IAdmPucReintegroService
    {
        Task<ResultDto<List<AdmPucReintegroResponseDto>>> GetAll();
        Task<ResultDto<AdmPucReintegroResponseDto>> Update(AdmPucReintegroUpdateDto dto);
        Task<ResultDto<AdmPucReintegroResponseDto>> Create(AdmPucReintegroUpdateDto dto);
        Task<ResultDto<AdmPucReintegroDeleteDto>> Delete(AdmPucReintegroDeleteDto dto);
    }
}
