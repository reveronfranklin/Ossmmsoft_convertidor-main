using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreCompromisosService
    {
        Task<ResultDto<List<PreCompromisosResponseDto>>> GetAll();
        Task<ResultDto<List<PreCompromisosResponseDto>>> GetByPresupuesto(PreCompromisosFilterDto filter);
        Task<ResultDto<PreCompromisosResponseDto>> Update(PreCompromisosUpdateDto dto);
        Task<ResultDto<PreCompromisosResponseDto>> Create(PreCompromisosUpdateDto dto); 
        Task<ResultDto<PreCompromisosDeleteDto>> Delete(PreCompromisosDeleteDto dto);
        Task<PreCompromisosResponseDto> GetByNumeroYFecha(string numeroCompromiso, DateTime fechaCompromiso);

        Task<ResultDto<bool>> CrearCompromisoDesdeSolicitud(int codigoSolicitud);
        
    }
}

