using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreDetalleCompromisosService
    {
        Task<ResultDto<List<PreDetalleCompromisosResponseDto>>> GetAll();
        Task<List<PreDetalleCompromisosResponseDto>> GetByCodigoCompromiso(int codigoCompromiso);
        Task<ResultDto<PreDetalleCompromisosResponseDto>> Update(PreDetalleCompromisosUpdateDto dto);
        Task<ResultDto<PreDetalleCompromisosResponseDto>> Create(PreDetalleCompromisosUpdateDto dto); 
        Task<ResultDto<PreDetalleCompromisosDeleteDto>> Delete(PreDetalleCompromisosDeleteDto dto);
        
    }
}

