using System;
namespace Convertidor.Dtos
{
	public class FilterHistoricoNominaPeriodo
	{

		public int CodigoPeriodo { get; set; }
		public int  TipoNomina { get; set; }
		public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }
}

