﻿namespace Convertidor.Dtos.Adm
{
    public class AdmPucOrdenPagoResponseDto
    {
        public int CodigoPucOrdenPago { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int CodigoPucCompromiso { get; set; }
        public int CodigoIcp { get; set; }
        
        public string DescripcionIcp { get; set; } = string.Empty;
        public int CodigoPuc { get; set; }
        
        public string DescripcionPuc { get; set; } = string.Empty;
        public int FinanciadoId { get; set; }
        public string DescripcionFinanciado { get; set; } = string.Empty;
        public int CodigoFinanciado { get; set; }
        public int CodigoSaldo { get; set; }
        public int Monto { get; set; }
        public int MontoPagado { get; set; }
        public int MontoAnulado { get; set; }
        public int CodigoCompromisoOp { get; set; }
        public int CodigoPresupuesto { get; set; }
    }
}
