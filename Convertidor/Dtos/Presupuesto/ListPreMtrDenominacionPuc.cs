using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class ListPreMtrDenominacionPuc
	{
        public int Id { get; set; }
        public int CodigoPuc { get; set; }
        public string CodigoPucConcat { get; set; } = string.Empty;
        public string DenominacionPuc { get; set; } = string.Empty;
        public string Dercripcion { get { return $"{CodigoPucConcat} {DenominacionPuc} "; } }
    }
}

