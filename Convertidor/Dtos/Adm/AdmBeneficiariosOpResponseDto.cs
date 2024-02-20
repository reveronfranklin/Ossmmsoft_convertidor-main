namespace Convertidor.Dtos.Adm
{
    public class AdmBeneficiariosOpResponseDto
    {
        public int CodigoBeneficiarioOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int CodigoProveedor { get; set; }
        public int CodigoContactoProveedor { get; set; }
        public float Monto { get; set; }
        public float MontoPagado { get; set; }
        public float MontoAnulado { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
