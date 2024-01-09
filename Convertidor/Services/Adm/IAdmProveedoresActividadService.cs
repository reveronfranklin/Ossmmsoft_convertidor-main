using Convertidor.Dtos;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm;

public interface IAdmProveedoresActividadService
{
    Task<ResultDto<AdmProveedorActividadResponseDto?>> Update(AdmProveedorActividadUpdateDto dto);
    Task<ResultDto<AdmProveedorActividadResponseDto>> Create(AdmProveedorActividadUpdateDto dto);
    Task<ResultDto<AdmProveedorActividadDeleteDto>> Delete(AdmProveedorActividadDeleteDto dto);
    Task<ResultDto<AdmProveedorActividadResponseDto>> GetByCodigo(AdmProveedorActividadFilterDto dto);
    Task<ResultDto<List<AdmProveedorActividadResponseDto>>> GetAll(AdmProveedorActividadFilterDto dto);
}