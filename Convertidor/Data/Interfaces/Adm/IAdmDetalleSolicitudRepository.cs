﻿using Convertidor.Data.Entities.ADM;
using Convertidor.Dtos.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmDetalleSolicitudRepository
    {
        Task<ADM_DETALLE_SOLICITUD> GetCodigoDetalleSolicitud(int codigoDetalleSolicitud);
        Task<List<ADM_DETALLE_SOLICITUD>> GetAll();
        
        Task<ResultDto<List<AdmDetalleSolicitudResponseDto>>> GetByCodigoSolicitud(AdmSolicitudesFilterDto filter);
        Task<ADM_DETALLE_SOLICITUD> GetByCodigoSolicitudProducto(int codigoSolicitud, int codigoProducto);
        Task<ResultDto<ADM_DETALLE_SOLICITUD>> Add(ADM_DETALLE_SOLICITUD entity);
        Task<ResultDto<ADM_DETALLE_SOLICITUD>> Update(ADM_DETALLE_SOLICITUD entity);
        Task<string> Delete(int codigoSolicitud);
        Task<int> GetNextKey();

        Task RecalculaImpuesto(int codigoPresupuesto, int codigoSolicitud);
        Task<TotalesResponseDto> GetTotales(int codigoPresupuesto, int codigoSolicitud);
        Task<bool> ExisteImpuesto(int codigoPresupuesto, int codigoSolicitud);

        Task<string> DeleteBySolicitud(int codigoSolicitud);
        Task<string> ActualizaMontos(int codigoPresupuesto);
        Task<string> LimpiaEnrer();
    }
}
