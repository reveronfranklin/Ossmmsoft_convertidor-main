using Convertidor.Dtos;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm;

public interface IAdmProveedoresDireccionesService
{
    Task<ResultDto<AdmProveedorDireccionResponseDto?>> Update(AdmProveedorDireccionUpdateDto dto);
    Task<ResultDto<AdmProveedorDireccionResponseDto>> Create(AdmProveedorDireccionUpdateDto dto);
    Task<ResultDto<AdmProveedorDireccionDeleteDto>> Delete(AdmProveedorDireccionDeleteDto dto);
    Task<ResultDto<AdmProveedorDireccionResponseDto>> GetByCodigo(AdmProveedorDireccionFilterDto dto);
    Task<ResultDto<List<AdmProveedorDireccionResponseDto>>> GetAll(AdmProveedorDireccionFilterDto dto);
    

}