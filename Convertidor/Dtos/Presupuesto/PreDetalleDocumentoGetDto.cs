using System;
using System.Globalization;
using Convertidor.Utility;

namespace Convertidor.Dtos.Presupuesto
{
	public class PreDetalleDocumentoGetDto
	{
        public int Id { get; set; }
        public int CodigoSaldo { get; set; }
        public int CodigoPresupuesto { get; set; }      
        public string DESCRIPCION { get; set; } = string.Empty;
        public DateTime Fecha  { get; set; }
		public string Numero { get; set; } = string.Empty;
        public string Origen { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public decimal Monto { get; set; }
     
        public string MontoFormat
        {
            get
            {
                return ConvertidorMoneda.ConvertMoneda(Monto);
              
            }
        }
        public string FechaFormat { get { return Fecha.ToShortDateString(); } }

    }
}

