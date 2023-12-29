using Convertidor.Dtos;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm;

public interface IAdmProveedoresService
{
    Task<ResultDto<AdmProveedorResponseDto?>> Update(AdmProveedorUpdateDto dto);
    Task<ResultDto<AdmProveedorResponseDto>> Create(AdmProveedorUpdateDto dto);
    Task<ResultDto<AdmProveedorDeleteDto>> Delete(AdmProveedorDeleteDto dto);
    Task<ResultDto<AdmProveedorResponseDto>> GetByCodigo(AdmProveedorFilterDto dto);
    Task<ResultDto<List<AdmProveedorResponseDto>>> GetAll();
}