﻿using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Data.Interfaces.Adm
{
    public interface IAdmOrdenPagoRepository
    {
        Task<ADM_ORDEN_PAGO> GetCodigoOrdenPago(int codigoOrdenPago);
        Task<ResultDto<ADM_ORDEN_PAGO>> Add(ADM_ORDEN_PAGO entity);
        Task<ResultDto<ADM_ORDEN_PAGO>> Update(ADM_ORDEN_PAGO entity);
        Task<string> Delete(int codigoOrdenPago);
        Task<int> GetNextKey();
        Task<ResultDto<List<ADM_ORDEN_PAGO>>> GetByPresupuesto(AdmOrdenPagoFilterDto filter);
        Task<string> GetNextOrdenPago(int codigoPresupuesto);
    }
}
