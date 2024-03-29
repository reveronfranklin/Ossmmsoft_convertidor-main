﻿namespace Convertidor.Dtos.Adm
{
    public class AdmRetencionesOpUpdateDto
    {
        public int CodigoRetencionOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int TipoRetencionId { get; set; }
        public int CodigoRetencion { get; set; }
        public int PorRetencion { get; set; }
        public int MontoRetencion { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public string Extra4 { get; set; } = string.Empty;
        public int BaseImponible { get; set; }
    }
}
