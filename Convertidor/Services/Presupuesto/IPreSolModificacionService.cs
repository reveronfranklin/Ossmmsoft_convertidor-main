using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreSolModificacionService
    {
        Task<ResultDto<List<PreSolModificacionResponseDto>>> GetAll();
        Task<ResultDto<PreSolModificacionResponseDto>> Update(PreSolModificacionUpdateDto dto);
        Task<ResultDto<PreSolModificacionResponseDto>> Create(PreSolModificacionUpdateDto dto); 
        Task<ResultDto<PreSolModificacionDeleteDto>> Delete(PreSolModificacionDeleteDto dto);
        Task<ResultDto<List<PreSolModificacionResponseDto>>> GetByPresupuesto(FilterPresupuestoDto filter);
        Task<bool> SolicitudPuedeModificarseoEliminarse(int codigoSolicitudModificacion);
        Task<ResultDto<PreSolModificacionResponseDto>> Aprobar(PreSolModificacionDeleteDto dto);
    }
}

