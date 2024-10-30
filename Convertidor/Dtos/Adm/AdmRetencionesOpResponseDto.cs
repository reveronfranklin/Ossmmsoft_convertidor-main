namespace Convertidor.Dtos.Adm
{
    public class AdmRetencionesOpResponseDto
    {
        public int CodigoRetencionOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int TipoRetencionId { get; set; }
        public int? CodigoRetencion { get; set; }
        public decimal? PorRetencion { get; set; }
        public decimal? MontoRetencion { get; set; }
        public string? Extra1 { get; set; } = string.Empty;
        public string? Extra2 { get; set; } = string.Empty;
        public string? Extra3 { get; set; } = string.Empty;
        public int? CodigoPresupuesto { get; set; }
        public string? Extra4 { get; set; } = string.Empty;
        public decimal? BaseImponible { get; set; }
    }
}
