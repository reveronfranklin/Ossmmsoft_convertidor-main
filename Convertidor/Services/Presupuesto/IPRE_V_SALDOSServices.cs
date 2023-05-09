using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPRE_V_SALDOSServices
	{

       
      
        Task<ResultDto<List<PreVSaldosGetDto>>> GetAll(FilterPRE_V_SALDOSDto filter);
        Task<ResultDto<List<PreVSaldosGetDto>>> GetAllByPresupuestoIpcPuc(FilterPresupuestoIpcPuc filter);
        Task<ResultDto<List<PreSaldoPorPartidaGetDto>>> GetAllByPresupuestoPucConcat(FilterPresupuestoPucConcat filter);
    }
}

