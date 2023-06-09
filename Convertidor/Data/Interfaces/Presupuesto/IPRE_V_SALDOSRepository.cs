using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Data.Interfaces.Presupuesto
{
	public interface IPRE_V_SALDOSRepository
	{
        Task<IEnumerable<PRE_V_SALDOS>> GetAll(FilterPRE_V_SALDOSDto filter);
        Task RecalcularSaldo(int codigo_presupuesto);
        Task<List<PRE_V_SALDOS>> GetAllByPresupuesto(int codigoPresupuesto);
        Task<List<PRE_V_SALDOS>> GetAllByPresupuestoPucConcat(FilterPresupuestoPucConcat filter);
        Task<ResultDto<List<PreDenominacionDto?>>> GetPreVDenominacionPuc(FilterPreDenominacionDto filter);
    }
}

