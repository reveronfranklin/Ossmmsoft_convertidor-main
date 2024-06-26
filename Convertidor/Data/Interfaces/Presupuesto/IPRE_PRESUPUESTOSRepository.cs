﻿using Convertidor.Data.Entities.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_PRESUPUESTOSRepository
	{

        Task<IEnumerable<PRE_PRESUPUESTOS>> GetAll();

        Task<PRE_PRESUPUESTOS> GetByCodigo(int codigoEmpresa, int codigoPresupuesto);
        Task<PRE_PRESUPUESTOS> GetByCodigoPresupuesto(int codigoPresupuesto);

        Task<ResultDto<PRE_PRESUPUESTOS>> GetByCodigoEmpresaPeriodo(int codigoEmpresa, int periodo);
        Task<ResultDto<PRE_PRESUPUESTOS>> Add(PRE_PRESUPUESTOS entity);

        Task<ResultDto<PRE_PRESUPUESTOS>> Update(PRE_PRESUPUESTOS entity);

        Task<string> Delete(int codigoEmpresa, int codigoPresupuesto);
        Task<bool> ExisteEnPeriodo(int codigoEmpresa, DateTime desde, DateTime hasta);

        Task<int> GetNextKey();
        Task<PRE_PRESUPUESTOS> GetUltimo();

        Task RecalcularSaldo(int codigo_presupuesto);
        Task<PRE_PRESUPUESTOS> GetLast();
        Task AprobarPresupuesto(int codigoPresupuesto, int codigoUsuario, int codigoEmpresa);


	}
}

