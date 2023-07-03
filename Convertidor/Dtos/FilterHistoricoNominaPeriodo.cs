using System;
namespace Convertidor.Dtos
{
	public class FilterHistoricoNominaPeriodo
	{
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public int CodigoTipoNomina { get; set; }
        public int CodigoPersona { get; set; }
        public string CodigoConcepto { get; set; } = string.Empty;
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

