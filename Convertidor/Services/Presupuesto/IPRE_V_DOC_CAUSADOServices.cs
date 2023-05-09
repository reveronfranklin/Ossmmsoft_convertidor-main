using System;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPRE_V_DOC_CAUSADOServices
	{
        Task<ResultDto<List<PreDetalleDocumentoGetDto>>> GetAllByCodigoSaldo(FilterDocumentosPreVSaldo filter);


    }
}

