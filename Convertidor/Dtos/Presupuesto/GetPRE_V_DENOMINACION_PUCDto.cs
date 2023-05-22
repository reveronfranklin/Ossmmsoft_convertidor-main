using System;
using System.Globalization;

namespace Convertidor.Dtos.Presupuesto
{
	public class GetPreDenominacionPucResumenAnoDto
    {
        
        public int AnoSaldo { get; set; }
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
        public decimal TotalPresupuestado { get { return Presupuestado + Modificado; } }

        public string PresupuestadoString { get { return ConvertMoneda(TotalPresupuestado); } }
        public string DisponibilidadString { get { return ConvertMoneda(Disponibilidad); } }
        public string DisponibilidadFinanString { get { return ConvertMoneda(DisponibilidadFinan); } }
        public string CodigoPUC { get { return $"{CodigoPartida}.{CodigoGenerica}.{CodigoEspecifica}.{CodigoSubEspecifica}.{CodigoNivel5}"; } }

        public string ConvertMoneda(decimal value)
        {
            string literal = "";
            if (Pagado == 0)
            {
                literal = "Bs. 0,0";
            }
            else
            {
                literal = "Bs. " + Pagado.ToString("#,#", CultureInfo.InvariantCulture);
            }


            return literal;
        }


    }


    
}

