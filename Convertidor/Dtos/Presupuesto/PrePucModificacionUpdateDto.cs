namespace Convertidor.Dtos.Presupuesto
{
    public class PrePucModificacionUpdateDto
    {
        public int CodigoPucModificacion { get; set; }
        public int CodigoModificacion { get; set; }
        public int CodigoSaldo { get; set; }
        public string FinanciadoId { get; set; } = string.Empty;
        public int CodigoFinanciado { get; set; }
        public int CodigoIcp { get; set; }
        public int CodigoPuc { get; set; }
        public int Monto { get; set; }
        public string DePara { get; set; } = string.Empty;
        public int Extra1 { get; set; }
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPucSolModificacion { get; set; }
        public int MontoAnulado { get; set; }
        public int CodigoPresupuesto { get; set; }
    }
}
