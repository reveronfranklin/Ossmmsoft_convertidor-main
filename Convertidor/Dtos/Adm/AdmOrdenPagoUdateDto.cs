﻿namespace Convertidor.Dtos.Adm
{
    public class AdmOrdenPagoUpdateDto
    {
        public int CodigoPresupuesto { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int CodigoProveedor { get; set; }
        public DateTime FechaOrdenPago { get; set; }
        public int TipoOrdenPagoId { get; set; }
        public int CantidadPago { get; set; }
        public int FrecuenciaPagoId { get; set; }
        public int TipoPagoId { get; set; }
        public string Motivo { get; set; } = string.Empty;
        
        public int? NumeroComprobante { get; set; }
        public DateTime? FechaComprobante { get; set; }
        public int? NumeroComprobante2 { get; set; }
        public int? NumeroComprobante3 { get; set; }
        public int? NumeroComprobante4 { get; set; }
    }
}

