﻿using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmDetalleSolCompromisoRepository
    {
        Task<ADM_DETALLE_SOL_COMPROMISO> GetByCodigo(int codigoDetalleSolicitud);
        Task<List<ADM_DETALLE_SOL_COMPROMISO>> GetAllByDetalleSolicitudPucSolicitud(int codigoDetalleSolicitud, int codigoPucSolicitud);
        Task<List<ADM_DETALLE_SOL_COMPROMISO>> GetAll();
        Task<ResultDto<ADM_DETALLE_SOL_COMPROMISO>> Add(ADM_DETALLE_SOL_COMPROMISO entity);
        Task<ResultDto<ADM_DETALLE_SOL_COMPROMISO>> Update(ADM_DETALLE_SOL_COMPROMISO entity);
        Task<string> Delete(int codigoDetalleSolicitud);
        Task<int> GetNextKey();
    }
}