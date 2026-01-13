using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm;

public interface IAdmProductosService
{
    Task<ResultDto<List<AdmProductosResponse>>> GetAll();

    Task<ResultDto<List<AdmProductosResponse>>> GetAllPaginate(AdmProductosFilterDto filter);
    Task<ResultDto<bool>> Update(AdmProductosUpdateDto dto);
}