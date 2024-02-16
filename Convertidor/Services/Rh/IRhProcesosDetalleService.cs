namespace Convertidor.Services.Rh;

public interface IRhProcesosDetalleService
{
    Task<RH_PROCESOS_DETALLES> GetByCodigo(int codigoProcessoDetalle);
    Task<ResultDto<List<RhProcesosDetalleResponseDtoDto>>> GetAll();
    Task<ResultDto<RhProcesosDetalleResponseDtoDto>> Update(RhProcesosDetalleUpdateDtoDto dto);
    Task<ResultDto<RhProcesosDetalleResponseDtoDto>> Create(RhProcesosDetalleUpdateDtoDto dto);
    Task<ResultDto<RhProcesosDetalleDeleteDtoDto>> Delete(RhProcesosDetalleDeleteDtoDto dto);
    Task<ResultDto<List<RhProcesosDetalleResponseDtoDto>>> GetByProceso(RhProcesosDetalleFilterDtoDto filter);

    Task<ResultDto<List<RhProcesosDetalleResponseDtoDto>>> GetByCodigoProcesoTipoNomina(
        RhProcesosDetalleFilterDtoDto filter);
}