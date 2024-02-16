using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm;

public interface IAdmProveedoresContactoService
{
    Task<ResultDto<AdmProveedorContactoResponseDto?>> Update(AdmProveedorContactoUpdateDto dto);
    Task<ResultDto<AdmProveedorContactoResponseDto>> Create(AdmProveedorContactoUpdateDto dto);
    Task<ResultDto<AdmProveedorContactoDeleteDto>> Delete(AdmProveedorContactoDeleteDto dto);
    Task<ResultDto<AdmProveedorContactoResponseDto>> GetByCodigo(AdmProveedorContactoFilterDto dto);
    Task<ResultDto<List<AdmProveedorContactoResponseDto>>> GetAll(AdmProveedorContactoFilterDto dto);
    

}