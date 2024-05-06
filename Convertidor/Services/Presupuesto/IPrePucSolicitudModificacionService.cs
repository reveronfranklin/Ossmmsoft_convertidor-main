using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto;

public interface IPrePucSolicitudModificacionService
{
    Task<bool> PresupuestoExiste(int codPresupuesto);
    Task<bool> ICPExiste(int codigoICP);
    Task<bool> PUCExiste(int codigoPUC);

    Task<ResultDto<List<PrePucSolModificacionResponseDto>>> GetAllByCodigoSolicitud(
        PrePucSolModificacionFilterDto filter);

}