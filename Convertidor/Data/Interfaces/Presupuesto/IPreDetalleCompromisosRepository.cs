﻿using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos.Adm;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPreDetalleCompromisosRepository
    {


        Task<PRE_DETALLE_COMPROMISOS> GetByCodigo(int codigoDetalleCompromiso);
        Task<List<PRE_DETALLE_COMPROMISOS>> GetByCodigoCompromiso(int codigoCompromiso);
        Task<List<PRE_DETALLE_COMPROMISOS>> GetAll();
        Task<ResultDto<PRE_DETALLE_COMPROMISOS>> Add(PRE_DETALLE_COMPROMISOS entity);
        Task<ResultDto<PRE_DETALLE_COMPROMISOS>> Update(PRE_DETALLE_COMPROMISOS entity);
        Task<string> Delete(int codigoDetalleCompromiso);
        Task<int> GetNextKey();
        decimal GetTotal(int codigoCompromiso);
        decimal GetTotalMonto(int codigoCompromiso);
        decimal GetTotalImpuesto(int codigoCompromiso);
        Task<string> ActualizaMontos(int codigoPresupuesto);
        Task<string> LimpiaEnrer();
        Task<TotalesResponseDto> GetTotales(int codigoPresupuesto, int codigoCompromiso);
    }
}
