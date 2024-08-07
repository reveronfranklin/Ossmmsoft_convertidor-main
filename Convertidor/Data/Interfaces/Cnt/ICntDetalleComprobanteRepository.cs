﻿using Convertidor.Data.Entities.Cnt;

namespace Convertidor.Data.Interfaces.Cnt
{
    public interface ICntDetalleComprobanteRepository
    {
        Task<List<CNT_DETALLE_COMPROBANTE>> GetAll();
        Task<CNT_DETALLE_COMPROBANTE> GetByCodigo(int codigoDetalleComprobante);
        Task<List<CNT_DETALLE_COMPROBANTE>> GetByCodigoComprobante(int codigoComprobante);
        Task<ResultDto<CNT_DETALLE_COMPROBANTE>> Add(CNT_DETALLE_COMPROBANTE entity);
        Task<ResultDto<CNT_DETALLE_COMPROBANTE>> Update(CNT_DETALLE_COMPROBANTE entity);
        Task<string> Delete(int codigoDetalleComprobante);
        Task<int> GetNextKey();
    }
}
