namespace Convertidor.Dtos.Presupuesto
{
    public class PreCompromisosUpdateDto
    {
        public int CodigoCompromiso { get; set; }
        public int Ano { get; set; }
        public int CodigoSolicitud { get; set; }
        public string NumeroCompromiso { get; set; } = string.Empty;
        public DateTime FechaCompromiso { get; set; }
        public int CodigoProveedor { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int CodigoDirEntrega { get; set; }
        public int TipoPagoId { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public int TipoRenglonId { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
    }
}
