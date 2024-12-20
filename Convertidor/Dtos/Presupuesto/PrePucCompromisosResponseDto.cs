﻿namespace Convertidor.Dtos.Presupuesto
{
    public class PrePucCompromisosResponseDto
    {
        public int CodigoPucCompromiso { get; set; }
        public int CodigoDetalleCompromiso { get; set; }
        public int CodigoPucSolicitud { get; set; }
        public int CodigoSaldo { get; set; }
        public int CodigoIcp { get; set; }
        public string CodigoIcpConcat { get; set; }
        public string DenominacionIcp { get; set; }
        public int CodigoPuc { get; set; }
        public string CodigoPucConcat { get; set; }
        public string DenominacionPuc { get; set; }
        public int FinanciadoId { get; set; }
        public string DescripcionFinanciado { get; set; }
        public int CodigoFinanciado { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoCausado { get; set; }
        public decimal MontoAnulado { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
    
    
    public class PrePucCompromisosResumenResponseDto
    {
      
        public string CodigoIcpConcat { get; set; }
        public string CodigoPucConcat { get; set; }
        public string DescripcionFinanciado { get; set; }
        public decimal Monto { get; set; }
       
    }
}
