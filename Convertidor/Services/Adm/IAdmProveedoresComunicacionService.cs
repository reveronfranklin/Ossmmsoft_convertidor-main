using Convertidor.Dtos;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm;

public interface IAdmProveedoresComunicacionService
{
    Task<ResultDto<AdmProveedorComunicacionResponseDto?>> Update(AdmProveedorComunicacionUpdateDto dto);
    Task<ResultDto<AdmProveedorComunicacionResponseDto>> Create(AdmProveedorComunicacionUpdateDto dto);
    Task<ResultDto<AdmProveedorComunicacionDeleteDto>> Delete(AdmProveedorComunicacionDeleteDto dto);
    Task<ResultDto<AdmProveedorComunicacionResponseDto>> GetByCodigo(AdmProveedorComunicacionFilterDto dto);
    Task<ResultDto<List<AdmProveedorComunicacionResponseDto>>> GetAll(AdmProveedorComunicacionFilterDto dto);

}