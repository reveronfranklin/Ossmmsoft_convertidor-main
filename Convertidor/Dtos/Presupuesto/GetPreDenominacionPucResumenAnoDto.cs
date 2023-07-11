using System;
using System.Globalization;
using Convertidor.Utility;

namespace Convertidor.Dtos.Presupuesto
{
	public class GetPRE_V_DENOMINACION_PUCDto
    {
        public int AnoSaldo { get; set; } 
        public int MesSaldo { get; set; } 
        public int CodigoPresupuesto { get; set; }
        public string CodigoPartida { get; set; } = string.Empty;
        public string CodigoGenerica { get; set; } = string.Empty;
        public string CodigoEspecifica { get; set; } = string.Empty;
        public string CodigoSubEspecifica { get; set; } = string.Empty;
        public string CodigoNivel5 { get; set; } = string.Empty;
        public string DenominacionPuc { get; set; } = string.Empty;
        public decimal Presupuestado { get; set; }
        public decimal Modificado { get; set; }
        public decimal Comprometido { get; set; }
        public decimal Causado { get; set; }
        public decimal Pagado { get; set; }
        public decimal Deuda { get; set; }
        public decimal Disponibilidad { get; set; }
        public decimal DisponibilidadFinan { get; set; }
        public decimal TotalPresupuestado { get { return Presupuestado; } }

        public string PresupuestadoString { get { return ConvertidorMoneda.ConvertMoneda(TotalPresupuestado); } }
        public string DisponibilidadString { get { return ConvertidorMoneda.ConvertMoneda(Disponibilidad); } }
        public string DisponibilidadFinanString { get { return ConvertidorMoneda.ConvertMoneda(DisponibilidadFinan); } }

      



    }


    
}

