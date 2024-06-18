namespace Convertidor.Dtos.Adm
{
    public class AdmImpuestosOpResponseDto
    {
        public int CodigoImpuestoOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int TipoImpuestoId { get; set; }
        public int? PorImpuesto { get; set; }
        public int? MontoImpuesto { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
