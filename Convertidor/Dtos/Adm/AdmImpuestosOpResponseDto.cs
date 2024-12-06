namespace Convertidor.Dtos.Adm
{
    public class AdmImpuestosOpResponseDto
    {
        public int CodigoImpuestoOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int TipoImpuestoId { get; set; }
        public string DescripcionTipoImpuesto { get; set; } = string.Empty;
        public int? PorImpuesto { get; set; }
        public int? MontoImpuesto { get; set; }
    
        public int CodigoPresupuesto { get; set; }
    }
}
