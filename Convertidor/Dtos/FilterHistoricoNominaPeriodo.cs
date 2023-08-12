using System;
using Convertidor.Dtos.Rh;

namespace Convertidor.Dtos
{
	public class FilterHistoricoNominaPeriodo
	{
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public List<ListTipoNominaDto>? CodigoTipoNomina { get; set; }
        public int CodigoPersona { get; set; }
        public int CodigoProceso { get; set; }
        public List<ListConceptosDto>? CodigoConcepto { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string TipoSort { get; set; }
        public string sortColumn { get; set; }
    }
}

