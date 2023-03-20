using System;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPRE_V_DENOMINACION_PUCServices
	{
       
        Task<ResultDto<List<GetPRE_V_DENOMINACION_PUCDto>>> GetAll(FilterPRE_V_DENOMINACION_PUC filter);
        Task<ResultDto<List<GetPRE_V_DENOMINACION_PUCDto>>> GetResumenPresupuestoDenominacionPuc(FilterPreVDenominacionPuc filter);
        List<GetPreDenominacionPucResumenAnoDto> ResumenePreDenominacionPuc(List<GetPRE_V_DENOMINACION_PUCDto> dto);
    }
}

