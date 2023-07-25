using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_PLAN_UNICO_CUENTASRepository
	{

        Task<IEnumerable<PRE_PLAN_UNICO_CUENTAS>> GetAll();
        Task<List<PRE_PLAN_UNICO_CUENTAS>> GetAllByCodigoPresupuesto(int codigoPresupuesto);
        Task<PRE_PLAN_UNICO_CUENTAS> GetByCodigos(FilterPrePUCPresupuestoCodigos filter);
        Task<PRE_PLAN_UNICO_CUENTAS> GetHastaNivel5(int codigoPresupuesto, int codigoPuc, string grupo, string nivel1, string nivel2, string nivel3, string nivel4, string nivel5);
        Task<PRE_PLAN_UNICO_CUENTAS> GetHastaNivel4(int codigoPresupuesto, int codigoPuc, string grupo, string nivel1, string nivel2, string nivel3, string nivel4);
        Task<PRE_PLAN_UNICO_CUENTAS> GetHastaNivel3(int codigoPresupuesto, int codigoPuc, string grupo, string nivel1, string nivel2, string nivel3);
        Task<PRE_PLAN_UNICO_CUENTAS> GetHastaNivel2(int codigoPresupuesto, int codigoPuc, string grupo, string nivel1, string nivel2);
        Task<PRE_PLAN_UNICO_CUENTAS> GetHastaNivel1(int codigoPresupuesto, int codigoPuc, string grupo, string nivel1);
        Task<PRE_PLAN_UNICO_CUENTAS> GetHastaGrupo(int codigoPresupuesto, int codigoPuc, string grupo);
        Task<PRE_PLAN_UNICO_CUENTAS> GetByCodigo(int codigoIcp);
        Task<string> Delete(int codigoPuc);
        Task<ResultDto<PRE_PLAN_UNICO_CUENTAS>> Update(PRE_PLAN_UNICO_CUENTAS entity);
        Task<ResultDto<PRE_PLAN_UNICO_CUENTAS>> Create(PRE_PLAN_UNICO_CUENTAS entity);
        Task<int> GetNextKey();
        Task<ResultDto<List<PRE_PLAN_UNICO_CUENTAS>>> ClonarByCodigoPresupuesto(int codigoPresupuestoOrigen, int codigoPresupuestoDestino);
    }
}

